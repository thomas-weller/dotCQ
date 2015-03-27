// ************************************************************************************************************************************************************
// <copyright file="WidgetDataFunnel.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace ProductZero.GeckoData.WidgetData.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ProductZero.GeckoData;
    using ProductZero.GeckoData.WidgetData;

    internal class WidgetDataFunnel : WidgetDataBase, IWidgetDataFunnel
    {
        private readonly IList<IValueAndLabelDataPoint> steps;

        public WidgetDataFunnel()
            : base(WidgetType.FunnelGraph)
        {
            this.steps = new List<IValueAndLabelDataPoint>();
            this.ShowPercentage = true;
            this.GreenOnTop = false;
        }

        public void AddStep(int value, string label)
        {
            this.steps.Add(new ValueAndLabelDataPoint{ Value = value, Label = label });
        }

        public bool ShowPercentage { get; set; }

        public bool GreenOnTop { get; set; }

        public override string ToJson()
        {
            var stringBuilder = new StringBuilder("{");

            if (this.GreenOnTop)
            {
                stringBuilder.Append("\"type\":\"reverse\",");
            }

            if (!this.ShowPercentage)
            {
                stringBuilder.Append("\"percentage\":\"hide\",");
            }

            stringBuilder.Append("\"item\":[");

            for (int i = 0; i < this.steps.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append(this.steps[i].ToJson());
            }

            stringBuilder.Append("]}");

            return stringBuilder.ToString();
        }

    } // class WidgetDataFunnel

} // namespace