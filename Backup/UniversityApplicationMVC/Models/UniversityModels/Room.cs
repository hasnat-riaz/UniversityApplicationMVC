using System.Collections.Generic;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Room
    {
        public int RoomId { set; get; }
        public string RoomNo { set; get; }
        public virtual List<ClassRoomAllocation> ClassRoomAllocations { set; get; } 
    }
}