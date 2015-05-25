using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using UniversityApplicationMVC.Models.DBContext;
using UniversityApplicationMVC.Models.UniversityModels;

namespace UniversityApplicationMVC.Controllers.UniversityController
{ 
    public class TeacherController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /Teacher/

        public ViewResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Designation).Include(t => t.Department);
            return View(teachers.ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ViewResult Details(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            return View(teacher);
        }

        //
        // GET: /Teacher/Create

        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            return View();
        } 

        //
        // POST: /Teacher/Create

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("MessageShow");
            }

            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            return View(teacher);
        }

        public ActionResult MessageShow()
        {
            @ViewBag.Msg = "Teacher Entry is successful.";
            return View();
        }
        //
        // GET: /Teacher/Edit/5
 
        public ActionResult Edit(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            return View(teacher);
        }

        //
        // POST: /Teacher/Edit/5

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            return View(teacher);
        }

        //
        // GET: /Teacher/Delete/5
 
        public ActionResult Delete(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            return View(teacher);
        }

        //
        // POST: /Teacher/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult TeacherEmailCheck(string email)
        {
            var isUnique = db.Teachers.Count(u => u.Email == email) == 0;
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }
    }
}