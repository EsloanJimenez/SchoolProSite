using SchoolProSite.DAL.Core;

namespace SchoolProSite.DAL.Models
{
    public class CourseDaoModel : BaseEntities
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
