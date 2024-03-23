using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoInstructor
    {
        void SaveInstructor(Instructor instructor);
        void UpdateInstructor(Instructor instructor);
        void DeleteInstructor(Instructor instructor);
        Instructor GetInstructor(int id);
        List<Instructor> GetInstructor();
        bool ExistsInstructor(Func<Instructor, bool> filter);
        List<Instructor> GetInstructor(Func<Instructor, bool> filter);
    }
}
