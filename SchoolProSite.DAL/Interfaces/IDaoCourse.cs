using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Models;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoCourse
    {
        void SaveCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        CourseDaoModel GetCourse(int Id);
        List<CourseDaoModel> GetCourses();
        List<CourseDaoModel> GetCourses(Func<Course, bool> filter);
        bool ExistsCourse(Func<Course, bool> filter);
    }
}
