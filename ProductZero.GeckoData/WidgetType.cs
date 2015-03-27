// ************************************************************************************************************************************************************
// <copyright file="WidgetType.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace ProductZero.GeckoData
{
    using System;

    /// <summary>
    /// Defines the various custom widget types available with Geckoboard.
    /// </summary>
    /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/">Custom Widget Types</seealso>
    [Flags]
    public enum WidgetType 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/number-and-optional-secondary-stat/">Geckoboard reference documentation</seealso>
        NumberAndSecondaryStat = 0x1,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/rag-column-and-numbers/">Geckoboard reference documentation</seealso>
        RagColumnAndNumbers = 0x2,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/rag-numbers-only/">Geckoboard reference documentation</seealso>
        RagNumbers = 0x4,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/text">Geckoboard reference documentation</seealso>
        Text = 0x8,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/pie-chart">Geckoboard reference documentation</seealso>
        PieChart = 0x10,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/line-chart">Geckoboard reference documentation</seealso>
        LineChart = 0x20,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/geck-o-meter">Geckoboard reference documentation</seealso>
        GeckOMeter = 0x40,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/funnel">Geckoboard reference documentation</seealso>
        FunnelGraph = 0x80,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/bullet-graph">Geckoboard reference documentation</seealso>
        BulletGraph = 0x100,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/highcharts">Geckoboard reference documentation</seealso>
        /// <remarks>
        /// Note that this widget type is currently not supported!
        /// </remarks>
        HighChart = 0x200,

        /// <summary>
        /// 
        /// </summary>
        /// <seealso href="http://www.geckoboard.com/developers/custom-widgets/widget-types/map">Geckoboard reference documentation</seealso>
        Map = 0x400
	
    } // enum WidgetType 

} // namespace