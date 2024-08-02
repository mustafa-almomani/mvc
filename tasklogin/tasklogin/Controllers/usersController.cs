using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tasklogin.Models;

namespace tasklogin.Controllers
{
    public class usersController : Controller
    {
        private loginEntities db = new loginEntities();

        // GET: users
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fname,lname,email,password,Image")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: users/Edit/5
        public ActionResult Edit(int? id , string image)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["UserImage1"] = image;
            user user = db.users.Find(Session["UserID"]);
            //Session["UserImage"] = user.Image;
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(user user, HttpPostedFileBase upload)
        {
          string x =  Session["UserImage1"].ToString();


            if (!Directory.Exists(Server.MapPath("~/Images/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Images/"));
            }

            if (upload != null && upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);

                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);


                if (!Directory.Exists(Server.MapPath("~/Images/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Images/"));
                }


                upload.SaveAs(path);
                user.Image = fileName;
            }
            else if(upload == null)
            {
                var fileName = Path.GetFileName(x);

              //  user.Image = Session["UserImage"] as string;
                user.Image = fileName;
            }

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        // GET: users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult login()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(string email, string password)
        {
            if (ModelState.IsValid)
            {

                var user = db.users.SingleOrDefault(u => u.email == email && u.password == password);
                if (user != null)
                {
                    Session["UserID"] = user.id;
                    Session["UserName"] = user.fname;
                    Session["UserImage"] = user.Image;

                    return RedirectToAction("Home");
                }
                else
                {
                    // Invalid login attempt
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View();
        }

        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult signup([Bind(Include = "id,fname,lname,email,password,Image")] user user, HttpPostedFileBase upload)
        {
            if (!Directory.Exists(Server.MapPath("~/Images/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Images/"));
            }

            if (upload != null && upload.ContentLength > 0)
            {
                var fileName = Path.GetFileName(upload.FileName);

                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);


                if (!Directory.Exists(Server.MapPath("~/Images/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Images/"));
                }


                upload.SaveAs(path);
                user.Image = fileName;
            }

            db.users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            int? userId = Session["UserID"] as int?;
            if (userId == null)
            {
                return RedirectToAction("login");
            }

            user user = db.users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult Logout()
        {
  
            Session.Clear();
            return RedirectToAction("login");
        }
    }
}
