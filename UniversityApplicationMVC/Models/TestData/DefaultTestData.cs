using System.Collections.Generic;
using System.Data.Entity;
using UniversityApplicationMVC.Models.DBContext;
using UniversityApplicationMVC.Models.UniversityModels;

namespace UniversityApplicationMVC.Models.TestData
{
    public class DefaultTestData : DropCreateDatabaseIfModelChanges<UniversityDBContext>
    {
        protected override void Seed(UniversityDBContext context)
        {
            new List<Department>
                {
                    new Department {DepartmentCode = "CSE", DepartmentName = "Computer Science & Engineering"},
                    new Department {DepartmentCode = "EEE", DepartmentName = "Electrical & Electronics Engnnering"},
                    new Department {DepartmentCode = "BBA", DepartmentName = "Besiness Adminestration"},
                    new Department {DepartmentCode = "CSTE", DepartmentName = "Computer Science and Telecommunication Engineering"}
                }.ForEach(department => context.Departments.Add(department));

            new List<Semester>
                {
                    new Semester {SemesterNumber = "1st"},
                    new Semester {SemesterNumber = "2nd"},
                    new Semester {SemesterNumber = "3rd"},
                    new Semester {SemesterNumber = "4th"},
                    new Semester {SemesterNumber = "5th"},
                    new Semester {SemesterNumber = "6th"},
                    new Semester {SemesterNumber = "7th"},
                    new Semester {SemesterNumber = "8th"}
                }.ForEach(semester => context.Semesters.Add(semester));

            new List<Designation>
                {
                    new Designation {DesignationName = "Lecturer"},
                    new Designation {DesignationName = "Assistent Professor"},
                    new Designation {DesignationName = "Associate Professor"},
                    new Designation {DesignationName = "Professor"}
                }.ForEach(designation => context.Designations.Add(designation));

            new List<Day>
                {
                    new Day() {DayName = "Saturday"},
                    new Day() {DayName = "Sunday"},
                    new Day() {DayName = "Monday"},
                    new Day() {DayName = "Tuesday"},
                    new Day() {DayName = "Wednesday"},
                    new Day() {DayName = "Thursday"},
                    new Day() {DayName = "Friday"}
                }.ForEach(day => context.Days.Add(day));

            new List<Room>
                {
                    new Room() {RoomNo = "A-101"},
                    new Room() {RoomNo = "A-102"},
                    new Room() {RoomNo = "B-201"},
                    new Room() {RoomNo = "B-202"},
                    new Room() {RoomNo = "C-301"},
                    new Room() {RoomNo = "C-302"}
                }.ForEach(room => context.Rooms.Add(room));

            //new List<Course>
            //    {
            //        new Course() {CourseCode = "CSTE 4103",Credit = "3.0",Name = "Cryptography",Description = "Cryptography and Network Security",DepartmentId = 4,SemesterId = 7},
            //        new Course() {CourseCode = "CSTE 3205",Credit = "3.0",Name = "AI",Description = "Artificial Intelligence",DepartmentId = 4,SemesterId = 6},
            //        new Course() {CourseCode = "CSTE 4107",Credit = "3.0",Name = "Microwave",Description = "Microwave and Satellite Communication",DepartmentId = 4,SemesterId = 7},
            //    }.ForEach(course => context.Courses.Add(course));
        }
    }
}