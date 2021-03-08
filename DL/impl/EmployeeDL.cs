using System.Data;

namespace DL
{
    public class EmployeeDL : IEmployeeDL
    {
        DataAccess da = new DataAccess();
        public DataTable GetDepartments()
        {
            return da.FetchDataTable("GetAllDepartments", false);
        }
        public DataTable GetEmployees(int DeptId)
        {
            string[] strParm = new string[1];
            object[] objParm = new object[1];

            strParm[0] = "@DeptId"; objParm[0] = DeptId;
            return da.FetchDataTable("GetDepartmentwiseEmployees", strParm, objParm, false);
        }
        public int SaveDepartment(int DeptId, string Code, string Department)
        {
            string[] strParm = new string[3];
            object[] objParm = new object[3];

            strParm[0] = "@DeptId"; objParm[0] = DeptId;
            strParm[1] = "@Code"; objParm[1] = Code;
            strParm[2] = "@Department"; objParm[2] = Department;

            return da.Execute("SaveDepartments", strParm, objParm, false);
        }
    }
}
