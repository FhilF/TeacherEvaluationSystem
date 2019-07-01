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
    public class ProfessorsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Professors
        public ActionResult Index()
        {
            if(@Session["sessionAdminPosition"] != null)
            {
                var Position = @Session["sessionAdminPosition"].ToString();
                if (!Position.Equals("Admin"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return RedirectToAction("~/Admins/Login");
            }
            

            var professors = db.Professors.Include(p => p.departments);
            return View(professors.ToList());
        }

        public ActionResult ViewResult(int? id)
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

            var sheets = db.Sheets.Include(s => s.Professors).Include(s => s.Students).Where(x => x.ProfessorID == id);
            ViewBag.ProfessorName = db.Professors.Find(id).Lastname + ", " + db.Professors.Find(id).Firstname;

            if (sheets == null)
            {
                return HttpNotFound();
            }
            return View(sheets);

        }

        public ActionResult EnrolledSubjects(int? id)
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
                return RedirectToAction("~/Admins/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var professors = db.ProfessorSubjects.Include(s => s.Subjects).Where(x => x.ProfessorID == id);
            ViewBag.ProfessorName = db.Professors.Find(id).Lastname + ", " + db.Professors.Find(id).Firstname;
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
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
                return RedirectToAction("~/Admins/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professors/Create
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
                return RedirectToAction("~/Admins/Login");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfessorID,Lastname,Firstname,Middlename,DepartmentID")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", professor.DepartmentID);
            return View(professor);
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(int? id)
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
                return RedirectToAction("~/Admins/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", professor.DepartmentID);
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfessorID,Lastname,Firstname,Middlename,DepartmentID")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "DepartmentName", professor.DepartmentID);
            return View(professor);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            var Position = @Session["sessionAdminPosition"].ToString();
            if (!Position.Equals("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Sheets.RemoveRange(db.Sheets.Where(x => x.ProfessorID == id));
            db.SaveChanges();
            db.ProfessorSubjects.RemoveRange(db.ProfessorSubjects.Where(s => s.ProfessorID == id));
            db.SaveChanges();
            Professor professor = db.Professors.Find(id);
            db.Professors.Remove(professor);
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
