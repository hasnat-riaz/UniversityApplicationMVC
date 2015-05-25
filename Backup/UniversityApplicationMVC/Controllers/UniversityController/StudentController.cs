using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityApplicationMVC.Models.DBContext;
using UniversityApplicationMVC.Models.UniversityModels;

namespace UniversityApplicationMVC.Controllers.UniversityController
{ 
    public class StudentController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /Student/

        public ViewResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            return View();
        } 

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {
            string departmentCode = db.Departments.Find(student.DepartmentId).DepartmentCode.ToString();
            string regNo = string.Format(student.Date.Year.ToString() + "-" + departmentCode + "-");
            var students = (from aStudent in db.Students
                            where (aStudent.DepartmentId ==student.DepartmentId && aStudent.Date.Year == student.Date.Year)
                            select aStudent).ToList();

            int totalNoOfStudentInDept = students.Count();
            if (totalNoOfStudentInDept < 10)
            {
                regNo += string.Format("00" +(totalNoOfStudentInDept + 1));
            }
            else if (totalNoOfStudentInDept >= 10)
            {
                regNo += string.Format("0" + (totalNoOfStudentInDept + 1));
            }
            else
            {
                regNo += string.Format(""+(totalNoOfStudentInDept + 1));
            }

            student.RegistrarionNumber = regNo;

            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                ViewBag.Message = "Mr/Mrs " + student.Name + " your registration no is: " + regNo;  
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }
        
        //
        // GET: /Student/Edit/5
 
        public ActionResult Edit(int id)
        {
            Student student = db.Students.Find(id);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }

        //
        // GET: /Student/Delete/5
 
        public ActionResult Delete(int id)
        {
            Student student = db.Students.Find(id);
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public JsonResult CheckUniquenessOfEmail(string email)
        {
            var result = db.Students.Count(d => d.Email == email) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}