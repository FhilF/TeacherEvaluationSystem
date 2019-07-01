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
    public class SheetsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Sheets
        public ActionResult Index()
        {
            var sheets = db.Sheets.Include(s => s.Professors).Include(s => s.Students);
            return View(sheets.ToList());
        }


        // GET: Sheets/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["sessionStudentId"] == null)
            {
                return RedirectToAction("../Students/Login");
            }
            if (id == null)
            {
                return RedirectToAction("../Students/Home");
            }
            Sheet sheet = db.Sheets.Find(id);
            if (sheet == null)
            {
                return HttpNotFound();
            }
            return View(sheet);
        }

        // GET: Sheets/Create
        public ActionResult Create()
        {
            
            if (Session["sessionStudentId"] == null)
            {
                return RedirectToAction("../Students/Login");
            }
            ViewBag.ProfessorID = db.Professors.OrderBy(p => p.departments.DepartmentName).ThenBy(p => p.Lastname).Select(p => new SelectListItem
            {
                Text = "(" + p.departments.DepartmentName + ") " + p.Lastname + ", " + p.Firstname ,
                Value = p.ProfessorID.ToString()
            });
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Lastname");
            return View();
        }

        // POST: Sheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SheetID,StudentID,ProfessorID,Subject,QuestionOne,QuestionTwo,QuestionThree,QuestionFour,QuestionFive,DateSubmitted")] Sheet sheet)
        {
            if (ModelState.IsValid)
            {
                db.Sheets.Add(sheet);
                db.SaveChanges();
                return RedirectToAction("../Students/Home");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "Lastname", sheet.ProfessorID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "Lastname", sheet.StudentID);
            return View(sheet);
        }


        // GET: Sheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["sessionStudentId"] == null)
            {
                return RedirectToAction("../Students/Login");
            }
            if (id == null)
            {
                return RedirectToAction("../Students/Home");
            }
            Sheet sheet = db.Sheets.Find(id);
            if (sheet == null)
            {
                return RedirectToAction("../Students/Home");
            }
            return View(sheet);
        }

        // POST: Sheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sheet sheet = db.Sheets.Find(id);
            db.Sheets.Remove(sheet);
            db.SaveChanges();
            return RedirectToAction("../Students/Home");
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
