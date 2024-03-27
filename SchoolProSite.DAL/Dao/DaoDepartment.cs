using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;

namespace SchoolProSite.DAL.Dao
{
    public class DaoDepartment : IDaoDepartment
    {
        private readonly SchoolContext _context;
        public DaoDepartment(SchoolContext context)
        {
            this._context = context;
        }

        public void SaveDepartment(Department department)
        {
            string message = string.Empty;

            if (!IsDepartmentValid(department, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.Departments.Add(department);
            this._context.SaveChanges();
        }
        public void UpdateDepartment(Department department)
        {
            string message = string.Empty;

            if (!IsDepartmentValid(department, ref message, Operations.Update))
                throw new DaoException(message);

            Department departmentToUpdate = this.GetDepartment(department.DepartmentId);

            if (departmentToUpdate is null)
                throw new DaoException("El departamento no se encuentra registrado.");


            departmentToUpdate.ModifyDate = department.ModifyDate;
            departmentToUpdate.Name = department.Name;
            departmentToUpdate.StartDate = department.StartDate;
            departmentToUpdate.Budget = department.Budget;
            departmentToUpdate.Administrator = department.Administrator;
            departmentToUpdate.UserMod = department.UserMod;

            this._context.Departments.Update(departmentToUpdate);
            this._context.SaveChanges();
        }
        public void DeleteDepartment(Department department)
        {
            Department departmentToRemove = this.GetDepartment(department.DepartmentId);

            departmentToRemove.Deleted = department.Deleted;
            departmentToRemove.DeletedDate = department.DeletedDate;
            departmentToRemove.UserDeleted = departmentToRemove.UserDeleted;

            this._context.Departments.Update(departmentToRemove);
            this._context.SaveChanges();
        }
        public Department GetDepartment(int id)
        {
            return this._context.Departments.Find(id);
        }
        public List<Department> GetDepartments()
        {
            return this._context.Departments.OrderByDescending(depto => depto.CreationDate).ToList();
        }

        public bool ExistsDepartment(Func<Department, bool> filter)
        {
            return this._context.Departments.Any(filter);
        }

        public List<Department> GetDepartments(Func<Department, bool> filter)
        {
            return this._context.Departments.Where(filter).ToList();
        }


        private bool IsDepartmentValid(Department department, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(department.Name))
            {
                message = "El nombre del departamento es requerido";
                return result;
            }
            if (department.Name.Length > 50)
            {
                message = "El nombre del departamento no puede ser mayor a 50 caracteres";
                return result;
            }
            if (department.Budget == 0)
            {
                message = "El presupuesto no puede ser cero(0)";
                return result;
            }
            if(operations == Operations.Save)
            {
                if (this.ExistsDepartment(cd => cd.Name == department.Name))
                {
                    message = "El departamento ya se encuentra registrado";
                    return result;
                } else
                {
                    result = true;
                }
                    
            }
            else
                result = true;

            return result;
        }

    }
}
