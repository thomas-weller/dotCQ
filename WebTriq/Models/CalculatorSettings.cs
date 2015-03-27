// ************************************************************************************************************************************************************
// <copyright file="CalculatorSettings.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace WebTriq.Models
{
    using System;

    using Newtonsoft.Json;

    public class CalculatorSettings
    {
        [JsonProperty("c")]
        public string Currency { get; set; }
        [JsonProperty("sc")]
        public int StaffCostsPerYear { get; set; }
        [JsonProperty("s")]
        public int ProjectSize { get; set; } // initial dev effort in man-years
        [JsonProperty("t")]
        public int Lifetime { get; set; }
        [JsonProperty("q")]
        public int InitialQuality { get; set; } // 1-4
        [JsonProperty("sp")]
        public int AdditionalQualityEffort { get; set; } // in percent

        public CalculatorSettings()
        {
            this.StaffCostsPerYear = 85000;
            this.Currency = "USD";
            this.Lifetime = 10;
            this.ProjectSize = 5;
            this.InitialQuality = 2;
            this.AdditionalQualityEffort = 25;
        }

    } // class CalculatorSettings

} // namespace WebTriq.Models