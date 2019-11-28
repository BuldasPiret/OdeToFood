using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class SaltyController : Controller
    {
        // GET: Salty
        public ActionResult Find(string soolane, string toiduaine)
        {
            return Content("This " + toiduaine + " is " + soolane);
        }
    }
}