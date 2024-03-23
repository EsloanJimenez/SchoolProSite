using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoOfficeAssignment
    {
        void SaveOfficeAssignment(OfficeAssignment officeAssignment);
        void UpdateOfficeAssignment(OfficeAssignment officeAssignment);
        void DeleteOfficeAssignment(OfficeAssignment officeAssignment);
        OfficeAssignment GetOfficeAssignment(int id);
        List<OfficeAssignment> GetOfficeAssignment();
        bool ExistsOfficeAssignment(Func<OfficeAssignment, bool> filter);
        List<OfficeAssignment> GetOfficeAssignment(Func<OfficeAssignment, bool> filter);
    }
}
