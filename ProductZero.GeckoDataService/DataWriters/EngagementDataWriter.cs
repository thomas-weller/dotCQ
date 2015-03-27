// ************************************************************************************************************************************************************
// <copyright file="EngagementDataWriter.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace ProductZero.GeckoDataService.DataWriters
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using MailChimp;
    using MailChimp.Types;

    using ProductZero.Data;
    using ProductZero.GeckoData.Implementation;
    using ProductZero.GeckoData.WidgetData;
    using ProductZero.GeckoData.WidgetData.Implementation;

    using ZendeskApi_v2;

    public class EngagementDataWriter : GeckoDataWriter
    {
        public EngagementDataWriter(ZeroEntities entities)
            : base(entities)
        {
        }

        protected override void ReadToLocalDatabase()
        {
            Entities.AddToOnSiteEngagements(new OnSiteEngagements
                                                {
                                                    Id = Guid.NewGuid(),
                                                    Date = DateTime.UtcNow,
                                                    ForumUsers = GetNumberOfForumUsers(),
                                                    NewsLetterSubscriptions = GetNumberOfNewsLetterSubscriptions(),
                                                    Surveys = GetNumberOfSurveys(),
                                                    Comments = GetNumberOfComments()
                                                });

            Entities.SaveChanges();
        }

        protected override void WriteToGeckoboardWidget()
        {
            OnSiteEngagements engagement = Entities.OnSiteEngagements.OrderByDescending(e => e.Date).First();

            IWidgetDataFunnel data = new WidgetDataFunnel { GreenOnTop = true, ShowPercentage = false };

            data.AddStep(engagement.NewsLetterSubscriptions.GetValueOrDefault(), "Subscriptions");
            data.AddStep(engagement.Surveys.GetValueOrDefault(), "Surveys");
            data.AddStep(engagement.ForumUsers.GetValueOrDefault(), "Forum users");
            data.AddStep(engagement.Comments.GetValueOrDefault(), "Comments");

            new GeckoBoardService().SetData("User Engagement", data);
        }

        private static int? GetNumberOfComments()
        {
            try
            {
                return ApiHelper.GetDisqusCount(
                        "posts", 
                        "list", 
                        new Dictionary<string, string>
                            {
                                { "forum", ConfigurationManager.AppSettings["disqus.website.shortname"] }
                            });
            }
            catch
            {
                return null;
            }
        }

        private static int? GetNumberOfSurveys()
        {
            return null; // no api key for SurveyMonkey yet
        }

        private static int? GetNumberOfNewsLetterSubscriptions()
        {
            try
            {
                MCApi api = ApiHelper.CreateMailChimpkApi();

                return api.ListMembers(
                            ConfigurationManager.AppSettings["mailchimp.newsletter.subscriberlist.id"], 
                            List.MemberStatus.Subscribed)
                          .Total;
            }
            catch
            {
                return null;
            }
        }

        private static int? GetNumberOfForumUsers()
        {
            try
            {
                ZendeskApi api = ApiHelper.CreateZendeskApi();

                return api.Users.GetAllUsers().Count;
            }
            catch
            {
                return null;
            }
        }

    } // class EngagementDataWriter

} // namespace ProductZero.GeckoDataService.DataWriters