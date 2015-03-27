// ************************************************************************************************************************************************************
// <copyright file="WidgetDataText.cs" company="dotCQ Software Development Ltd. & Co. KG">
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
    using System.Collections.Generic;
    using System.Text;

    internal class WidgetDataText : WidgetDataBase, IWidgetDataText
    {
        private readonly IList<ITextDataPoint> dataPoints;
 
        public WidgetDataText()
            : base(WidgetType.Text)
        {
            this.dataPoints = new List<ITextDataPoint>();
        }

        protected override void AppendData(StringBuilder stringBuilder)
        {
            for (int i = 0; i < this.dataPoints.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append(this.dataPoints[i].ToJson());
            }
        }

        public void AddText(string text, WidgetCornerType cornerType = WidgetCornerType.None)
        {
            if (this.dataPoints.Count < 10)
            {
                this.dataPoints.Add(new TextDataPoint { Text = text, CornerType = cornerType });
            }
        }

    } // class WidgetDataText

} // namespace