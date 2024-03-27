using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;

namespace SchoolProSite.DAL.Dao
{
    public class DaoInstructor : IDaoInstructor
    {
        private readonly SchoolContext _context;

        public DaoInstructor(SchoolContext context)
        {
            this._context = context;
        }

        public void SaveInstructor(Instructor instructor)
        {
            string message = string.Empty;

            if(!IsInstructorValid(instructor, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.Instructors.Add(instructor);
            this._context.SaveChanges();
        }
        public void UpdateInstructor(Instructor instructor)
        {
            string message = string.Empty;

            if (!IsInstructorValid(instructor, ref message, Operations.Update))
                throw new DaoException(message);
            
            Instructor instructorToUpdate = this.GetInstructor(instructor.Id);

            instructorToUpdate.ModifyDate = instructor.ModifyDate;
            instructorToUpdate.LastName = instructor.LastName;
            instructorToUpdate.FirstName = instructor.FirstName;
            instructorToUpdate.HireDate = instructor.HireDate;
            instructorToUpdate.UserMod = instructor.UserMod;

            this._context.Instructors.Update(instructorToUpdate);
            this._context.SaveChanges();
        }
        public void DeleteInstructor(Instructor instructor)
        {
            Instructor instructorToRemove = this.GetInstructor(instructor.Id);

            instructorToRemove.ModifyDate = instructor.ModifyDate;
            instructorToRemove.LastName = instructor.LastName;
            instructorToRemove.FirstName = instructor.FirstName;
            instructorToRemove.UserMod = instructor.UserMod;

            this._context.Instructors.Update(instructorToRemove);
            this._context.SaveChanges();
        }
        public Instructor GetInstructor(int id)
        {
            return this._context.Instructors.Find(id);
        }
        public List<Instructor> GetInstructor()
        {
            return this._context.Instructors.ToList();
        }

        public bool ExistsInstructor(Func<Instructor, bool> filter)
        {
            return this._context.Instructors.Any(filter);
        }

        public List<Instructor> GetInstructor(Func<Instructor, bool> filter)
        {
            return this._context.Instructors.Where(filter).ToList();
        }

        private bool IsInstructorValid(Instructor instructor, ref string message, Operations operations)
        {
            bool result = false;

            if(string.IsNullOrEmpty(instructor.LastName))
            {
                message = "El primer nombre es requerido";
                return result;
            }
            if(instructor.LastName.Length > 50)
            {
                message = "El primer nombre no puede ser mayor a 50 caracteres";
                return result;
            }
            if(string.IsNullOrEmpty(instructor.FirstName))
            {
                message = "El segundo nombre es requerido";
                return result;
            }
            if(instructor.FirstName.Length > 50)
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
