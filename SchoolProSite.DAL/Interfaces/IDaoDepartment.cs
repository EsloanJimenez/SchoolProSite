using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoDepartment
    {
        void SaveDepartment(Department department);
        void UpgradeDepartment(Department department);
        void DeleteDepartment(Department department);
        Department GetDepartment(int id);
        List<Department> GetDepartments();
        bool ExistsDepartment(Func<Department, bool> filter);

        List<Department> GetDepartments(Func<Department, bool> filter);
    }
}
