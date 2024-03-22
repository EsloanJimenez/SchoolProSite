using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
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
            throw new NotImplementedException();
        }
        public void UpdateCourse(Course course)
        {
            Course courseToUpdated = this.GetCourse(course.CourseId);

            courseToUpdated.ModifyDate = course.ModifyDate;
            courseToUpdated.Title = course.Title;
        }
        public void DeleteCourse(Course course)
        {
            throw new NotImplementedException();
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




    }
}
