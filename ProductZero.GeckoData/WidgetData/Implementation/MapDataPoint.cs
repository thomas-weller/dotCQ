// ************************************************************************************************************************************************************
// <copyright file="MapDataPoint.cs" company="dotCQ Software Development Ltd. & Co. KG">
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
    using System.Text;
    using System.Text.RegularExpressions;

    using ProductZero.GeckoData.WidgetData;

    internal class MapDataPoint : IMapDataPoint
    {
        private string location;

        private MapDataPointLocationType locationType;

        public string ToJson()
        {
            var stringBuilder = new StringBuilder("{");

            if (this.locationType == MapDataPointLocationType.IpAddress)
            {
                stringBuilder.Append("\"ip\":\"" + this.location + "\"");
            }
            else if (this.locationType == MapDataPointLocationType.Url)
            {
                stringBuilder.Append("\"host\":\"" + this.location + "\"");
            }

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                MapDataPointLocationType locType = this.GetLoationType(value);

                if (locType != MapDataPointLocationType.Undefined)
                {
                    this.locationType = locType;
                    this.location = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Location format: '" + value + "'");
                }
            }
        }

        public MapDataPointLocationType LocationType
        {
            get
            {
                if (this.locationType == MapDataPointLocationType.Undefined)
                {
                    this.locationType = this.GetLoationType(this.location);
                }
                
                return this.locationType;
            }
        }

        private MapDataPointLocationType GetLoationType(string locationString)
        {
            if (!string.IsNullOrWhiteSpace(locationString))
            {
                if (IsIpAddress(locationString))
                {
                    return MapDataPointLocationType.IpAddress;
                }

                if (Uri.IsWellFormedUriString(locationString, UriKind.RelativeOrAbsolute))
                {
                    return MapDataPointLocationType.Url;
                }
            }

            return MapDataPointLocationType.Undefined;
        }

        private static bool IsIpAddress(string locationString)
        {
            return Regex.IsMatch(
                locationString, 
                @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$\b");
        }

    } // class MapDataPoint

} // namespace