using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (AgarEntities database = new Models.AgarEntities())
            {
                int data = database.UserData.Where(i => i.UserID == 2).Select(i => i.Level).FirstOrDefault();
                ViewBag.Level = data;
            }

            using (Models.SecondNamespace.TestSiteEntities2 db = new Models.SecondNamespace.TestSiteEntities2())
            {
                string comp = db.User.Where(i => i.Name == "Andreas").Select(i => i.CarCompany.CarName).FirstOrDefault();
                ViewBag.CarCompany = comp;
            }

            //Next Version 1

            return View();
        }

    }
}