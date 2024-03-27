using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Entities
{
    public partial class Student : BasePerson
    {
        public int Id { get; set; }

        public DateTime? EnrollmentDate { get; set; }
    }
}