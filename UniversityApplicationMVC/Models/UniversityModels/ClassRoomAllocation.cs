using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class ClassRoomAllocation
    {
        public int ClassRoomAllocationId { set; get; }

        public virtual Course Course { set; get; }
        [Required(ErrorMessage = "Select Course")]
        public int CourseId { set; get; }

        public virtual Room Room { set; get; }
        [Required(ErrorMessage = "Select Room")]
        public int RoomId { set; get; }

        public virtual Day Day { set; get; }
        [Required(ErrorMessage = "Select Day")]
        public int DayId { set; get; }

        [Display(Name = "Straring Time")]
        [Required(ErrorMessage = "Select starting time")]
        public DateTime StartingTime { set; get; }
        [Display(Name = "Ending Time")]
        [Required(ErrorMessage = "Select ending time")]
        public DateTime EndingTime { set; get; }
        
    }
}