// ************************************************************************************************************************************************************
// <copyright file="ZenDeskTopProposalDataWriter.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace ProductZero.GeckoDataService.DataWriters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ProductZero.Data;
    using ProductZero.GeckoData;
    using ProductZero.GeckoData.Implementation;
    using ProductZero.GeckoData.WidgetData;
    using ProductZero.GeckoData.WidgetData.Implementation;

    using ZendeskApi_v2;
    using ZendeskApi_v2.Models.Forums;
    using ZendeskApi_v2.Models.Topics;

    /// <summary>
    /// Custom Widget:
    /// Topmost 8 feature proposals from forum
    /// </summary>
    public class ZenDeskTopProposalDataWriter : GeckoDataWriter
    {
        public ZenDeskTopProposalDataWriter(ZeroEntities entities)
            : base(entities)
        {
        }

        protected override void ReadToLocalDatabase()
        {
            Dictionary<string, int>  topProposals = GetTopmostProposals();

            foreach (KeyValuePair<string, int> proposal in topProposals)
            {
                Entities.AddToFeatureProposals(new FeatureProposals
                                                   {
                                                       Id = Guid.NewGuid(),
                                                       Date = DateTime.UtcNow,
                                                       Title = proposal.Key,
                                                       Votes = proposal.Value
                                                   });
            }

            Entities.SaveChanges();
        }

        protected override void WriteToGeckoboardWidget()
        {
            IWidgetDataFunnel data = new WidgetDataFunnel { GreenOnTop = true, ShowPercentage = false };

            var tmpDate = DateTime.UtcNow.AddMinutes(-1);

            var proposals = Entities.FeatureProposals.Where(p => p.Date > tmpDate)
                                                     .OrderByDescending(p => p.Date)
                                                     .Take(8)
                                                     .OrderByDescending(p => p.Votes)
                                                     .Take(8);

            foreach (FeatureProposals proposal in proposals)
            {
                data.AddStep(proposal.Votes, proposal.Title);
            }

            new GeckoBoardService().SetData("Highest voted feature proposals", data);
        }

// ReSharper disable LoopCanBeConvertedToQuery
        internal Dictionary<string, int> GetTopmostProposals()
        {
            ZendeskApi api = ApiHelper.CreateZendeskApi();

            Forum proposalForum = api.Forums.GetForums().Forums.FirstOrDefault(forum => forum.Name.ToLowerInvariant().StartsWith("feature"));

            var result = new Dictionary<string, int>();

            foreach (Topic topic in api.Topics.GetTopicsByForum(proposalForum.Id.GetValueOrDefault()).Topics)
            {
                result.Add(topic.Title, api.Topics.GetTopicVotes(topic.Id.GetValueOrDefault()).Count);
            }

            return result.OrderByDescending(kvp => kvp.Value)
                         .Take(8)
                         .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
// ReSharper restore LoopCanBeConvertedToQuery

    } // class ZenDeskTopProposalDataWriter

} // namespace ProductZero.GeckoDataService.DataWriters