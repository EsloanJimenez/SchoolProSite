﻿using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
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
            if (this.ExistsDepartment(cd => cd.Name == department.Name))
                throw new DaoDepartmentException("El departamento ya se encuentra registrado");

            this._context.Departments.Add(department);
            this._context.SaveChanges();
        }
        public void UpgradeDepartment(Department department)
        {
            Department departmentToUpdate = this.GetDepartment(department.DepartmentId);

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
            return this._context.Departments.ToList();
        }

        public bool ExistsDepartment(Func<Department, bool> filter)
        {
            return this._context.Departments.Any(filter);
        }

        public List<Department> GetDepartments(Func<Department, bool> filter)
        {
            return this._context.Departments.Where(filter).ToList();
        }




    }
}
