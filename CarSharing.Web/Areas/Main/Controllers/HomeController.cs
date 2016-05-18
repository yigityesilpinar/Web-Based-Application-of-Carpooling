using CarSharing.Domain.DataAccess;
using CarSharing.Domain.Helper;
using CarSharing.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarSharing.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           return View();
        }
    }
}
