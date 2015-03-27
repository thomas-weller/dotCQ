/* ****************************************************************************  
 *
 * Copyright (c) Microsoft Corporation. All rights reserved.
 *
 * Content of this class was mostly derived from the original
 * JsonResult class in the System.Web.Mvc 2.0 RTM Assembly. This
 * has beeen slightly extended for use with JSONP calls.
 *
 * This software is subject to the Microsoft Public License (Ms-PL).
 * A copy of the license can be found in the license.htm file included
 * in this distribution.
 *
 * You must not remove this notice, or any other, from this software.
 *
 * ***************************************************************************/

// ReSharper disable once CheckNamespace
namespace System.Web.Mvc
{
    using System;
    using Text;
    using Web;
    using Script.Serialization;

    public class JsonpResult : ActionResult
    {
        public Encoding ContentEncoding
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public string JsonCallback { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.JsonCallback = context.HttpContext.Request["jsoncallback"];

            if (string.IsNullOrEmpty(this.JsonCallback))
                this.JsonCallback = context.HttpContext.Request["callback"];

// ReSharper disable once NotResolvedInText
            if (string.IsNullOrEmpty(this.JsonCallback))
                throw new ArgumentNullException("JsonCallback required for JSONP response.");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                var serializer = new JavaScriptSerializer();
                response.Write(string.Format("{0}({1});", this.JsonCallback, serializer.Serialize(Data)));
            }
        }
    }

    //extension methods for the controller to allow jsonp.
    public static class ContollerExtensions
    {
        public static JsonpResult Jsonp(this Controller controller, object data)
        {
            var result = new JsonpResult {Data = data};
            return result;
        }
    }
}