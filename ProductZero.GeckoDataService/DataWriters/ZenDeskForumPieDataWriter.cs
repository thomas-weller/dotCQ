// ************************************************************************************************************************************************************
// <copyright file="ZenDeskForumPieDataWriter.cs" company="dotCQ Software Development Ltd. & Co. KG">
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
    using ProductZero.GeckoData.Implementation;
    using ProductZero.GeckoData.WidgetData;
    using ProductZero.GeckoData.WidgetData.Implementation;

    using ZendeskApi_v2;
    using ZendeskApi_v2.Models.Forums;

    /// <summary>
    /// Custom Widget:
    /// Pie chart of forums and number of topics
    /// </summary>
    public class ZenDeskForumPieDataWriter : GeckoDataWriter
    {
        private readonly string[] colors = new[] { "FFFF10AA", "FFAA0AAA", "FF5505AA", "FF0000AA", "A60000", "BF3030", "FF4040", "FF7373" };

        public ZenDeskForumPieDataWriter(ZeroEntities entities)
            : base(entities)
        {
        }

        protected override void ReadToLocalDatabase()
        {
            Dictionary<string, int> forumInfos = GetForumInfo();

            foreach (KeyValuePair<string, int> forumInfo in forumInfos)
            {
                Entities.AddToForums(new Forums
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.UtcNow,
                    Name = forumInfo.Key,
                    Topics = forumInfo.Value
                });
            }

            Entities.SaveChanges();
        }

        protected override void WriteToGeckoboardWidget()
        {
            IWidgetDataPieChart data = new WidgetDataPieChart();

            var tmpDate = DateTime.UtcNow.AddMinutes(-1);

            var forums = Entities.Forums.Where(f => f.Date > tmpDate)
                                        .OrderByDescending(f => f.Date)
                                        .Take(8)
                                        .OrderByDescending(f => f.Topics)
                                        .Take(8)
                                        .ToList();

            for (int i = 0; i < forums.Count; i++)
            {
                Forums forum = forums[i];
                data.AddSlice(forum.Topics, forum.Name + " (" + forum.Topics + ")", colors[i]);
            }

            new GeckoBoardService().SetData("Forums Pie Chart", data);
        }

            // ReSharper disable LoopCanBeConvertedToQuery
        private Dictionary<string, int> GetForumInfo()
        {
            var result = new Dictionary<string, int>();

            ZendeskApi api = ApiHelper.CreateZendeskApi();

            foreach (Forum forum in api.Forums.GetForums().Forums)
            {
                result.Add(forum.Name, api.Topics.GetTopicsByForum(forum.Id.GetValueOrDefault()).Count);
            }

            return result.OrderByDescending(kvp => kvp.Value)
                         .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
            // ReSharper restore LoopCanBeConvertedToQuery

    } // class ZenDeskForumPieDataWriter

} // namespace ProductZero.GeckoDataService.DataWriters