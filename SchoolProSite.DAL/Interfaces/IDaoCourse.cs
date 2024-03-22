using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoCourse
    {
        void SaveCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        Course GetCourse(int id);
        List<Course> GetCourses();
        bool ExistsCourse(Func<Course, bool> filter);
        List<Course> GetCourses(Func<Course, bool> filter);
    }
}
