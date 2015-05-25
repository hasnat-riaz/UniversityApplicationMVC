namespace UniversityApplicationMVC.Models.UniversityModels
{
    public class CourseAssignToTeacher
    {
        public int DepartmentId { set; get; }
        public Department Department { set; get; }

        public int TeacherId { set; get; }
        public Teacher Teacher { set; get; }

        public int CourseId { set; get; }
        public Course Course { set; get; }
    }
}