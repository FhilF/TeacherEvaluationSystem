using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EvaluationSystemProjectFHJ.Models;

namespace EvaluationSystemProjectFHJ.Controllers
{
    public class AdminsController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(string username, string password)
        {
            if (username == null && password == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            {
                return Json(db.Admins.Select(x => new
                {
                    Username = x.Username,
                    Password = x.Password,
                    Position = x.Position,
                    State = x.State,
                    Name = x.Lastname + ", " + x.Firstname,
                }).Where(x => x.Username == username).Where(x => x.Password == password).ToList(), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult LoginAdminSession(string username, string password)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Where(x => x.Username == username).Where(x => x.Password == password).FirstOrDefault<Admin>();
            if (admin == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["sessionAdminUsername"] = admin.Username;
                Session["sessionAdminPassword"] = admin.Password;
                Session["sessionAdminName"] = admin.Lastname + ", " + admin.Firstname;
                Session["sessionAdminPosition"] = admin.Position;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult LogoutSession()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Cookies.Clear();
            return RedirectToAction("Login");
        }

        // GET: Admins
        public ActionResult Index()
        {
            if (@Session["sessionAdminPosition"] != null)
            {
                var Position = @Session["sessionAdminPosition"].ToString();
                if (!Position.Equals("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return RedirectToAction("../Admins/Login");
            }
            return View(db.Admins.ToList());
        }

        // GET: Admins/Details/5
        public ActionResult Details(string id)
        {
            if (@Session["sessionAdminPosition"] != null)
            {
                var Position = @Session["sessionAdminPosition"].ToString();
                if (!Position.Equals("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return RedirectToAction("../Admins/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            if (@Session["sessionAdminPosition"] != null)
            {
                var Position = @Session["sessionAdminPosition"].ToString();
                if (!Position.Equals("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return RedirectToAction("../Admins/Login");
            }
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,Firstname,Lastname,Middlename,Position,State")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        // GET: Admins/Edit/5
        public ActionResult Edit(string id)
        {
            if (@Session["sessionAdminPosition"] != null)
            {
                var Position = @Session["sessionAdminPosition"].ToString();
                if (!Position.Equals("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return RedirectToAction("../Admins/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password,Firstname,Lastname,Middlename,Position,State")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(string id)
        {
            if (@Session["sessionAdminPosition"] != null)
            {
                var Position = @Session["sessionAdminPosition"].ToString();
                if (!Position.Equals("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return RedirectToAction("../Admins/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
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
    }
}
