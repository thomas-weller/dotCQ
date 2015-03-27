// ************************************************************************************************************************************************************
// <copyright file="NUnitFixture1.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace TestData
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    using ProductZero.Data;
    using ProductZero.GeckoDataService.DataWriters;

    [TestFixture]
    public class NUnitFixture1
    {
        #region Tests
        // ReSharper disable InconsistentNaming

        [Test]
        public void WriteZenDeskForumPieData()
        {
            var writer = new ZenDeskForumPieDataWriter(new ZeroEntities());

            writer.WriteData();
        }

        [Test]
        public void WriteZenDeskTopProposalData()
        {
            var writer = new ZenDeskTopProposalDataWriter(new ZeroEntities());

            writer.WriteData();
        }

        [Test]
        public void WriteEngagementData()
        {
            var writer = new EngagementDataWriter(new ZeroEntities());

            writer.WriteData();
        }

        [Test]
        public void TestExchangeRates()
        {
            CalculationDataWriter.GetOpenExchangeRates("EUR");

            foreach (KeyValuePair<string, double> rate in CalculationDataWriter.ConversionRates)
            {
                Console.WriteLine("{0}: {1}", rate.Key, rate.Value);
            }
        }

        [Test]
        public void WriteCalculationData()
        {
            var writer = new CalculationDataWriter(new ZeroEntities());

            writer.WriteData();
        }

        // ReSharper restore InconsistentNaming
        #endregion // Tests

    } // class NUnitFixture1

} // namespace TestData