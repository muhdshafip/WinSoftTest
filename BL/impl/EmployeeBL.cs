using DL;
using System.Data;

namespace BL
{
    public class EmployeeBL : IEmployeeBL
    {
        IEmployeeDL employeeDL;
        public EmployeeBL()
        {
            employeeDL = new EmployeeDL();
        }
        public DataTable GetDepartments()
        {
            return employeeDL.GetDepartments();
        }
        public DataTable GetEmployees(int DeptId)
        {
            return employeeDL.GetEmployees(DeptId);
        }
        public int SaveDepartment(int DeptId, string Code, string Department)
        {
            return employeeDL.SaveDepartment(DeptId, Code, Department);
        }
    }
}