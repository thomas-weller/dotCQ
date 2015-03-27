// ************************************************************************************************************************************************************
// <copyright file="ZeroController.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace WebTriq.Controllers
{
    using System;
    using System.Linq;

    using BootstrapMvcSample.Controllers;

    using ProductZero.Data;

    public class ZeroController : BootstrapBaseController
    {
        // protected readonly ZeroEntities Entities = new ZeroEntities();

        protected IpAddresses GetIpAddress(string userHostAddress)
        {
            return null;

            //var address = Entities.IpAddresses.FirstOrDefault(a => a.Address == userHostAddress);

            //if (address != null)
            //{
            //    return address;
            //}

            //try
            //{
            //    address = new IpAddresses { Id = Guid.NewGuid(), Address = userHostAddress };

            //    Entities.AddToIpAddresses(address);
            //    Entities.SaveChanges();

            //    return address;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }


    } // class ZeroController

} // namespace WebTriq.Controllers