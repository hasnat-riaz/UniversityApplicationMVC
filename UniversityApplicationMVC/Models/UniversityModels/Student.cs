using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Student
    {
        public int StudentId { set; get; }

        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { set; get; }

        public string RegistrarionNumber { set; get; }
        
        [Required(ErrorMessage = "Email can't be empty")]
        [Remote("CheckUniquenessOfEmail", "Student", ErrorMessage = @"Sorry this Email exists in your system.")]
        public string Email  {set; get; }
        
        [Display(Name = "Contact No")]
        public string ContactNo{ set; get; }
        
        [Required(ErrorMessage = "Date can't be empty")]
        public System.DateTime Date { set; get; }

        [Required(ErrorMessage = "Address can't be empty")]
        public string Address { set; get; }
        
        public virtual Department Department { set; get; }
        
        [Required(ErrorMessage = "Depart't ment can't be empty")]
        public int DepartmentId { set; get; }
        
    }
}