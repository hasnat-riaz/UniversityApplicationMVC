using System.Data;
using System.Linq;
using System.Web.Mvc;
using UniversityApplicationMVC.Models.DBContext;
using UniversityApplicationMVC.Models.UniversityModels;

namespace UniversityApplicationMVC.Controllers.UniversityController
{ 
    public class DepartmentController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /Department/

        public ViewResult Index()
        {
            return View(db.Departments.ToList());
        }

        //
        // GET: /Department/Details/5

        public ViewResult Details(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("MessageShow");
            }

            return View(department);
        }

        public ActionResult MessageShow()
        {
            @ViewBag.Msg = "This Department Is Saved Successfully";
            return View();
        }
        //
        // GET: /Department/Edit/5
 
        public ActionResult Edit(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        //
        // GET: /Department/Delete/5
 
        public ActionResult Delete(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult DepartmentCodeCheck(string departmentcode)
        {
            var isUnique = db.Departments.Count(u => u.DepartmentCode == departmentcode) == 0;
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DepartmentNameCheck(string departmentname)
        {
            var isUnique = db.Departments.Count(u => u.DepartmentName == departmentname) == 0;
            return Json(isUnique, JsonRequestBehavior.AllowGet);
        }
    }
}