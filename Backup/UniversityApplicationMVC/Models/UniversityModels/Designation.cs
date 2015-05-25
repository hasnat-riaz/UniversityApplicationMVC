using System.Collections.Generic;

namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class Designation
    {
        public int DesignationId { set; get; }
        public string DesignationName { set; get; }
        public virtual List<Teacher> Teachers { set; get; }
    }
}