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
        private const string STR_CBOXALLOW = "chkAllowMember";
        private const string STR_FRAC = "FRAC";
        private const string STR_MembersSortDirection = "MembersSortDirection";

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

        public MemberType currentMemberType
        {
            get
            {
                if (dlistType.SelectedValue == STR_Committee)
                    return daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.CommitteeMember, false);
                else
                    return daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.FacultyMember, false);
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
        
        protected void dlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(STR_CommitteeManagement + dlistType.SelectedValue);
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

            foreach (DepartmentMember m in currentPosition.PositionCommittee)
            {
                if (m.MemberType == currentMemberType) //Make sure the member is of the correct type
                {
                    //Make sure the member is in the FRAC department
                    if (m.DepartmentFIS == STR_FRAC)
                    {
                        members.Add(m);
                    }
                }
            }

            members.Sort(new DepartmentMemberComparer(sortExpression, sortDirection));
            //members.Sort(new GenericComparer<DepartmentMember>(sortExpression, sortDirection));
            
            gviewMembers.DataSource = members;
            gviewMembers.DataBind();
        }

        /// <summary>
        /// Gets the row's department member, and checks the allow box if the member is in the current position
        /// </summary>
        protected void gviewMembers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gview = (GridView)sender;
            CheckBox cbox = e.Row.FindControl(STR_CBOXALLOW) as CheckBox;

            int DepartmentMemberID;
            DepartmentMember member;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DepartmentMemberID = (int)gview.DataKeys[e.Row.RowIndex]["id"];
                member = daoFactory.GetDepartmentMemberDao().GetById(DepartmentMemberID, false);

                if (currentPosition.PositionCommittee.Contains(member))
                    cbox.Checked = true;
                else
                    cbox.Checked = false;                
            }
        }

        protected void btnUpdateAccess_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gviewMembers.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cboxAllow = (CheckBox)row.FindControl(STR_CBOXALLOW);
                    DepartmentMember member = daoFactory.GetDepartmentMemberDao().GetById((int)gviewMembers.DataKeys[row.RowIndex]["id"], false);

                    using (new NHibernateTransaction())
                    {
                        if (currentPosition.PositionCommittee.Contains(member))
                        {
                            //If the member is already in the committee, then delete only if the box is unchecked
                            if (cboxAllow.Checked == false)
                            {
                                currentPosition.PositionCommittee.Remove(member);
                            }
                        }
                        else
                        {
                            //The member is not already in the committee, so add only if the box is checked
                            if (cboxAllow.Checked == true)
                            {
                                currentPosition.PositionCommittee.Add(member);
                            }
                        }

                        daoFactory.GetPositionDao().SaveOrUpdate(currentPosition);
                    }
                }
            }

            this.bindMembers();
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

            if (dlistType.SelectedValue == STR_Committee)
            {
                member.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.CommitteeMember, false);
            }
            else
            {
                member.MemberType = daoFactory.GetMemberTypeDao().GetById((int)MemberTypes.FacultyMember, false);
            }

            //save the department member and add to the position committee for this position
            if (ValidateBO<DepartmentMember>.isValid(member))
            {
                using (new NHibernateTransaction())
                {
                    member = daoFactory.GetDepartmentMemberDao().SaveOrUpdate(member); //Save them member
                    
                    currentPosition.PositionCommittee.Add(member);
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