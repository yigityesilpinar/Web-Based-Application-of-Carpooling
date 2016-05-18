using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System.Web.Mvc
{
    public class JsonNetResult : ActionResult
    {
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }

        public JsonSerializerSettings SerializerSettings { get; set; }

        public JsonNetResult()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.StatusCode = StatusCode;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if ((StatusCode >= 400) && (StatusCode <= 599))
                response.TrySkipIisCustomErrors = true;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null)
                return;

            var formatting = Formatting.None;

            var writer = new JsonTextWriter(response.Output) { Formatting = formatting };

            var serializer = JsonSerializer.Create(SerializerSettings);
            serializer.Serialize(writer, Data);

            writer.Flush();
        }
    }

    public static class ContollerExtensions
    {
        public static JsonNetResult JsonNet(this Controller controller, object data)
        {
            return new JsonNetResult { Data = data };
        }
    }
}