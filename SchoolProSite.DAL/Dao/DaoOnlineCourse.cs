using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;

namespace SchoolProSite.DAL.Dao
{
    public class DaoOnlineCourse : IDaoOnlineCourse
    {
        private readonly SchoolContext _context;
        public DaoOnlineCourse(SchoolContext context)
        {
            this._context = context;
        }
        public void SaveOnlineCourse(OnlineCourse onlineCourse)
        {
            string message = string.Empty;

            if (!IsOnlineCourseValid(onlineCourse, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.OnlineCourse.Add(onlineCourse);
            this._context.SaveChanges();
        }
        public void UpdateOnlineCourse(OnlineCourse onlineCourse)
        {
            string message = string.Empty;

            if (!IsOnlineCourseValid(onlineCourse, ref message, Operations.Update))
                throw new DaoException(message);

            OnlineCourse onlineCourseToUpdated = this.GetOnlineCourse(onlineCourse.OnlineCourseId);

            onlineCourseToUpdated.Url = onlineCourse.Url;

            this._context.OnlineCourse.Update(onlineCourseToUpdated);
            this._context.SaveChanges();
        }
        public void DeleteOnlineCourse(OnlineCourse onlineCourse)
        {
            OnlineCourse onlineCourseToRemove = this.GetOnlineCourse(onlineCourse.OnlineCourseId);

            this._context.OnlineCourse.Update(onlineCourseToRemove);
            this._context.SaveChanges();
        }
        public OnlineCourse GetOnlineCourse(int id)
        {
            return this._context.OnlineCourse.Find(id);
        }
        public List<OnlineCourse> GetOnlineCourse()
        {
            return this._context.OnlineCourse.ToList();
        }
        public bool ExistsOnlineCourse(Func<OnlineCourse, bool> filter)
        {
            return this._context.OnlineCourse.Any(filter);
        }

        public List<OnlineCourse> GetOnlineCourses(Func<OnlineCourse, bool> filter)
        {
            return this._context.OnlineCourse.Where(filter).ToList();
        }


        private bool IsOnlineCourseValid(OnlineCourse onlineCourse, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(onlineCourse.Url))
            {
                message = "La URL es requerido";
                return result;
            }
            if (onlineCourse.Url.Length > 100)
            {
                message = "La URL no puede ser mayor a 100 caracteres";
                return result;
            }
            if (operations == Operations.Save)
            {
                if (this.ExistsOnlineCourse(cd => cd.Url == onlineCourse.Url))
                {
                    message = "La URL ya se encuentra registrada";
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
