// ************************************************************************************************************************************************************
// <copyright file="CalculationDataWriter.cs" company="dotCQ Software Development Ltd. & Co. KG">
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
    using System.Net;
    using System.Web;

    using Newtonsoft.Json;

    using ProductZero.Data;

    using RestSharp;

    public class CalculationDataWriter : GeckoDataWriter
    {
        public const string BaseCurrency = "EUR";
        private static readonly DateTime Date = DateTime.UtcNow;
        internal static Dictionary<string, double> ConversionRates; // base EUR

        public CalculationDataWriter(ZeroEntities entities)
            : base(entities)
        {
        }

        protected override void ReadToLocalDatabase()
        {
            if (!GetOpenExchangeRates(BaseCurrency))
            {
                return;
            }

            foreach (Calculations calculation in Entities.Calculations.Where(c => c.NormalizedCalculation == null))
            {
                AddNormalizedCalculation(calculation);
            }

            Entities.SaveChanges();
        }

        protected override void WriteToGeckoboardWidget()
        {
        }

        private void AddNormalizedCalculation(Calculations calculation)
        {
            double conversionFactor = 1 / ConversionRates[calculation.Currency];

            Entities.AddToNormalizedCalculations(new NormalizedCalculations
                                                     {
                                                         CalculationId = calculation.Id,
                                                         Timestamp = Date,
                                                         SrcCurrency = calculation.Currency,
                                                         Currency = BaseCurrency,
                                                         StaffCostsPerYear = calculation.StaffCostsPerYear * conversionFactor,
                                                         ProjectSize = calculation.ProjectSize,
                                                         LifeTime = calculation.Lifetime,
                                                         Quality = calculation.Quality,
                                                         SurplusPercent = calculation.SurplusPercent,
                                                         SurplusInvestment = calculation.SurplusInvestment * conversionFactor,
                                                         DevCosts = calculation.DevCosts * conversionFactor,
                                                         ModDevCosts = calculation.ModDevCosts * conversionFactor,
                                                         MaintCosts = calculation.MaintCosts * conversionFactor,
                                                         ModMaintCosts = calculation.ModMaintCosts * conversionFactor,
                                                         TotalCosts = calculation.TotalCosts * conversionFactor,
                                                         ModTotalCosts = calculation.ModTotalCosts * conversionFactor,
                                                         MaintPercent = calculation.MaintPercent,
                                                         ModMaintPercent = calculation.ModMaintPercent,
                                                         BreakEven = calculation.BreakEven,
                                                         Roi = calculation.Roi,
                                                         AvgYearlySaving = calculation.AvgYearlySaving * conversionFactor,
                                                         MaxYearlySaving = calculation.MaxYearlySaving * conversionFactor,
                                                         MinYearlySaving = calculation.MinYearlySaving * conversionFactor
                                                     });
        }

        internal static bool GetOpenExchangeRates(string baseCurrency)
        {
            try
            {
                string url = string.Format(
                    "http://openexchangerates.org/api/historical/{0}-{1}-{2}.json?app_id={3}",
                    Date.Year.ToString("D4"),
                    Date.Month.ToString("D2"),
                    Date.Day.ToString("D2"),
                    ConfigurationManager.AppSettings["openrate.appid"]);

                var client = new RestClient();
                var request = new RestRequest(url, Method.GET);

                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus != ResponseStatus.Completed || response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.ErrorException != null)
                    {
                        throw response.ErrorException;
                    }

                    throw new HttpException(response.StatusDescription);
                }

                int idxOpeningBracket = response.Content.LastIndexOf('{');
                string ratesData = '{' +
                                   response.Content.Substring(idxOpeningBracket + 1)
                                           .Replace('}', ' ')
                                           .Trim() +
                                   '}';

                ConversionRates = JsonConvert.DeserializeObject<Dictionary<string, double>>(ratesData);

                double factor = 1 / ConversionRates[baseCurrency];

                for (int i = 0; i < ConversionRates.Count; i++)
                {
                    string key = ConversionRates.ElementAt(i).Key;
                    ConversionRates[key] *= factor;
                }

                return true;
            }
            catch
            {
                ConversionRates.Clear();
                return false;
            }
        }

    } // class CalculationDataWriter

} // namespace ProductZero.GeckoDataService.DataWriters