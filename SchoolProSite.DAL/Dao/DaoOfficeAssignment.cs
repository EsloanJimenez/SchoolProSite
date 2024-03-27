using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;

namespace SchoolProSite.DAL.Dao
{
    public class DaoOfficeAssignment : IDaoOfficeAssignment
    {
        private readonly SchoolContext _context;
        public DaoOfficeAssignment(SchoolContext context)
        {
            this._context = context;
        }
        public void SaveOfficeAssignment(OfficeAssignment officeAssignment)
        {
            string message = string.Empty;

            if (!IsOfficeAssignmentValid(officeAssignment, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.OfficeAssignments.Add(officeAssignment);
            this._context.SaveChanges();
        }
        public void UpdateOfficeAssignment(OfficeAssignment officeAssignment)
        {
            string message = string.Empty;

            if (!IsOfficeAssignmentValid(officeAssignment, ref message, Operations.Update))
                throw new DaoException(message);

            OfficeAssignment officeAssignmentToUpdated = this.GetOfficeAssignment(officeAssignment.InstructorID);

            officeAssignmentToUpdated.Location = officeAssignment.Location;

            this._context.OfficeAssignments.Update(officeAssignmentToUpdated);
            this._context.SaveChanges();
        }
        public void DeleteOfficeAssignment(OfficeAssignment officeAssignment)
        {
            OfficeAssignment officeAssignmentToRemove = this.GetOfficeAssignment(officeAssignment.InstructorID);

            this._context.OfficeAssignments.Update(officeAssignmentToRemove);
            this._context.SaveChanges();
        }
        public OfficeAssignment GetOfficeAssignment(int id)
        {
            return this._context.OfficeAssignments.Find(id);
        }
        public List<OfficeAssignment> GetOfficeAssignment()
        {
            return this._context.OfficeAssignments.ToList();
        }
        public bool ExistsOfficeAssignment(Func<OfficeAssignment, bool> filter)
        {
            return this._context.OfficeAssignments.Any(filter);
        }

        public List<OfficeAssignment> GetOfficeAssignment(Func<OfficeAssignment, bool> filter)
        {
            return this._context.OfficeAssignments.Where(filter).ToList();
        }


        private bool IsOfficeAssignmentValid(OfficeAssignment officeAssignment, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(officeAssignment.Location))
            {
                message = "La ubicacion es requerido";
                return result;
            }
            if (officeAssignment.Location.Length > 100)
            {
                message = "La ubicacion no puede ser mayor a 50 caracteres";
                return result;
            }
            if (operations == Operations.Save)
            {
                if (this.ExistsOfficeAssignment(cd => cd.Location == officeAssignment.Location))
                {
                    message = "La ubicacion ya se encuentra registrada";
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
