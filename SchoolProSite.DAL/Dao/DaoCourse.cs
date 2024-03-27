using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.DAL.Models;

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
            try
            {
                string message = string.Empty;

                if (!IsCourseValid(course, ref message, Operations.Save))
                    throw new DaoCourseException(message);

                this._context.Course.Add(course);
                this._context.SaveChanges();
            } catch(Exception ex)
            {
                throw new DaoCourseException(ex.Message);
            }
        }
        public void UpdateCourse(Course course)
        {
            try
            {
                string message = string.Empty;

                if (!IsCourseValid(course, ref message, Operations.Update))
                    throw new DaoCourseException(message);

                Course? courseToUpdated = this._context.Course.Find(course.CourseId);

                if (course is null)
                    throw new DaoCourseException("No se encontro el curso especificado");


                courseToUpdated.ModifyDate = DateTime.Now;
                courseToUpdated.Title = course.Title;
                courseToUpdated.Credits = course.Credits;
                courseToUpdated.DepartmentId = course.DepartmentId;
                courseToUpdated.UserMod = course.UserMod;

                this._context.Course.Update(courseToUpdated);
                this._context.SaveChanges();
            } catch(Exception ex)
            {
                throw new DaoCourseException(ex.Message);
            }
        }
        public void DeleteCourse(Course course)
        {
            CourseDaoModel courseToRemove = GetCourse(course.CourseId);

            courseToRemove.Deleted = course.Deleted;
            courseToRemove.DeletedDate = course.DeletedDate;
            courseToRemove.UserDeleted = course.UserDeleted;

            this._context.Update(courseToRemove);
            this._context.SaveChanges();
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

        public CourseDaoModel GetCourse(int Id)
        {
            CourseDaoModel? courseDaoModel = new CourseDaoModel();

            try
            {
                courseDaoModel = (from co in this._context.Course
                             join depto in this._context.Departments on co.DepartmentId equals depto.DepartmentId
                             where co.Deleted == false
                             && co.CourseId == Id
                             select new CourseDaoModel() 
                             { 
                                CourseId = co.CourseId,
                                CreatedDate = co.CreationDate,
                                Credits = co.Credits,
                                DepartmentId = co.DepartmentId,
                                DepartmentName = depto.Name,
                                Title = co.Title
                             }).FirstOrDefault();

            } catch(Exception ex)
            {
                throw new DaoCourseException($"Error obteniendo el curso: {ex.Message}");
            }
            return courseDaoModel;
        }

        public List<CourseDaoModel> GetCourses()
        {
            List<CourseDaoModel>? courseList = new List<CourseDaoModel>();

            try
            {
                courseList = (from course in this._context.Course
                                  join depto in this._context.Departments on course.DepartmentId 
                                                                          equals depto.DepartmentId
                                  where course.Deleted == false
                                  orderby course.CreationDate descending
                                  select new CourseDaoModel()
                                  {
                                      CourseId = course.CourseId,
                                      CreatedDate = course.CreationDate,
                                      Credits = course.Credits,
                                      DepartmentId = course.DepartmentId,
                                      DepartmentName = depto.Name,
                                      Title = course.Title
                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new DaoCourseException($"Error obteniendo el curso: {ex.Message}");
            }
            return courseList;
        }

        public bool ExistsCourse(Func<Course, bool> filter)
        {
            throw new NotImplementedException();
        }

        public List<CourseDaoModel> GetCourses(Func<Course, bool> filter)
        {
            List<CourseDaoModel>? courseList = new List<CourseDaoModel>();

            try
            {
                var courses = this._context.Course.Where(filter);

                courseList = (from course in courses
                              join depto in this._context.Departments.ToList()
                              on course.DepartmentId equals depto.DepartmentId
                              where course.Deleted == false
                              select new CourseDaoModel()
                              {
                                  CourseId = course.CourseId,
                                  CreatedDate = course.CreationDate,
                                  Credits = course.Credits,
                                  DepartmentId = course.DepartmentId,
                                  DepartmentName = depto.Name,
                                  Title = course.Title
                              }).ToList();
            }
            catch (Exception ex)
            {
                throw new DaoCourseException($"Error obteniendo el curso: {ex.Message}");
            }
            return courseList;
        }
    }
}
