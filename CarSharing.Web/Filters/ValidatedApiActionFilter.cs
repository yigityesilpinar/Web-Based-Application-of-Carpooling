using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace CarSharing.Web.Filters
{
    public class ValidatedApiActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errors = actionContext.ModelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new {Key = s.Key, Message = s.Value.Errors.First().ErrorMessage})
                    .ToArray();

                var response = new { Error = true, Messages = errors };
                actionContext.Response = actionContext.Request.CreateResponse();
                actionContext.Response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(response, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings));
            }
        }
    }
}