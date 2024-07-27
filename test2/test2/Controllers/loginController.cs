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
        public ActionResult Index (FormCollection form)
        {
            Session["login"]= "ابو شهاب";
            string email = form["email"];
            string password = form["password"];
            if (email =="mustafa@gmail.com" && password== "123456")
            {
                Session["name"] = "0";
                return View("home");
            }

            return View();
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult contactus(FormCollection form)
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
        [HttpPost]
        public ActionResult ShowInformation(FormCollection form)
        {
            //ViewBag.email = form["email"];
            //ViewBag.password = form["password"];
            //ViewBag.address = form["address"];
            //ViewBag.address2 = form["address2"];
            //ViewBag.city = form["city"];
            //ViewBag.zip = form["zip"];
            //ViewBag.select = form["select"];
            //ViewBag.check = form["check"];



            return View();
        }
        public ActionResult home()
        {
            Session["name"] = "1";
            return View();
        }
        public ActionResult serves()
        {
            return View();
        }
      
    }
}