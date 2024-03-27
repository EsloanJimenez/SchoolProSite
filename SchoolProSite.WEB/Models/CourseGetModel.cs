using SchoolProSite.DAL.Models;

namespace SchoolProSite.WEB.Models
{
    public class CourseGetModel
    {
        public CourseGetModel()
        {
            
        }
        public CourseGetModel(CourseDaoModel courseDaoModel)
        {
            this.CourseId = courseDaoModel.CourseId;
            this.Title = courseDaoModel.Title;
            this.Credits = courseDaoModel.Credits;
            this.DepartmentId = courseDaoModel.DepartmentId;
            this.DepartmentName = courseDaoModel.DepartmentName;
            this.CreateDate = courseDaoModel.CreationDate;
        }
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
