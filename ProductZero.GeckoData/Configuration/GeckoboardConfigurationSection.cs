// ************************************************************************************************************************************************************
// <copyright file="GeckoboardConfigurationSection.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace ProductZero.GeckoData.Configuration
{
    using System.Configuration;

    public class GeckoboardConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("S3Bucket", IsRequired = true)]
        public S3BucketElement S3Bucket
        {
            get
            {
                return (S3BucketElement)this["S3Bucket"];
            }
        }

        [ConfigurationProperty("Widgets", IsRequired = true)]
        [ConfigurationCollection(typeof(WidgetElementCollection), AddItemName = "Widget")]
        public WidgetElementCollection Widgets
        {
            get
            {
                return (WidgetElementCollection)this["Widgets"];
            }
        }

    } // class GeckoboardConfigurationSection

} // namespace