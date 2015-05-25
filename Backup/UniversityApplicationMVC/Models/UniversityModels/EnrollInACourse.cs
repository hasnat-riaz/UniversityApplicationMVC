using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class EnrollInACourse
    {
        public int Id { set; get; }

        public virtual Student Student { set; get; }
        [Required(ErrorMessage = "Select Student")]
        public int StudentId { set; get; }

        public virtual Course Course { set; get; }
        [Required(ErrorMessage = "Select Course")]
        public int CourseId { set; get; }

        public DateTime Date { set; get; }
    }
}