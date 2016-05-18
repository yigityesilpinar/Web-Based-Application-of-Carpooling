using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CarSharing.Web.Filters
{
    public class ValidatedMvcActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                var errors = filterContext.Controller.ViewData.ModelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new { Message = s.Value.Errors.First().ErrorMessage })
                    .ToArray();

                filterContext.Result = new JsonResult() { Data = new { Error = true, Messages = errors } };
            }
        }
    }
}
