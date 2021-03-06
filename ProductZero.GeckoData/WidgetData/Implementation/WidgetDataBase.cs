﻿// ************************************************************************************************************************************************************
// <copyright file="WidgetDataBase.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

    using ProductZero.GeckoData;
    using ProductZero.GeckoData.WidgetData;

    internal abstract class WidgetDataBase : IWidgetData
    {
        protected WidgetDataBase(WidgetType correspondingWidgetType)
        {
            this.CorrespondingWidgetType = correspondingWidgetType;
        }

        public WidgetType CorrespondingWidgetType { get; private set; }

        public virtual string ToJson()
        {
            var stringBuilder = new StringBuilder("{\"item\":[");

            this.AppendData(stringBuilder);

            stringBuilder.Append("]}");

            return stringBuilder.ToString();
        }

        protected virtual void AppendData(StringBuilder stringBuilder)
        {
        }

    } // class WidgetDataBase

} // namespace