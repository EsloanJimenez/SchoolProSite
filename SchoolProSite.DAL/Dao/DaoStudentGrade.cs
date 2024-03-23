using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Linq;

namespace SchoolProSite.DAL.Dao
{
    public class DaoStudentGrade : IDaoStudentGrade
    {
        private readonly SchoolContext _context;

        public DaoStudentGrade(SchoolContext context)
        {
            this._context = context;
        }

        public void SaveStudentGrade(StudentGrade studentGrade)
        {
            this._context.StudentGrades.Add(studentGrade);
            this._context.SaveChanges();
        }
        public void UpdateStudentGrade(StudentGrade studentGrade)
        {
            StudentGrade studentGradeToUpdated = this.GetStudentGrade(studentGrade.EnrollmentId);

            studentGradeToUpdated.Grade = studentGrade.Grade;

            this._context.StudentGrades.Update(studentGradeToUpdated);
            this._context.SaveChanges();
        }
        public void DeleteStudentGrade(StudentGrade studentGrade)
        {
            StudentGrade studentGradeToRemove = this.GetStudentGrade(studentGrade.EnrollmentId);

            this._context.StudentGrades.Update(studentGradeToRemove);
            this._context.SaveChanges();
        }
        public StudentGrade GetStudentGrade(int id)
        {
            return this._context.StudentGrades.Find(id);
        }
        public List<StudentGrade> GetStudentGrades()
        {
            return this._context.StudentGrades.ToList();
        }
        public bool ExistsStudentGrade(Func<StudentGrade, bool> filter)
        {
            return this._context.StudentGrades.Any(filter);
        }

        public List<StudentGrade> GetStudentGrades(Func<StudentGrade, bool> filter)
        {
            return this._context.StudentGrades.Where(filter).ToList();
        }
    }
}
