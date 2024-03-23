using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoStudentGrade
    {
        void SaveStudentGrade(StudentGrade studentGrade);
        void UpdateStudentGrade(StudentGrade studentGrade);
        void DeleteStudentGrade(StudentGrade studentGrade);
        StudentGrade GetStudentGrade(int id);
        List<StudentGrade> GetStudentGrades();
        bool ExistsStudentGrade(Func<StudentGrade, bool> filter);

        List<StudentGrade> GetStudentGrades(Func<StudentGrade, bool> filter);
    }
}
