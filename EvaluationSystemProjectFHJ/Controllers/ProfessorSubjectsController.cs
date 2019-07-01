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
    public class ProfessorSubjectsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: ProfessorSubjects
        public ActionResult Index()
        {
            var professorSubjects = db.ProfessorSubjects.Include(p => p.Professors).Include(p => p.Subjects);
            return View(professorSubjects.ToList());
        }

        // GET: ProfessorSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorSubject professorSubject = db.ProfessorSubjects.Find(id);
            if (professorSubject == null)
            {
                return HttpNotFound();
            }
            return View(professorSubject);
        }

        public ActionResult getSubjectByProfessor(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return Json(db.ProfessorSubjects.Include(x => x.Subjects).OrderBy(x => x.Subjects.SubjectName).Select(x => new
            {
                ProfessorSubjectsID = x.ProfessorSubjectID,
                ProfessorSubjects = x.Subjects.SubjectName,
                ProfessorID = x.ProfessorID,
                ProfessorSubjectCode = x.Subjects.SubjectCode
            }).Where(x => x.ProfessorID == id), JsonRequestBehavior.AllowGet);
        }

        // GET: ProfessorSubjects/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorID = db.Professors.Select(p => new SelectListItem
            {
                Text = "(" +p.departments.DepartmentName +") "+ p.Lastname + ", " + p.Firstname,
                Value = p.ProfessorID.ToString()
            });
            ViewBag.SubjectID = db.Subjects.Select(p => new SelectListItem
            {
                Text = p.SubjectCode + " - " + p.SubjectName,
                Value = p.SubjectID.ToString()
            });
            return View();
        }

        // POST: ProfessorSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfessorSubjectID,ProfessorID,SubjectID")] ProfessorSubject professorSubject)
        {
            if (ModelState.IsValid)
            {
                db.ProfessorSubjects.Add(professorSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "Lastname", professorSubject.ProfessorID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectCode", professorSubject.SubjectID);
            return View(professorSubject);
        }

        // GET: ProfessorSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorSubject professorSubject = db.ProfessorSubjects.Find(id);
            if (professorSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "Lastname", professorSubject.ProfessorID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectCode", professorSubject.SubjectID);
            return View(professorSubject);
        }

        // POST: ProfessorSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfessorSubjectID,ProfessorID,SubjectID")] ProfessorSubject professorSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professorSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "Lastname", professorSubject.ProfessorID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectCode", professorSubject.SubjectID);
            return View(professorSubject);
        }

        // GET: ProfessorSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessorSubject professorSubject = db.ProfessorSubjects.Find(id);
            if (professorSubject == null)
            {
                return HttpNotFound();
            }
            return View(professorSubject);
        }

        // POST: ProfessorSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfessorSubject professorSubject = db.ProfessorSubjects.Find(id);
            db.ProfessorSubjects.Remove(professorSubject);
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
