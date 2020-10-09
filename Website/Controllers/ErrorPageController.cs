using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.StatusCode = "Unbekannt";
            ViewBag.Title = "Fehler";

            return View("Error");
        }

        public ActionResult NotFound()
        {
            ViewBag.StatusCode = 404;
            ViewBag.Title = "404 Not Found";

            Response.StatusCode = 200;
            return View("Error");
        }
    }
}