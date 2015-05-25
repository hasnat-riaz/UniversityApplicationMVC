using System.Collections.Generic;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Day
    {
        public int DayId { set; get; }
        public string DayName { set; get; }
        public virtual List<ClassRoomAllocation> ClassRoomAllocations { set; get; } 
    }
}