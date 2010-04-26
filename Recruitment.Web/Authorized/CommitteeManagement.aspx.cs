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

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_CommitteeManagement : ApplicationPage
    {
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

        }

        protected void dlistPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.bindMembers();
        }

        protected void gviewMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gview = (GridView)sender;

            //Grab the current committeeMemberID
            int committeeMemberID = (int)gview.DataKeys[e.RowIndex]["id"];

            using (new NHibernateTransaction())
            {
                daoFactory.GetCommitteeMemberDao().Delete(daoFactory.GetCommitteeMemberDao().GetById(committeeMemberID, false));
            }

            this.bindMembers();

            e.Cancel = true;
        }

        private void bindMembers()
        {
            if (committeeType == "committee")
            {
                gviewMembers.DataSource = daoFactory.GetCommitteeMemberDao().GetAllByMemberType(currentPosition, MemberTypes.AllCommittee);
            }
            else if (committeeType == "faculty")
            {
                gviewMembers.DataSource = daoFactory.GetCommitteeMemberDao().GetAllByMemberType(currentPosition, MemberTypes.FacultyMember);
            }

            gviewMembers.DataBind();
        }
}
}