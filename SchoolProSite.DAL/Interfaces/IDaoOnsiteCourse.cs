using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoOnsiteCourse
    {
        void SaveOnsiteCourse(OnsiteCourse onsiteCourse);
        void UpdateOnsiteCourse(OnsiteCourse onsiteCourse);
        void DeleteOnsiteCourse(OnsiteCourse onsiteCourse);
        OnsiteCourse GetOnsiteCourse(int id);
        List<OnsiteCourse> GetOnsiteCourses();
        bool ExistsOnsiteCourse(Func<OnsiteCourse, bool> filter);

        List<OnsiteCourse> GetOnsiteCourses(Func<OnsiteCourse, bool> filter);
    }
}
