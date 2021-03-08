using BL;
using System;
using System.Data;
using System.Web.ClientServices.Providers;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WinSoft
{
    public partial class _Default : Page
    {
        IEmployeeBL employeeBL;
        protected void Page_Load(object sender, EventArgs e)
        {
            employeeBL = new EmployeeBL();
            if(!IsPostBack)
            {
                BindDepartments();
            }
        }

        private void BindDepartments()
        {
            DataTable dtDepartments = employeeBL.GetDepartments();
            gvDepartments.DataSource = dtDepartments;
            gvDepartments.DataBind();
        }

        protected void lbnDepartment_Click(object sender, EventArgs e)
        {
            GridViewRow grw = (GridViewRow)((LinkButton)sender).Parent.Parent;
            hdnDeptId.Value = gvDepartments.DataKeys[grw.RowIndex]["DeptId"].ToString();
            BindEmployees();
            divDeptSave.Visible = false;
        }

        private void BindEmployees()
        {
            DataTable dtEmployees = employeeBL.GetEmployees(Convert.ToInt32(hdnDeptId.Value));
            gvEmployees.DataSource = dtEmployees;
            gvEmployees.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            divDeptSave.Visible = true;
            GridViewRow grw = (GridViewRow)((Button)sender).Parent.Parent;
            hdnDeptId.Value = gvDepartments.DataKeys[grw.RowIndex]["DeptId"].ToString();
            txtCode.Text = grw.Cells[0].Text;
            txtDepartment.Text = ((LinkButton)grw.FindControl("lbnDepartment")).Text;
        }

        protected void btnSaveDepartment_Click(object sender, EventArgs e)
        {
            int Result = employeeBL.SaveDepartment(Convert.ToInt32(hdnDeptId.Value), txtCode.Text, txtDepartment.Text);
            if(Result != 0)
            {
                BindDepartments();
                divDeptSave.Visible = false;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            hdnDeptId.Value = "0";
            txtCode.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            BindDepartments();
            divDeptSave.Visible = false;
        }

        [WebMethod]
        public static int SaveDepartment(int DeptId, string Code, string Department)
        {
            IEmployeeBL _employeeBL = new EmployeeBL();
            return _employeeBL.SaveDepartment(DeptId, Code, Department);
        }
    }
}