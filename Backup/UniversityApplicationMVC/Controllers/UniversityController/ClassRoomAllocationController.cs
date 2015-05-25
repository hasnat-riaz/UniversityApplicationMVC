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
    public class ClassRoomAllocationController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        //
        // GET: /ClassRoomAllocation/

        public ViewResult Index()
        {
            var classroomallocations = db.ClassRoomAllocations.Include(c => c.Course).Include(c => c.Room).Include(c => c.Day);
            return View(classroomallocations.ToList());
        }

        //
        // GET: /ClassRoomAllocation/Details/5

        public ViewResult Details(int id)
        {
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            return View(classroomallocation);
        }

        //
        // GET: /ClassRoomAllocation/Create

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo");
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName");
            return View();
        } 

        //
        // POST: /ClassRoomAllocation/Create

        [HttpPost]
        public ActionResult Create(ClassRoomAllocation classroomallocation)
        {
            double startingHourAndMinute = double.Parse(string.Format(classroomallocation.StartingTime.Hour.ToString()+"."+classroomallocation.StartingTime.Minute.ToString()));
            double endingHourAndMinute = double.Parse(string.Format(classroomallocation.EndingTime.Hour.ToString() + "." + classroomallocation.EndingTime.Minute.ToString()));

            List<ClassRoomAllocation> classRoomAllocations = (from aSchedule in db.ClassRoomAllocations
                                        where (aSchedule.DayId == classroomallocation.DayId && aSchedule.RoomId==classroomallocation.RoomId)
                                        select aSchedule).ToList();
            if (classRoomAllocations.Count != 0)
            {
                foreach (var aClassRoomAllocation in classRoomAllocations)
                {
                    double startingHourAndMinuteInDb = double.Parse(string.Format(aClassRoomAllocation.StartingTime.Hour.ToString() + "." + aClassRoomAllocation.StartingTime.Minute.ToString()));
                    double endingHourAndMinuteInDb = double.Parse(string.Format(aClassRoomAllocation.EndingTime.Hour.ToString() + "." + aClassRoomAllocation.EndingTime.Minute.ToString()));
                    if (endingHourAndMinute <= startingHourAndMinuteInDb)
                    {
                        if (ModelState.IsValid)
                        {
                            db.ClassRoomAllocations.Add(classroomallocation);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else if (startingHourAndMinute >= endingHourAndMinuteInDb)
                    {
                        if (ModelState.IsValid)
                        {
                            db.ClassRoomAllocations.Add(classroomallocation);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        //string msg = string.Format("Sorry this time is overlapping with course : "+aClassRoomAllocation.Course.CourseCode);
                        ViewBag.Message = "Sorry this time is overlapping with course : " + aClassRoomAllocation.Course.CourseCode+" ("
                            +aClassRoomAllocation.StartingTime.Hour.ToString() + ":" + aClassRoomAllocation.StartingTime.Minute.ToString()
                            +" to "+ aClassRoomAllocation.EndingTime.Hour.ToString() + ":" + aClassRoomAllocation.EndingTime.Minute.ToString()+")";
                    }
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.ClassRoomAllocations.Add(classroomallocation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", classroomallocation.CourseId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", classroomallocation.RoomId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", classroomallocation.DayId);
            return View(classroomallocation);
        }
        
        //
        // GET: /ClassRoomAllocation/Edit/5
 
        public ActionResult Edit(int id)
        {
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", classroomallocation.CourseId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", classroomallocation.RoomId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", classroomallocation.DayId);
            return View(classroomallocation);
        }

        //
        // POST: /ClassRoomAllocation/Edit/5

        [HttpPost]
        public ActionResult Edit(ClassRoomAllocation classroomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classroomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", classroomallocation.CourseId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", classroomallocation.RoomId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", classroomallocation.DayId);
            return View(classroomallocation);
        }

        //
        // GET: /ClassRoomAllocation/Delete/5
 
        public ActionResult Delete(int id)
        {
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            return View(classroomallocation);
        }

        //
        // POST: /ClassRoomAllocation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ClassRoomAllocation classroomallocation = db.ClassRoomAllocations.Find(id);
            db.ClassRoomAllocations.Remove(classroomallocation);
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