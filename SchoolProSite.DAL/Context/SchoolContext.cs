using Microsoft.EntityFrameworkCore;
using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Context
{
    public class SchoolContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        #region "Entities"
        public DbSet<Course> Course { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<OnlineCourse> OnlineCourse { get; set; }
        public DbSet<OnsiteCourse> OnsiteCourse { get; set;}
        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentGrade> StudentGrades { get;set; }
        #endregion
    }
}
