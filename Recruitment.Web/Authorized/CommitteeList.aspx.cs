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
                units = daoFactory.GetUnitDao().GetAll("ShortName", true);
            }
            else
            {
                User u = daoFactory.GetUserDao().GetUserByLogin(User.Identity.Name);

                units = new List<CAESDO.Recruitment.Core.Domain.Unit>(u.Units);
            }

            dlistDepartment.DataSource = units;
            dlistDepartment.DataBind();
        }

        protected void ObjectDepartmentMembers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (dlistType.SelectedValue == STR_Committee)
                e.InputParameters["type"] = MemberTypes.AllCommittee;
            else
                e.InputParameters["type"] = MemberTypes.FacultyMember;
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

            if (dlistType.SelectedValue == STR_Committee)
                member.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.CommitteeMember, false);
            else
                member.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.FacultyMember, false);

            member.DepartmentFIS = dlistDepartment.SelectedValue;

            if (ValidateBO<DepartmentMember>.isValid(member))
            {
                using (new NHibernateTransaction())
                {
                    daoFactory.GetDepartmentMemberDao().SaveOrUpdate(member);
                }
            }
            else
            {
                eReport.ReportError(new ApplicationException("DepartmentMember not valid " + ValidateBO<DepartmentMember>.GetValidationResultsAsString(member)), "btnAddMember_Click");
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.VALIDATION));
            }

            //Now we have a new member, so rebind the grid
            gviewCommitteeList.DataBind();
        }

        protected void gviewCommitteeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DepartmentMember member = daoFactory.GetDepartmentMemberDao().GetById((int)gviewCommitteeList.DataKeys[e.RowIndex]["id"], false);

            using (new NHibernateTransaction())
            {
                daoFactory.GetDepartmentMemberDao().Delete(member);
            }

            e.Cancel = true;

            gviewCommitteeList.DataBind();
        }

        protected void dlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gviewCommitteeList.DataBind();
            //ObjectDepartmentMembers.DataBind();
        }
}

}