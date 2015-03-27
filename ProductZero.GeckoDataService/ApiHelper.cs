// ************************************************************************************************************************************************************
// <copyright file="ApiHelper.cs" company="dotCQ Software Development Ltd. & Co. KG">
//     Copyright (c) 2013, dotCQ Software Development Ltd. & Co. KG. All rights reserved.
// </copyright>
// <authors>
//   <author>Thomas Weller</author>
// </authors>
// <license name="Apache License, Version 2.0">
// 
// Copyright (c) 2013, dotCQ Software Development Ltd. & Co. KG. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. A copy of the License
// is included with the software project which contains this file; or you may obtain a copy at http://www.apache.org/licenses/LICENSE-2.0.
// 
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
// 
// </license>
// <Description/>
// ************************************************************************************************************************************************************

namespace ProductZero.GeckoDataService
{
    using System.Collections.Generic;
    using System.Configuration;

    using MailChimp;

    using RestSharp;

    using ZendeskApi_v2;

    public static class ApiHelper
    {
        public static ZendeskApi CreateZendeskApi()
        {
            string user = ConfigurationManager.AppSettings["zendesk.user"];
            string password = ConfigurationManager.AppSettings["zendesk.password"];
            string url = string.Format(
                "https://{0}.zendesk.com/api/v2", 
                ConfigurationManager.AppSettings["zendesk.subdomain"]);

            return new ZendeskApi(url, user, password);
        }

        public static MCApi CreateMailChimpkApi()
        {
            string apiKey = ConfigurationManager.AppSettings["mailchimp.apikey"];

            return new MCApi(apiKey, true);
        }

        public static string GetDisqusResponse(string resource, string method, IDictionary<string, string> parameters = null)
        {
            var client = new RestClient("https://disqus.com/api/3.0/");
            string apiKey = ConfigurationManager.AppSettings["disqus.apikey"];

            var request = new RestRequest(resource + "/" + method + ".json", Method.GET).AddParameter("api_key", apiKey);

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.GetOrPost);
                }
            }

            return client.Execute(request).Content;
        }

        public static int GetDisqusCount(string resource, string method, IDictionary<string, string> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.Add("limit", "100");

            var response = ApiHelper.GetDisqusResponse(resource, method, parameters);

            int count = CountDisqusResponseKeyword(resource, method, response);

            if (!response.Contains("\"hasNext\": true"))
            {
                return count;
            }

            while (response.Contains("\"hasNext\": true"))
            {
                parameters["cursor"] = ExtractNextCursor(response);
                response = ApiHelper.GetDisqusResponse(resource, method, parameters);
                count += CountDisqusResponseKeyword(resource, method, response);
            }

            return count;
        }

        private static int CountDisqusResponseKeyword(string resource, string method, string disqusResponse)
        {
            string keyword;

            switch (resource)
            {
                case "posts":
                    keyword = "raw_message";
                    break;
                default:
                    return 0;
            }

            int result = 0;
            int index = 0;

            while ((index = disqusResponse.IndexOf(keyword, index + 1, System.StringComparison.Ordinal)) > 0)
            {
                result++;
            }

            return result;
        }

        private static string ExtractNextCursor(string disqusResponse)
        {
            int index1 = disqusResponse.IndexOf("\"next\":", System.StringComparison.Ordinal) + 8;
            int index2 = disqusResponse.IndexOf(',', index1);

            return disqusResponse.Substring(index1, index2 - index1)
                                 .Replace("\"", null)
                                 .Trim();
        }

    } // class ApiHelper

} // namespace ProductZero.GeckoDataService