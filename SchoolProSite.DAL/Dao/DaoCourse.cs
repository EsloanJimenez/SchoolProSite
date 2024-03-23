using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;

namespace SchoolProSite.DAL.Dao
{
    public class DaoCourse : IDaoCourse
    {
        private readonly SchoolContext _context;
        public DaoCourse(SchoolContext context)
        {
            this._context = context;
        }
        public void SaveCourse(Course course)
        {
            string message = string.Empty;

            if (!IsCourseValid(course, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.Courses.Add(course);
            this._context.SaveChanges();
        }
        public void UpdateCourse(Course course)
        {
            string message = string.Empty;

            if (!IsCourseValid(course, ref message, Operations.Update))
                throw new DaoException(message);

            Course courseToUpdated = this.GetCourse(course.CourseId);

            courseToUpdated.ModifyDate = course.ModifyDate;
            courseToUpdated.Title = course.Title;
            courseToUpdated.Credits = course.Credits;
            courseToUpdated.UserMod = course.UserMod;

            this._context.Courses.Update(courseToUpdated);
            this._context.SaveChanges();
        }
        public void DeleteCourse(Course course)
        {
            Course courseToRemove = this.GetCourse(course.CourseId);

            courseToRemove.Deleted = course.Deleted;
            courseToRemove.DeletedDate = course.DeletedDate;
            courseToRemove.UserDeleted = course.UserDeleted;

            this._context.Courses.Update(courseToRemove);
            this._context.SaveChanges();
        }
        public Course GetCourse(int id)
        {
            return this._context.Courses.Find(id);
        }
        public List<Course> GetCourses()
        {
            return this._context.Courses.ToList();
        }
        public bool ExistsCourse(Func<Course, bool> filter)
        {
            return this._context.Courses.Any(filter);
        }

        public List<Course> GetCourses(Func<Course, bool> filter)
        {
            return this._context.Courses.Where(filter).ToList();
        }


        private bool IsCourseValid(Course course, ref string message, Operations operations)
        {
            bool result = false;

            if(string.IsNullOrEmpty(course.Title))
            {
                message = "El titulo del curso requerido";
                return result;
            }
            if(course.Title.Length > 100)
            {
                message = "El titulo del curso no puede ser mayor a 100 caracteres";
                return result;
            }
            if(course.Credits == 0)
            {
                message = "El credito no puede ser cero(0)";
                return result;
            }
            if (operations == Operations.Save)
            {
                if(this.ExistsCourse(cd => cd.Title == course.Title))
                {
                    message = "El curso ya se encuentra registrado";
                    return result;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

    }
}
