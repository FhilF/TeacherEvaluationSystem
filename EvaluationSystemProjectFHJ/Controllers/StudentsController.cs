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
    public class StudentsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Courses);
            return View(students.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(db.Students.Select(x => new
                {
                    StudentId = x.StudentID,
                    StudentName = x.Lastname + ", " + x.Firstname,
                    StudentCourse = x.Courses.CourseCode,
                }).Where(x => x.StudentId == id).ToList(), JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public ActionResult LoginSession(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {

                Session["sessionStudentId"] = student.StudentID;
                Session["sessionStudentName"] = student.Lastname + ", " + student.Firstname;
                Session["sessionStudentCourse"] = student.Courses.CourseCode;
                return Json(student.Firstname,JsonRequestBehavior.AllowGet);
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


        public ActionResult Home()
        {
            if (Session["sessionStudentId"] == null)
            {
                return RedirectToAction("Login");
            }
            var id = @Session["sessionStudentId"].ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sheets = db.Sheets.Include(s => s.Professors).Include(s => s.Students).Where(x => x.StudentID == id);
            if (sheets == null)
            {
                return HttpNotFound();
            }
            return View(sheets);

        }


        public ActionResult ViewStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sheets = db.Sheets.Include(s => s.Professors).Include(s => s.Students).Where(x => x.StudentID == id);
            ViewBag.StudentName = db.Students.Find(id).Lastname + ", " + db.Students.Find(id).Firstname;
            ViewBag.StudentId = db.Students.Find(id).StudentID;
            ViewBag.StudentCourse = db.Students.Find(id).Courses.CourseCode;

            if (sheets == null)
            {
                return HttpNotFound();
            }
            return View(sheets);

        }

        public ActionResult CheckStudent(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(db.Students.Select(x => new
            {
                StudentID = x.StudentID
            }).Where(x => x.StudentID == id), JsonRequestBehavior.AllowGet);
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseCode");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Lastname,Firstname,Middlename,CourseID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseCode", student.CourseID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseCode", student.CourseID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Lastname,Firstname,Middlename,CourseID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseCode", student.CourseID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            
            db.Sheets.RemoveRange(db.Sheets.Where(x => x.StudentID == id));
            db.SaveChanges();
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteRecord()
        {

            db.Sheets.RemoveRange(db.Sheets);
            db.SaveChanges();
            db.Students.RemoveRange(db.Students);
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
