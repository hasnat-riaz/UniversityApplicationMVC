using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using UniversityApplicationMVC.Models.DBContext;
using UniversityApplicationMVC.Models.UniversityModels;

namespace UniversityApplicationMVC.Controllers.UniversityController
{ 
    public class CourseController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /Course/

        public ViewResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }

        //
        // GET: /Course/Details/5

        public ViewResult Details(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterNumber");
            return View();
        } 

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("MessageShow");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterNumber", course.SemesterId);
            return View(course);
        }

        public ActionResult MessageShow()
        {
            @ViewBag.Msg = "Course Is Saved Successfully";
            return View();
        }
        
        //
        // GET: /Course/Edit/5
 
        public ActionResult Edit(int id)
        {
            Course course = db.Courses.Find(id);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterNumber", course.SemesterId);
            return View(course);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterNumber", course.SemesterId);
            return View(course);
        }

        //
        // GET: /Course/Delete/5
 
        public ActionResult Delete(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult CourseCodeCheck(string coursecode)
        {
            var isUnique = db.Courses.Count(u => u.CourseCode == coursecode) == 0;
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CourseNameCheck(string name)
        {
            var isUnique = db.Courses.Count(u => u.Name == name) == 0;
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }
    }
}