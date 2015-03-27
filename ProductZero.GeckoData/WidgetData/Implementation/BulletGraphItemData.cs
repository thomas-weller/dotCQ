// ************************************************************************************************************************************************************
// <copyright file="BulletGraphItemData.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

    using ProductZero.GeckoData.WidgetData;

    internal class BulletGraphItemData : IBulletGraphItemData
    {
        public BulletGraphItemData()
        {
            this.Scale = new List<double>();
            this.FirstRange = new ColorStartEndDataPoint();
            this.MiddleRange = new ColorStartEndDataPoint();
            this.LastRange = new ColorStartEndDataPoint();
            this.CurrentMeasure = new StartEndDataPoint();
            this.ProjectedMeasure = new StartEndDataPoint();
        }

        public string ToJson()
        {
            var stringBuilder = new StringBuilder("{");

            stringBuilder.Append("\"label\":\"" + this.Label + "\",");

            if (!string.IsNullOrEmpty(this.Sublabel))
            {
                stringBuilder.Append("\"sublabel\":\"" + this.Sublabel + "\",");
            }

            this.AppendScale(stringBuilder);
            stringBuilder.Append(",");

            this.AppendRange(stringBuilder);
            stringBuilder.Append(",");

            this.AppendMeasures(stringBuilder);

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        public string Label { get; set; }

        public string Sublabel { get; set; }

        public IList<double> Scale { get; private set; }

        public IColorStartEndDataPoint FirstRange { get; private set; }

        public IColorStartEndDataPoint MiddleRange { get; private set; }

        public IColorStartEndDataPoint LastRange { get; private set; }

        public IStartEndDataPoint CurrentMeasure { get; private set; }

        public IStartEndDataPoint ProjectedMeasure { get; private set; }

        public double? ComparativeMeasure { get; set; }

        private void AppendScale(StringBuilder stringBuilder)
        {
            stringBuilder.Append("\"axis\":{\"point\":[");

            for (int i = 0; i < this.Scale.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append(Math.Round(this.Scale[i], 2).ToString(CultureInfo.InvariantCulture));
            }

            stringBuilder.Append("]}");
        }

        private void AppendRange(StringBuilder stringBuilder)
        {
            stringBuilder.Append("\"range\":[");

            stringBuilder.Append(this.FirstRange.ToJson());
            stringBuilder.Append("," + this.MiddleRange.ToJson());
            stringBuilder.Append("," + this.LastRange.ToJson());

            stringBuilder.Append("]");
        }

        private void AppendMeasures(StringBuilder stringBuilder)
        {
            stringBuilder.Append("\"measure\":{");

            stringBuilder.Append("\"current\":" + this.CurrentMeasure.ToJson());

            if (this.ProjectedMeasure.End > this.ProjectedMeasure.Start)
            {
                stringBuilder.Append(",\"projected\":" + this.ProjectedMeasure.ToJson());
            }

            stringBuilder.Append("}");

            if (this.ComparativeMeasure.HasValue)
            {
                stringBuilder.Append(
                    ",\"comparative\":{\"point\":\"" + 
                    Math.Round(this.ComparativeMeasure.Value, 2).ToString(CultureInfo.InvariantCulture) + 
                    "\"}");
            }
        }

    } // class BulletGraphItemData

} // namespace