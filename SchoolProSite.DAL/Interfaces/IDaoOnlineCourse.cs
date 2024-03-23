using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoOnlineCourse
    {
        void SaveOnlineCourse(OnlineCourse onlineCourse);
        void UpdateOnlineCourse(OnlineCourse onlineCourse);
        void DeleteOnlineCourse(OnlineCourse onlineCourse);
        OnlineCourse GetOnlineCourse(int id);
        List<OnlineCourse> GetOnlineCourse();
        bool ExistsOnlineCourse(Func<OnlineCourse, bool> filter);

        List<OnlineCourse> GetOnlineCourses(Func<OnlineCourse, bool> filter);
    }
}
