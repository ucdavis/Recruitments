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
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_CommitteeManagement : ApplicationPage
    {
        public const string STR_ADMININDEX = "AdminIndex.aspx";
        private const string STR_CommitteeManagement = "CommitteeManagement.aspx?type=";
        private const string STR_Committee = "committee";
        private const string STR_Faculty = "faculty";
        private const string STR_FRAC = "FRAC";
        private const string STR_MembersSortDirection = "MembersSortDirection";
        private const string STR_ChkAllowCommittee = "chkAllowMember";        
        private const string STR_ChkAllowFaculty = "chkAllowFaculty";
        private const string STR_ChkAllowReview = "chkAllowReview";

        public Position currentPosition
        {
            get
            {
                if (dlistPositions.SelectedIndex == 0)
                    return null;
                else
                    return PositionBLL.GetByID(int.Parse(dlistPositions.SelectedValue));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// When the user chooses a position, pull all of the departmental members for that position with their roles
        /// </summary>
        protected void dlistPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlistPositions.SelectedValue != "0") //Make sure they chose a real position
            {
                pnlAccess.Visible = true;
                this.bindMembers();

                this.ShowUpdateAcessRegion();
            }
            else
            {
                //If the change to the select a position 0 entry, hide the access panel and rebind to the empty set
                pnlAccess.Visible = false;
                lviewMembers.DataSource = null;
                lviewMembers.DataBind();
            }
        }
        
        /// <summary>
        /// Overload to call bindMembers with the default sorting
        /// </summary>
        private void bindMembers()
        {
            List<DepartmentMember> uniqueMembers = DepartmentMemberBLL.GetUniqueByPosition(currentPosition);

            lviewMembers.DataSource = uniqueMembers;
            lviewMembers.DataBind();
        }

        /*
        /// <summary>
        /// Gets the row's department member, and checks all of the appropriate boxes regarding the roles that the member
        /// has in the current position
        /// </summary>
        protected void gviewMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gview = (GridView)sender;
            CheckBox cboxCommittee = e.Row.FindControl(STR_ChkAllowCommittee) as CheckBox;
            CheckBox cboxFaculty = e.Row.FindControl(STR_ChkAllowFaculty) as CheckBox;
            CheckBox cboxReview = e.Row.FindControl(STR_ChkAllowReview) as CheckBox;

            int DepartmentMemberID;
            DepartmentMember member;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DepartmentMemberID = (int)gview.DataKeys[e.Row.RowIndex]["id"];
                member = DepartmentMemberBLL.GetByID(DepartmentMemberID);

                //Now we have the departmental member, check the committee records for this position
                List<CommitteeMember> committeAccessList = CommitteeMemberBLL.GetByAssociationsPosition(currentPosition, member);

                //Now we have a list of all access types for this department member, so go through and check the correct boxes

                foreach (CommitteeMember cAccess in committeAccessList)
                {
                    switch (cAccess.MemberType.ID)
                    {
                        case (int)MemberTypes.CommitteeMember:
                            cboxCommittee.Checked = true;
                            break;
                        case (int)MemberTypes.FacultyMember:
                            cboxFaculty.Checked = true;
                            break;
                        case (int)MemberTypes.Reviewer:
                            cboxReview.Checked = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
         */

        /// <summary>
        /// Gets the row's department member, and checks all of the appropriate boxes regarding the roles that the member
        /// has in the current position
        /// </summary>
        protected void lviewMembers_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView lview = (ListView)sender;
            ListViewDataItem currentItem = (ListViewDataItem)e.Item;

            CheckBox cboxCommittee = currentItem.FindControl(STR_ChkAllowCommittee) as CheckBox;
            CheckBox cboxFaculty = currentItem.FindControl(STR_ChkAllowFaculty) as CheckBox;
            CheckBox cboxReview = currentItem.FindControl(STR_ChkAllowReview) as CheckBox;

            int DepartmentMemberID;
            DepartmentMember member;
            
            DepartmentMemberID = (int)lviewMembers.DataKeys[currentItem.DataItemIndex]["id"];
            member = DepartmentMemberBLL.GetByID(DepartmentMemberID);

            //Now we have the departmental member, check the committee records for this position
            List<CommitteeMember> committeAccessList = CommitteeMemberBLL.GetByAssociationsPosition(currentPosition, member);

            //Now we have a list of all access types for this department member, so go through and check the correct boxes

            foreach (CommitteeMember cAccess in committeAccessList)
            {
                switch (cAccess.MemberType.ID)
                {
                    case (int)MemberTypes.CommitteeMember:
                        cboxCommittee.Checked = true;
                        break;
                    case (int)MemberTypes.FacultyMember:
                        cboxFaculty.Checked = true;
                        break;
                    case (int)MemberTypes.Reviewer:
                        cboxReview.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        protected void btnUpdateAccess_Click(object sender, EventArgs e)
        {
            foreach (var row in lviewMembers.Items)
            {
                if (row.ItemType == ListViewItemType.DataItem)
                {
                    CheckBox cboxCommittee = row.FindControl(STR_ChkAllowCommittee) as CheckBox;
                    CheckBox cboxFaculty = row.FindControl(STR_ChkAllowFaculty) as CheckBox;
                    CheckBox cboxReview = row.FindControl(STR_ChkAllowReview) as CheckBox;

                    //Get the departmental member associated with this row
                    int departmentMemberID = (int)lviewMembers.DataKeys[row.DataItemIndex]["id"];
                    DepartmentMember member = DepartmentMemberBLL.GetByID(departmentMemberID);

                    using (new TransactionScope())
                    {
                        DepartmentMemberBLL.UpdateAccess(member, currentPosition, cboxCommittee.Checked, cboxFaculty.Checked, cboxReview.Checked);
                    }
                }
            }

            this.bindMembers();

            this.ShowUpdateAcessRegion();

            //Display an update successful message
            lblCommitteeUpdated.Text = "Committee Membership Successfully Updated";
        }

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            //Create the new department member
            DepartmentMember member = new DepartmentMember();

            member.DepartmentFIS = STR_FRAC;
            member.FirstName = txtFName.Text;
            member.LastName = txtLName.Text;
            member.OtherDepartmentName = txtDepartment.Text;
            member.LoginID = txtLoginID.Text;

            //Create the membership object
            CommitteeMember committeeAccess = new CommitteeMember();
            committeeAccess.AssociatedPosition = currentPosition;
            committeeAccess.DepartmentMember = member;

            switch (dlistMemberType.SelectedValue)
            {
                case "Committee":
                    committeeAccess.MemberType = MemberTypeBLL.GetByID((int)MemberTypes.CommitteeMember);
                    break;
                case "Faculty":
                    committeeAccess.MemberType = MemberTypeBLL.GetByID((int)MemberTypes.FacultyMember);
                    break;
                case "Review":
                    committeeAccess.MemberType = MemberTypeBLL.GetByID((int)MemberTypes.Reviewer);
                    break;
            }

            //save the department member and add to the position committee for this position
            using (new TransactionScope())
            {
                DepartmentMemberBLL.EnsurePersistent(ref member);

                committeeAccess.DepartmentMember = member;

                currentPosition.CommitteeMembers.Add(committeeAccess);

                Position position = currentPosition;

                PositionBLL.EnsurePersistent(ref position);
            }

            //Display an update successful message
            lblCommitteeUpdated.Text = "Committee Membership Successfully Updated";

            //rebind the datagrid
            this.bindMembers();

            this.ShowUpdateAcessRegion();
        }

        /*
        protected void gviewMembers_Sorting(object sender, GridViewSortEventArgs e)
        {            
            this.bindMembers(e.SortExpression, membersSortDirection); //Sort by the current sort direction

            //Now flip the current sort direction
            membersSortDirection = (membersSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
        }
         */

        private void ShowUpdateAcessRegion()
        {
            //Show the access panel if there are any results
            if (lviewMembers.Items.Count > 0)
                pnlUpdateAccess.Visible = true;
            else
                pnlUpdateAccess.Visible = false;
        }
    }
}