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
using CAESDO.Recruitment.Data;
using System.Collections.Generic;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_CommitteeManagement : ApplicationPage
    {
        public const string STR_ADMININDEX = "AdminIndex.aspx";
        private const string STR_CommitteeManagement = "CommitteeManagement.aspx?type=";
        private const string STR_Committee = "committee";
        private const string STR_Faculty = "faculty";

        public string committeeType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        public Position currentPosition
        {
            get
            {
                if (dlistPositions.SelectedIndex == 0)
                    return null;
                else
                    return daoFactory.GetPositionDao().GetById(int.Parse(dlistPositions.SelectedValue), false);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(committeeType))
                    Response.Redirect(STR_ADMININDEX);

                //Now that we have a proper query string, set the selected item
                ListItem item = dlistType.Items.FindByValue(committeeType.ToLower());

                if (item != null)
                    item.Selected = true;
                else
                    Response.Redirect(STR_ADMININDEX);
            }
        }

        protected void dlistPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlistPositions.SelectedValue != "0") //Make sure they chose a real position
                this.bindMembers();
        }
        
        protected void dlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(STR_CommitteeManagement + dlistType.SelectedValue);
        }

        private void bindMembers()
        {
            List<DepartmentMember> members = new List<DepartmentMember>();
            List<string> departmentFISList = new List<string>();

            //Get a list of all departments assocaited with this position
            foreach (Department d in currentPosition.Departments)
            {
                departmentFISList.Add(d.DepartmentFIS);
            }

            //Get all DepartmentMembers for the current position (aka: in the list of positions departments) of the proper type
            if (committeeType == STR_Committee)
            {
                members = daoFactory.GetDepartmentMemberDao().GetMembersByDepartmentAndType(departmentFISList.ToArray(), MemberTypes.AllCommittee);
            }
            else if (committeeType == STR_Faculty)
            {
                members = daoFactory.GetDepartmentMemberDao().GetMembersByDepartmentAndType(departmentFISList.ToArray(), MemberTypes.FacultyMember);
            }

            gviewMembers.DataSource = members;
            gviewMembers.DataBind();
            
        }

}
}