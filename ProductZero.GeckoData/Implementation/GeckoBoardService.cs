// ************************************************************************************************************************************************************
// <copyright file="GeckoBoardService.cs" company="dotCQ Software Development Ltd. & Co. KG">
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

namespace ProductZero.GeckoData.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Amazon;
    using Amazon.S3;
    using Amazon.S3.Model;

    using ProductZero.GeckoData.Configuration;
    using ProductZero.GeckoData.WidgetData;

    internal class GeckoBoardService : IGeckoBoardService
    {
        #region Constants

        private const string SectionName = "Geckoboard";
        private const string AwsSectionName = "AWS"; // TODO: Global const

        #endregion // Constants

        #region Fields

        private readonly string bucketName;
        private readonly Dictionary<string, IGeckoboardWidget> widgetCache;
        private readonly AmazonS3 s3;

        #endregion // Fields

        #region Construction

        public GeckoBoardService()
        {
            var section = (GeckoboardConfigurationSection)ConfigurationManager.GetSection(SectionName);

            this.bucketName = section.S3Bucket.Name;

            this.widgetCache = new Dictionary<string, IGeckoboardWidget>();

            this.s3 = CreateS3Client();
        }

        #endregion // Construction

        #region Operations

        public void SetData(string widgetName, IWidgetData data)
        {
            IGeckoboardWidget widget = this.GetWidget(widgetName);

            if (!data.CorrespondingWidgetType.HasFlag(widget.Type))
            {
                throw new InvalidOperationException(
                    "Widget type (" + widget.Type + ") and data widget type (" + data.CorrespondingWidgetType + ") do not match.");
            }

            this.WriteToBucket(widget.S3FileKey, data);
        }

        private void WriteToBucket(string s3FileKey, IWidgetData data)
        {
            var request = new PutObjectRequest().WithBucketName(this.bucketName)
                                                .WithKey(s3FileKey)
                                                .WithContentBody(data.ToJson())
                                                .WithCannedACL(S3CannedACL.PublicRead);

            this.s3.EndPutObject(this.s3.BeginPutObject(request, null, null));
        }

        #endregion // Operations

        #region Implementation

        private static AmazonS3 CreateS3Client()
        {
            var section = (AwsAcessCredentialsConfigurationSection)ConfigurationManager.GetSection(AwsSectionName);

            return AWSClientFactory.CreateAmazonS3Client(
                section.AccessKeyID.Value,
                section.SecretKey.Value,
                RegionEndpoint.GetBySystemName(section.RegionEndPoint.Name));
        }

        private IGeckoboardWidget GetWidget(string name)
        {
            return this.GetWidgetFromCache(name) ?? this.GetWidgetFromConfig(name);
        }

        private IGeckoboardWidget GetWidgetFromConfig(string name)
        {
            var section = (GeckoboardConfigurationSection)ConfigurationManager.GetSection(SectionName);

            WidgetElement widgetElement = section.Widgets.Cast<WidgetElement>()
                                                 .FirstOrDefault(element => element.Name == name);

            if (widgetElement == null)
            {
                throw new ConfigurationErrorsException("No widget with name '" + name + "' found in configuration.");
            }

            var widget = new GeckoboardWidget
                             {
                                 S3FileKey = widgetElement.FileKey,
                                 Name = widgetElement.Name,
                                 Type = widgetElement.Type
                             };

            this.EnsureCached(widget);

            return widget;
        }

        private void EnsureCached(IGeckoboardWidget widget)
        {
            if (this.widgetCache.ContainsKey(widget.Name))
            {
                this.widgetCache.Remove(widget.Name);
            }

            this.widgetCache.Add(widget.Name, widget);
        }

        private IGeckoboardWidget GetWidgetFromCache(string name)
        {
            return this.widgetCache.ContainsKey(name) ? this.widgetCache[name] : null;
        }

        #endregion // Implementation

    } // class GeckoBoardService

} // namespace