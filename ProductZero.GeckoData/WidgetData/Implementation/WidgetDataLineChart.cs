// ************************************************************************************************************************************************************
// <copyright file="WidgetDataLineChart.cs" company="dotCQ Software Development Ltd. & Co. KG">
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
    using System.Globalization;
    using System.Text;

    using ProductZero.GeckoData;
    using ProductZero.GeckoData.WidgetData;

    internal class WidgetDataLineChart : WidgetDataBase, IWidgetDataLineChart
    {
        enum Axis
        {
            X,
            Y
        }

        public WidgetDataLineChart()
            : base(WidgetType.LineChart)
        {
            this.Items = new List<double>();
            this.AxisX = new List<string>();
            this.AxisY = new List<string>();
            this.Color = "ff9900";
        }

        public IList<double> Items { get; private set; }

        public IList<string> AxisX { get; private set; }

        public IList<string> AxisY { get; private set; }

        public string Color { get; set; }

        public override string ToJson()
        {
            var stringBuilder = new StringBuilder("{");

            this.AppendItems(stringBuilder);
            this.AppendSettings(stringBuilder);

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        private void AppendSettings(StringBuilder stringBuilder)
        {
            stringBuilder.Append("\"settings\":{");

            this.AppendAxis(stringBuilder, Axis.X);
            this.AppendAxis(stringBuilder, Axis.Y);
            stringBuilder.Append("\"colour\":\"" + this.Color + "\"");

            stringBuilder.Append("}");
        }

        private void AppendAxis(StringBuilder stringBuilder, Axis axis)
        {
            string axisName = axis == Axis.X ? "axisx" : "axisy";
            IList<string> axisValues = axis == Axis.X ? this.AxisX : this.AxisY;

            stringBuilder.Append("\"" + axisName + "\":[");

            for (int i = 0; i < axisValues.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append("\"" + axisValues[i] + "\"");
            }

            stringBuilder.Append("],");
        }

        private void AppendItems(StringBuilder stringBuilder)
        {
            stringBuilder.Append("\"item\":[");

            for (int i = 0; i < this.Items.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append("\"" + Math.Round(this.Items[i], 1).ToString(CultureInfo.InvariantCulture) + "\"");
            }

            stringBuilder.Append("],");
        }

    } // class WidgetDataLineChart

} // namespace