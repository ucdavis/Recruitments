using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_CommitteeList : ApplicationPage
    {
        private const string STR_Committee = "Committee";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindDepartments();
            }
        }

        private void DataBindDepartments()
        {
            List<CAESDO.Recruitment.Core.Domain.Unit> units = new List<CAESDO.Recruitment.Core.Domain.Unit>();

            if (Roles.IsUserInRole("Admin"))
            {
                units =  UnitBLL.GetAll("ShortName", true);
            }
            else
            {
                User u = UserBLL.GetByLogin(User.Identity.Name);

                units = new List<CAESDO.Recruitment.Core.Domain.Unit>(u.Units);
            }

            dlistDepartment.DataSource = units;
            dlistDepartment.DataBind();
        }

        protected void dlistDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlistDepartment.SelectedValue == "0")
                pnlAddMember.Visible = false;
            else
                pnlAddMember.Visible = true;
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            DepartmentMember member = new DepartmentMember();

            member.LoginID = txtLoginID.Text;
            member.FirstName = txtFName.Text;
            member.LastName = txtLName.Text;
                        
            member.DepartmentFIS = dlistDepartment.SelectedValue;

            using (var ts = new TransactionScope())
            {
                DepartmentMemberBLL.EnsurePersistent(ref member);

                ts.CommitTransaction();
            }

            //Now we have a new member, so rebind the grid
            lviewApplications.DataBind();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;

            int departmentMemberID = int.Parse(lbtn.CommandArgument);
            DepartmentMember member = DepartmentMemberBLL.GetByID(departmentMemberID);

            using (var ts = new TransactionScope())
            {
                member.Inactive = true; //Mark the member as inactive

                DepartmentMemberBLL.EnsurePersistent(ref member);

                ts.CommitTransaction();
            }

            lviewApplications.DataBind();
        }

        /*
        protected void gviewCommitteeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int departmentMemberID = (int)gviewCommitteeList.DataKeys[e.RowIndex]["id"];
            DepartmentMember member = DepartmentMemberBLL.GetByID(departmentMemberID);

            using (var ts = new TransactionScope())
            {
                member.Inactive = true; //Mark the member as inactive

                DepartmentMemberBLL.EnsurePersistent(ref member);
                
                ts.CommitTransaction();
            }

            e.Cancel = true;

            gviewCommitteeList.DataBind();
        }
         */

        protected void dlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lviewApplications.DataBind();
            //ObjectDepartmentMembers.DataBind();
        }
}

}