using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarSharing.Web.Areas.Main.Controllers
{
    public class TemplateController : Controller
    {
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Index(string name)
        {
            return PartialView(name);
        }
    }
}