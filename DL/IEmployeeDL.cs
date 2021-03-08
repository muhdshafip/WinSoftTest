using System.Data;

namespace DL
{
    public interface IEmployeeDL
    {
        DataTable GetDepartments();
        DataTable GetEmployees(int DeptId);
        int SaveDepartment(int DeptId, string Code, string Department);
    }
}
