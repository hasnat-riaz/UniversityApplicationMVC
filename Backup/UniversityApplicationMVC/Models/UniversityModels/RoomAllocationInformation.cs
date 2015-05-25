using System.ComponentModel.DataAnnotations;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class RoomAllocationInformation
    {
        public int RoomAllocationInformationId { set; get; }

        public Department Department { set; get; }
        [Required(ErrorMessage="Select Department")]
        public int DepartmentId { set; get; }

        public string CourseCode { set; get; }
        public string Name { set; get; }
        public string ScheduleInfo { set; get; }

        public Course Course { get;private set;}
 
    }
}