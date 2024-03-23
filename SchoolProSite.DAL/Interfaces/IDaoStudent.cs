using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoStudent
    {
        void SaveStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        Student GetStudent(int id);
        List<Student> GetStudents();
        bool ExistsStudent(Func<Student, bool> filter);

        List<Student> GetStudents(Func<Student, bool> filter);
    }
}
