using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test2.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult contactus(FormCollection form)
        {

            return View();
        }
        [HttpPost]
        public ActionResult ShowInformation(FormCollection form)
        {
            ViewBag.email = form["email"];
            ViewBag.password = form["password"];
            ViewBag.address = form["address"];
            ViewBag.address2 = form["address2"];
            ViewBag.city = form["city"];
            ViewBag.zip = form["zip"];
            ViewBag.select = form["select"];
            ViewBag.check = form["check"];



            return View();
        }
        public ActionResult home()
        {
            return View();
        }
        public ActionResult serves()
        {
            return View();
        }
    }
}