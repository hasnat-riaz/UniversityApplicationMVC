using System.Data.Entity;
using UniversityApplicationMVC.Models.UniversityModels;

namespace UniversityApplicationMVC.Models.DBContext
{
    public class UniversityDBContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Day> Days { get; set; }

        public DbSet<ClassRoomAllocation> ClassRoomAllocations { get; set; }
        public DbSet<RoomAllocationInformation> RoomAllocationInformations { get; set; }
        public DbSet<EnrollInACourse> EnrollInACourse { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrollInACourse>()
                        .HasRequired(a => a.Course)
                        .WithMany()
                        .HasForeignKey(u => u.CourseId).WillCascadeOnDelete(false);

            modelBuilder.Entity<EnrollInACourse>()
                        .HasRequired(a => a.Student)
                        .WithMany()
                        .HasForeignKey(u => u.StudentId).WillCascadeOnDelete(false);
        }
    }
}