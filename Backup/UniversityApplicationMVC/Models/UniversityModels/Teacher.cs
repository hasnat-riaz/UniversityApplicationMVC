using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Teacher
    {
        public int TeacherId { set; get; }

        [Required(ErrorMessage = "Name Can't be Empty")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Address Can't be Empty")]
        public string Address { set; get; }

        [Required(ErrorMessage = "Email Can't be Empty")]
        [Remote("TeacherEmailCheck", "Teacher", ErrorMessage = "Email already Exists")]
        public string Email { set; get; }

        [Display(Name = "Contact No")]
        public string ContactNo { set; get; }

        public virtual Designation Designation { set; get; }
        public virtual Department Department { set; get; }

        [Required(ErrorMessage = "Select A Department")]
        public int DepartmentId { set; get; }

        [Required(ErrorMessage = "Select A Designation")]
        public int DesignationId { set; get; }

        [Display(Name = "Credit To Be Taken")]
        public double CreditToBeTaken { set; get; }

    }
}