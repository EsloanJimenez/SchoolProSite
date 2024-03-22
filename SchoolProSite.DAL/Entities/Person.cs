using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Entities
{
    public partial class Person : BasePerson
    {
        public Person()
        {
            StudentGrades = new HashSet<StudentGrade>();
            Courses = new HashSet<Course>();
        }

        public int PersonId { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Discriminator { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}