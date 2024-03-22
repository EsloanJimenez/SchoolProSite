using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Entities
{
    public partial class Instructor : BasePerson
    {
        public int Id { get; set; }

        public DateTime? HireDate { get; set; }
    }
}