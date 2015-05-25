using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityApplicationMVC.Models.UniversityModels;
using UniversityApplicationMVC.Models.DBContext;

namespace UniversityApplicationMVC.Controllers.UniversityController
{ 
    public class EnrollInACourseController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /EnrollInACourse/

        public ViewResult Index()
        {
            var enrollinacourse = db.EnrollInACourse.Include(e => e.Student).Include(e => e.Course);
            return View(enrollinacourse.ToList());
        }

        //
        // GET: /EnrollInACourse/Details/5

        public ViewResult Details(int id)
        {
            EnrollInACourse enrollinacourse = db.EnrollInACourse.Find(id);
            return View(enrollinacourse);
        }

        //
        // GET: /EnrollInACourse/Create

        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrarionNumber");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            return View();
        } 

        //
        // POST: /EnrollInACourse/Create

        [HttpPost]
        public ActionResult Create(EnrollInACourse enrollinacourse)
        {
            var enrollInACourse =
                from aCourse in db.EnrollInACourse
                where (aCourse.CourseId == enrollinacourse.CourseId && aCourse.StudentId == enrollinacourse.StudentId)
                select aCourse;

                
            if (enrollInACourse.Count() != 0 )
            {
                @ViewBag.Message = "This Course is already taken";
            }

            else if (ModelState.IsValid)
            {
                db.EnrollInACourse.Add(enrollinacourse);
                db.SaveChanges();
                return RedirectToAction("MessageShow");  
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "RegistrarionNumber", enrollinacourse.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", enrollinacourse.CourseId);
            return View(enrollinacourse);
        }

        public ActionResult MessageShow()
        {
            @ViewBag.Msg = "Course is Enrolled Successfully";
            return View();
        }
        //
        // GET: /EnrollInACourse/Edit/5
 
        public ActionResult Edit(int id)
        {
            EnrollInACourse enrollinacourse = db.EnrollInACourse.Find(id);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollinacourse.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", enrollinacourse.CourseId);
            return View(enrollinacourse);
        }

        //
        // POST: /EnrollInACourse/Edit/5

        [HttpPost]
        public ActionResult Edit(EnrollInACourse enrollinacourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollinacourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", enrollinacourse.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", enrollinacourse.CourseId);
            return View(enrollinacourse);
        }

        //
        // GET: /EnrollInACourse/Delete/5
 
        public ActionResult Delete(int id)
        {
            EnrollInACourse enrollinacourse = db.EnrollInACourse.Find(id);
            return View(enrollinacourse);
        }

        //
        // POST: /EnrollInACourse/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            EnrollInACourse enrollinacourse = db.EnrollInACourse.Find(id);
            db.EnrollInACourse.Remove(enrollinacourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}