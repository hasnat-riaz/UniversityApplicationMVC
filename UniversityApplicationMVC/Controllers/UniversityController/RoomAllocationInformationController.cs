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
    public class RoomAllocationInformationController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /RoomAllocationInformation/
        //TODO performing testing case here
        public void SaveAllToDb()
        {
            //from aRoomAllocationInformatio in db.RoomAllocationInformations 
            List<RoomAllocationInformation> allRoomAllocationInformations =db.RoomAllocationInformations.ToList();
            foreach (RoomAllocationInformation aRoomAllocationInformation in allRoomAllocationInformations)
            {
                db.RoomAllocationInformations.Remove(aRoomAllocationInformation);
                db.SaveChanges();
            }
            
            List<Course> courses = db.Courses.ToList();
            List<ClassRoomAllocation> classRoomAllocations = db.ClassRoomAllocations.ToList();
            RoomAllocationInformation roomAllocationInformation = new RoomAllocationInformation();
            foreach (Course aCourse in courses)
            {
                roomAllocationInformation.DepartmentId = aCourse.DepartmentId;
                roomAllocationInformation.CourseCode = aCourse.CourseCode;
                roomAllocationInformation.Name = aCourse.Name;
                string scheduleInfo = string.Empty;
                foreach (ClassRoomAllocation aClassRoomAllocation in classRoomAllocations)
                {
                    if (aCourse.CourseId == aClassRoomAllocation.CourseId)
                    {
                        scheduleInfo += String.Format("Room No:{0}, {1}, {2} to {3} ; ",
                            aClassRoomAllocation.Room.RoomNo, aClassRoomAllocation.Day.DayName , 
                            aClassRoomAllocation.StartingTime.TimeOfDay , aClassRoomAllocation.EndingTime.TimeOfDay);
                    }
                }
                if (scheduleInfo == string.Empty)
                {
                    scheduleInfo = "Not set yet";
                }
                roomAllocationInformation.ScheduleInfo = scheduleInfo;
                if (ModelState.IsValid)
                {
                    db.RoomAllocationInformations.Add(roomAllocationInformation);
                    db.SaveChanges();
                }
            }
            
        }

        public ViewResult Index(int? departmentId)
        {
            SaveAllToDb();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            if (departmentId != null)
            {
                return View(db.RoomAllocationInformations.Include(r => r.Department).Where(s => s.Department.DepartmentId == departmentId));
            }
            else
            {
                return View(db.RoomAllocationInformations.Include(r => r.Department));
            }
            //var roomallocationinformations = db.RoomAllocationInformations.Include(r => r.Department);
            //return View(roomallocationinformations.ToList());
        }

        public PartialViewResult FilteredRoomAllocationInfo(int? departmentId)
        {
            if (departmentId != null)
            {
                var model = db.RoomAllocationInformations.Include(r => r.Department).Where(r => r.Department.DepartmentId == departmentId);
                return PartialView("~/Views/Shared/_FilteredRoomAllocationInfo.cshtml", model);
            }
            else
            {
                return PartialView("~/Views/Shared/_FilteredRoomAllocationInfo.cshtml", db.RoomAllocationInformations.Include(r => r.Department));
            }
        }
        //
        // GET: /RoomAllocationInformation/Details/5

        public ViewResult Details(int id)
        {
            RoomAllocationInformation roomallocationinformation = db.RoomAllocationInformations.Find(id);
            return View(roomallocationinformation);
        }

        //
        // GET: /RoomAllocationInformation/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            return View();
        } 

        //
        // POST: /RoomAllocationInformation/Create

        [HttpPost]
        public ActionResult Create(RoomAllocationInformation roomallocationinformation)
        {
            if (ModelState.IsValid)
            {
                db.RoomAllocationInformations.Add(roomallocationinformation);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", roomallocationinformation.DepartmentId);
            return View(roomallocationinformation);
        }
        
        //
        // GET: /RoomAllocationInformation/Edit/5
 
        public ActionResult Edit(int id)
        {
            RoomAllocationInformation roomallocationinformation = db.RoomAllocationInformations.Find(id);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", roomallocationinformation.DepartmentId);
            return View(roomallocationinformation);
        }

        //
        // POST: /RoomAllocationInformation/Edit/5

        [HttpPost]
        public ActionResult Edit(RoomAllocationInformation roomallocationinformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomallocationinformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", roomallocationinformation.DepartmentId);
            return View(roomallocationinformation);
        }

        //
        // GET: /RoomAllocationInformation/Delete/5
 
        public ActionResult Delete(int id)
        {
            RoomAllocationInformation roomallocationinformation = db.RoomAllocationInformations.Find(id);
            return View(roomallocationinformation);
        }

        //
        // POST: /RoomAllocationInformation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            RoomAllocationInformation roomallocationinformation = db.RoomAllocationInformations.Find(id);
            db.RoomAllocationInformations.Remove(roomallocationinformation);
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