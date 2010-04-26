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
                    return daoFactory.GetPositionDao().GetById(int.Parse(dlistPositions.SelectedValue), false);
            }
        }

        public SortDirection membersSortDirection
        {
            get
            {
                if (ViewState[STR_MembersSortDirection] == null)
                {
                    ViewState[STR_MembersSortDirection] = SortDirection.Ascending;
                }

                return (SortDirection)ViewState[STR_MembersSortDirection];
            }

            set
            {
                ViewState[STR_MembersSortDirection] = value;
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
                this.bindMembers();
            else
            {
                //If the change to the select a position 0 entry, hide the access panel and rebind to the empty set
                pnlAccess.Visible = false;
                gviewMembers.DataSource = null;
                gviewMembers.DataBind();
            }

            if (gviewMembers.Rows.Count > 0)
                pnlAccess.Visible = true;
            else
                pnlAccess.Visible = false;
        }
        
        /// <summary>
        /// Overload to call bindMembers with the default sorting
        /// </summary>
        private void bindMembers()
        {
            this.bindMembers("LastName", SortDirection.Ascending);
        }

        private void bindMembers(string sortExpression, SortDirection sortDirection)
        {
            List<DepartmentMember> nonUniqueMembers = new List<DepartmentMember>();
            List<DepartmentMember> uniqueMembers = new List<DepartmentMember>();

            List<string> departmentFISList = new List<string>();

            //Get a list of all departments assocaited with this position
            foreach (Department d in currentPosition.Departments)
            {
                departmentFISList.Add(d.DepartmentFIS);
            }

            //Get all DepartmentMembers for the current position (aka: in the list of positions departments) of the proper type
            nonUniqueMembers = daoFactory.GetDepartmentMemberDao().GetMembersByDepartment(departmentFISList.ToArray());
            
            //Add external members
            foreach (CommitteeMember m in currentPosition.CommitteeMembers)
            {
                //Make sure the member is in the FRAC department
                if (m.DepartmentMember.DepartmentFIS == STR_FRAC)
                {
                    nonUniqueMembers.Add(m.DepartmentMember);
                }
            }

            //Now go through and make sure the member as unique
            foreach (DepartmentMember member in nonUniqueMembers)
            {
                if (!uniqueMembers.Contains(member))
                    uniqueMembers.Add(member);
            }

            uniqueMembers.Sort(new DepartmentMemberComparer(sortExpression, sortDirection));

            gviewMembers.DataSource = uniqueMembers;
            gviewMembers.DataBind();
        }

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
                member = daoFactory.GetDepartmentMemberDao().GetById(DepartmentMemberID, false);

                //Now we have the departmental member, check the committee records for this position
                List<CommitteeMember> committeAccessList = daoFactory.GetCommitteeMemberDao().GetMemberAssociationsByPosition(currentPosition, member);

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

        protected void btnUpdateAccess_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gviewMembers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cboxCommittee = row.FindControl(STR_ChkAllowCommittee) as CheckBox;
                    CheckBox cboxFaculty = row.FindControl(STR_ChkAllowFaculty) as CheckBox;
                    CheckBox cboxReview = row.FindControl(STR_ChkAllowReview) as CheckBox;

                    //Get the departmental member associated with this row
                    DepartmentMember member = daoFactory.GetDepartmentMemberDao().GetById((int)gviewMembers.DataKeys[row.RowIndex]["id"], false);

                    //Now get all committee roles for this member
                    List<CommitteeMember> memberAccess = daoFactory.GetCommitteeMemberDao().GetMemberAssociationsByPosition(currentPosition, member);
                    
                    using (new NHibernateTransaction())
                    {
                        CommitteeMember currentMember = new CommitteeMember();
                        currentMember.DepartmentMember = member;
                        currentMember.AssociatedPosition = currentPosition;

                        CommitteeMember currentMemberAccess = new CommitteeMember();

                        //First check for Committee Member access
                        currentMemberAccess = this.MemberInCommitteeListOfType(memberAccess, MemberTypes.CommitteeMember);

                        if (currentMemberAccess == null)
                        {
                            //member is not in the committee list.  If the box is checked, add them to the committee list
                            if (cboxCommittee.Checked)
                            {
                                CommitteeMember newMemberAccess = new CommitteeMember();
                                newMemberAccess.DepartmentMember = member;
                                newMemberAccess.AssociatedPosition = currentPosition;
                                newMemberAccess.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.CommitteeMember, false);

                                currentPosition.CommitteeMembers.Add(newMemberAccess);
                            }
                        }
                        else
                        {
                            //member is in the committee list.  Remove if the box is unchecked
                            if (!cboxCommittee.Checked)
                                currentPosition.CommitteeMembers.Remove(currentMemberAccess);
                        }

                        //Now check for Faculty Member access
                        currentMemberAccess = this.MemberInCommitteeListOfType(memberAccess, MemberTypes.FacultyMember);

                        if (currentMemberAccess == null)
                        {
                            //member is not in the faculty list.  If the box is checked, add them
                            if (cboxFaculty.Checked)
                            {
                                CommitteeMember newMemberAccess = new CommitteeMember();
                                newMemberAccess.DepartmentMember = member;
                                newMemberAccess.AssociatedPosition = currentPosition;
                                newMemberAccess.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.FacultyMember, false);

                                currentPosition.CommitteeMembers.Add(newMemberAccess);
                            }
                        }
                        else
                        {
                            //member is in the committee list.  Remove if the box is unchecked
                            if (!cboxFaculty.Checked)
                                currentPosition.CommitteeMembers.Remove(currentMemberAccess);
                        }

                        //Finally check for review member access
                        currentMemberAccess = this.MemberInCommitteeListOfType(memberAccess, MemberTypes.Reviewer);

                        if (currentMemberAccess == null)
                        {
                            //member is not in the reviewer list.  If the box is checked, add them
                            if (cboxReview.Checked)
                            {
                                CommitteeMember newMemberAccess = new CommitteeMember();
                                newMemberAccess.DepartmentMember = member;
                                newMemberAccess.AssociatedPosition = currentPosition;
                                newMemberAccess.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.Reviewer, false);

                                currentPosition.CommitteeMembers.Add(newMemberAccess);
                            }
                        }
                        else
                        {
                            //member is in the committee list.  Remove if the box is unchecked
                            if (!cboxReview.Checked)
                                currentPosition.CommitteeMembers.Remove(currentMemberAccess);
                        }

                        daoFactory.GetPositionDao().SaveOrUpdate(currentPosition);
                    }
                }
            }

            this.bindMembers();

            //Display an update successful message
            lblCommitteeUpdated.Text = "Committee Membership Successfully Updated";
        }

        /// <summary>
        /// Searches the committee list and returns the member record that matches the given type
        /// </summary>
        /// <returns>Member if found, else null</returns>
        private CommitteeMember MemberInCommitteeListOfType(List<CommitteeMember> memberList, MemberTypes type)
        {
            foreach (CommitteeMember member in memberList)
            {
                if (member.MemberType.ID == (int)type) //If the member type matches, return
                    return member;
            }

            return null;
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
                    committeeAccess.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.CommitteeMember, false);
                    break;
                case "Faculty":
                    committeeAccess.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.FacultyMember, false);
                    break;
                case "Review":
                    committeeAccess.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.Reviewer, false);
                    break;
            }

            //save the department member and add to the position committee for this position
            if (ValidateBO<DepartmentMember>.isValid(member))
            {
                using (new NHibernateTransaction())
                {
                    member = daoFactory.GetDepartmentMemberDao().SaveOrUpdate(member); //Save them member

                    committeeAccess.DepartmentMember = member;

                    currentPosition.CommitteeMembers.Add(committeeAccess);
                    daoFactory.GetPositionDao().SaveOrUpdate(currentPosition);
                }
            }
            else
            {
                eReport.ReportError(new ApplicationException("Member Not Valid: " + ValidateBO<DepartmentMember>.GetValidationResultsAsString(member)), "btnAddMember_Click");
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.VALIDATION));
            }

            //rebind the datagrid
            this.bindMembers();
        }

        protected void gviewMembers_Sorting(object sender, GridViewSortEventArgs e)
        {            
            this.bindMembers(e.SortExpression, membersSortDirection); //Sort by the current sort direction

            //Now flip the current sort direction
            membersSortDirection = (membersSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
        }
    }

    public class DepartmentMemberComparer : IComparer<DepartmentMember>
    {
        private const string STR_FRAC = "FRAC";
        private const string STR_Department = "Department";

        private SortDirection sortDirection;  

        public SortDirection SortDirection
        {
            get { return this.sortDirection; }
            set { this.sortDirection = value; } 
        }

        private string sortExpression;

        public DepartmentMemberComparer(string sortExpression, SortDirection sortDirection)
	    {
            this.sortExpression = sortExpression;
            this.sortDirection = sortDirection; 
	    }

        #region IComparer<DepartmentMember> Members

        public int Compare(DepartmentMember x, DepartmentMember y)
        {
            IComparable obj1 = null;
            IComparable obj2 = null;
            
            if (sortExpression == STR_Department)
            {
                //Do a custom search for department
                if (x.DepartmentFIS != STR_FRAC)
                {
                    //If the department FIS is not FRAC, use the unit short name
                    obj1 = x.Unit.ShortName;
                }
                else
                {
                    //Else, use the OtherDepartmentName
                    obj1 = x.OtherDepartmentName;
                }

                //Do a custom search for department
                if (y.DepartmentFIS != STR_FRAC)
                {
                    //If the department FIS is not FRAC, use the unit short name
                    obj2 = y.Unit.ShortName;
                }
                else
                {
                    //Else, use the OtherDepartmentName
                    obj2 = y.OtherDepartmentName;
                }
            }
            else
            {
                //If it's not department, use the generic comparison
                System.Reflection.PropertyInfo propertyInfo = typeof(DepartmentMember).GetProperty(sortExpression);
                obj1 = (IComparable)propertyInfo.GetValue(x, null);
                obj2 = (IComparable)propertyInfo.GetValue(y, null);
            }
                        
            if (SortDirection == SortDirection.Ascending)
            {
                return obj1.CompareTo(obj2);
            }
            else 
                return obj2.CompareTo(obj1); 
        }

        #endregion
    }
}