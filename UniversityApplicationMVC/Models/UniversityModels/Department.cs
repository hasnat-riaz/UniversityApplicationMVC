using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Department
    {   
        public int DepartmentId { set; get; }

        [Required(ErrorMessage = "Department Code Can't be Empty")]
        [Display(Name = "Department Code")]
        [Remote("DepartmentCodeCheck", "Department", ErrorMessage = "This Code already Exists")]
        public string DepartmentCode { set; get; }

        [Required(ErrorMessage = "Department Name Can't be Empty")]
        [Display(Name = "Department Name")]
        [Remote("DepartmentNameCheck", "Department", ErrorMessage = "This Name already Exists")]
        public string DepartmentName { set; get; }
        
    }
}