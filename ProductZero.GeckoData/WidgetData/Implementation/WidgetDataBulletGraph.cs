// ************************************************************************************************************************************************************
// <copyright file="WidgetDataBulletGraph.cs" company="dotCQ Software Development Ltd. & Co. KG">
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
    using System.Text;
    using System.Web.UI.WebControls;

    using ProductZero.GeckoData;
    using ProductZero.GeckoData.WidgetData;

    internal class WidgetDataBulletGraph : WidgetDataBase, IWidgetDataBulletGraph
    {
        public WidgetDataBulletGraph()
            : base(WidgetType.BulletGraph)
        {
            this.Orientation = Orientation.Horizontal;
            this.Items = new IBulletGraphItemData[4];
            this.Items[0] = new BulletGraphItemData();
        }

        public Orientation Orientation { get; set; }

        public IBulletGraphItemData[] Items { get; private set; }

        public override string ToJson()
        {
            bool isArray = this.Items[1] != null || this.Items[2] != null || this.Items[3] != null;

            var stringBuilder = new StringBuilder("{");

            stringBuilder.Append("\"orientation\":\"" + this.Orientation.ToString().ToLowerInvariant() + "\"");

            stringBuilder.Append(",\"item\":");

            if (isArray)
            {
                stringBuilder.Append("[");
            }

            stringBuilder.Append(this.Items[0].ToJson());

            if (this.Items[1] != null)
            {
                stringBuilder.Append("," + this.Items[1].ToJson());
            }

            if (this.Items[2] != null)
            {
                stringBuilder.Append("," + this.Items[2].ToJson());
            }

            if (this.Items[3] != null)
            {
                stringBuilder.Append("," + this.Items[3].ToJson());
            }

            if (isArray)
            {
                stringBuilder.Append("]");
            }

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        private void AppendItem(StringBuilder stringBuilder, IBulletGraphItemData data)
        {
            stringBuilder.Append("\"item\":" + data.ToJson());
        }

    } // class WidgetDataBulletGraph

} // namespace