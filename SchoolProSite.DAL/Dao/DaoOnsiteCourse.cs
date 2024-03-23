using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Linq;

namespace SchoolProSite.DAL.Dao
{
    public class DaoOnsiteCourse : IDaoOnsiteCourse
    {
        private readonly SchoolContext _context;

        public DaoOnsiteCourse(SchoolContext context)
        {
            this._context = context;
        }

        public void SaveOnsiteCourse(OnsiteCourse onsiteCourse)
        {
            string message = string.Empty;

            if (!IsOnsiteCourseValid(onsiteCourse, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.OnsiteCourse.Add(onsiteCourse);
            this._context.SaveChanges();
        }
        public void UpdateOnsiteCourse(OnsiteCourse onsiteCourse)
        {
            string message = string.Empty;

            if (!IsOnsiteCourseValid(onsiteCourse, ref message, Operations.Update))
                throw new DaoException(message);

            OnsiteCourse onsiteCourseToUpdated = this.GetOnsiteCourse(onsiteCourse.OnsiteCourseId);

            onsiteCourseToUpdated.Location = onsiteCourse.Location;
            onsiteCourseToUpdated.Days = onsiteCourse.Days;

            this._context.OnsiteCourse.Update(onsiteCourseToUpdated);
            this._context.SaveChanges();
        }
        public void DeleteOnsiteCourse(OnsiteCourse onsiteCourse)
        {
            OnsiteCourse OnsiteCourseToRemove = this.GetOnsiteCourse(onsiteCourse.OnsiteCourseId);

            this._context.OnsiteCourse.Update(OnsiteCourseToRemove);
            this._context.SaveChanges();
        }
        public OnsiteCourse GetOnsiteCourse(int id)
        {
            return this._context.OnsiteCourse.Find(id);
        }
        public List<OnsiteCourse> GetOnsiteCourses()
        {
            return this._context.OnsiteCourse.ToList();
        }
        public bool ExistsOnsiteCourse(Func<OnsiteCourse, bool> filter)
        {
            return this._context.OnsiteCourse.Any(filter);
        }

        public List<OnsiteCourse> GetOnsiteCourses(Func<OnsiteCourse, bool> filter)
        {
            return this._context.OnsiteCourse.Where(filter).ToList();
        }


        private bool IsOnsiteCourseValid(OnsiteCourse onsiteCourse, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(onsiteCourse.Location))
            {
                message = "La ubicacion es requerido";
                return result;
            }
            if (onsiteCourse.Location.Length > 100)
            {
                message = "La ubicacion no puede ser mayor a 50 caracteres";
                return result;
            }
            if (string.IsNullOrEmpty(onsiteCourse.Days))
            {
                message = "El dia es requerido";
                return result;
            }
            if (onsiteCourse.Days.Length > 50)
            {
                message = "El dia no puede ser mayor a 50 caracteres";
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
