// ************************************************************************************************************************************************************
// <copyright file="IpAddressesDataWriter.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

//namespace ProductZero.GeckoDataService.DataWriters
//{
//    using System;
//    using System.Linq;

//    using ProductZero.Data;
//    using ProductZero.GeckoData;
//    using ProductZero.GeckoData.Implementation;
//    using ProductZero.GeckoData.WidgetData;
//    using ProductZero.GeckoData.WidgetData.Implementation;

//    public class IpAddressesDataWriter : IGeckoDataWriter
//    {
//        public void WriteData(ZeroEntities entities = null)
//        {
//            if (entities == null)
//            {
//                entities = new ZeroEntities();
//            }

//            try
//            {
//                IGeckoBoardService service = new GeckoBoardService();

//                IWidgetDataMap data = new WidgetDataMap();

//                var ipAddresses = entities.IpAddresses.Select(a => a.Address).ToList();

//                foreach (string address in ipAddresses.Select(a => a.Substring(0, a.LastIndexOf('.') + 1) + "1").Distinct())
//                {
//                    data.AddLocation(address);
//                }

//                service.SetData("VisitorsMap", data);
//            }
//// ReSharper disable EmptyGeneralCatchClause
//            catch
//// ReSharper restore EmptyGeneralCatchClause
//            {
//            }
//        }

//    } // class IpAddressesDataWriter

//} // namespace ProductZero.GeckoDataService.DataWriters