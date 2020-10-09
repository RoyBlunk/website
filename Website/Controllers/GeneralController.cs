using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class GeneralController : Controller
    {
        public ActionResult RegisterLogin(int? state)
        {
            ViewBag.State = state;
            ViewBag.Title = state == 0 ? "Login" : "Register";
            return View();
        }
    }
}