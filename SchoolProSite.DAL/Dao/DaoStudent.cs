using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System;
using System.Linq;

namespace SchoolProSite.DAL.Dao
{
    public class DaoStudent : IDaoStudent
    {
        private readonly SchoolContext _context;

        public DaoStudent(SchoolContext context)
        {
            this._context = context;
        }

        public void SaveStudent(Student student)
        {
            string message = string.Empty;

            if (!IsStudentValid(student, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.Students.Add(student);
            this._context.SaveChanges();
        }
        public void UpdateStudent(Student student)
        {
            string message = string.Empty;

            if (!IsStudentValid(student, ref message, Operations.Update))
                throw new DaoException(message);

            Student studentToUpdated = this.GetStudent(student.Id);

            studentToUpdated.ModifyDate = student.ModifyDate;
            studentToUpdated.LastName = student.LastName;
            studentToUpdated.FirstName = student.FirstName;
            studentToUpdated.UserMod = student.UserMod;

            this._context.Students.Update(studentToUpdated);
            this._context.SaveChanges();
        }
        public void DeleteStudent(Student student)
        {
            Student studentToRemove = this.GetStudent(student.Id);

            studentToRemove.Deleted = student.Deleted;
            studentToRemove.DeletedDate = student.DeletedDate;
            studentToRemove.UserDeleted = student.UserDeleted;

            this._context.Students.Update(studentToRemove);
            this._context.SaveChanges();
        }
        public Student GetStudent(int id)
        {
            return this._context.Students.Find(id);
        }
        public List<Student> GetStudents()
        {
            return this._context.Students.ToList();
        }
        public bool ExistsStudent(Func<Student, bool> filter)
        {
            return this._context.Students.Any(filter);
        }

        public List<Student> GetStudents(Func<Student, bool> filter)
        {
            return this._context.Students.Where(filter).ToList();
        }


        private bool IsStudentValid(Student student, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(student.LastName))
            {
                message = "El primer nombre es requerido";
                return result;
            }
            if (student.LastName.Length > 50)
            {
                message = "El primer nombre no puede ser mayor a 50 caracteres";
                return result;
            }
            if (string.IsNullOrEmpty(student.FirstName))
            {
                message = "El segundo nombre es requerido";
                return result;
            }
            if (student.FirstName.Length > 50)
            {
                message = "El segundo nombre no puede ser mayor a 50 caracteres";
                return result;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
