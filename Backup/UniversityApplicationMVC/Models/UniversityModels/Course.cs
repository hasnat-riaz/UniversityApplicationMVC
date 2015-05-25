using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Course
    {
        public int CourseId { set; get; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "Code Can't be Empty")]
        [Remote("CourseCodeCheck", "Course", ErrorMessage = "This Code already Exists")]
        public string CourseCode { set; get; }

        [Required(ErrorMessage = "Credit Can't be Empty")]
        public double Credit { set; get; }

        [Required(ErrorMessage = "Course Name Can't be Empty")]
        [Remote("CourseNameCheck", "Course", ErrorMessage = "This Code already Exists")]
        public string Name { set; get; }

        public string Description { set; get; }
        public virtual Department Department { set; get; }
        public virtual Semester Semester { set; get; }

        [Required(ErrorMessage = "Select A Department")]
        public int DepartmentId { set; get; }

        [Required(ErrorMessage = "Select A Semester")]
        public int SemesterId { set; get; }
    }
}