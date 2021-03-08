using System.Data;

namespace BL
{
    public interface IEmployeeBL
    {
        DataTable GetDepartments();
        DataTable GetEmployees(int DeptId);
        int SaveDepartment(int DeptId, string Code, string Department);
    }
}
