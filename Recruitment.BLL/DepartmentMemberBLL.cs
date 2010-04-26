using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;
using System.Web.UI.WebControls;
using System.DirectoryServices.Protocols;

namespace CAESDO.Recruitment.BLL
{
    public class DepartmentMemberBLL : GenericBLL<DepartmentMember, int>
    {
        public static List<DepartmentMember> Sort(List<DepartmentMember> departmentMembers, string sortExpression, SortDirection sortDirection)
        {
            departmentMembers.Sort(new DepartmentMemberComparer(sortExpression, sortDirection));

            return departmentMembers;
        }

        public static DepartmentMember Search(string loginID)
        {
            DepartmentMember member = new DepartmentMember();

            //Establishing an Anonymous Connection to the LDAP Server
            LdapDirectoryIdentifier ldapident = new LdapDirectoryIdentifier("ldap.ucdavis.edu");
            LdapConnection lc = new LdapConnection(ldapident, null, AuthType.Basic);
            lc.Bind();
            lc.SessionOptions.ProtocolVersion = 3;
            lc.SessionOptions.SecureSocketLayer = false;
            
            //Configure the Search Request to Query the UCD OpenLDAP Server's People Search Base for a Specific User ID or Mail ID and Return the Requested Attributes 
            string[] attributesToReturn = new string[] { "uid", "mail", "displayName", "cn", "sn", "givenName", "telephoneNumber", "ou", "departmentNumber" };

            string strSearchFilter = "(&(uid=" + loginID + "))";
            string strSearchBase = "ou=People,dc=ucdavis,dc=edu";

            SearchRequest sRequest = new SearchRequest(strSearchBase, strSearchFilter, SearchScope.Subtree, attributesToReturn);
            //Send the Request and Load the Response
            SearchResponse sResponse = (SearchResponse)lc.SendRequest(sRequest);

            if (sResponse.Entries.Count > 0)
            {
                //Grab out the first response entry
                SearchResultEntry sResult = sResponse.Entries[0];

                foreach (DirectoryAttribute attr in sResult.Attributes.Values)
                {
                    switch (attr.Name)
                    {
                        case "uid":
                            member.LoginID = attr[0].ToString();
                            break;
                        case "givenName":
                            member.FirstName = attr[0].ToString();
                            break;
                        case "sn":
                            member.LastName = attr[0].ToString();
                            break;
                        case "ou":
                            member.OtherDepartmentName = attr[0].ToString();
                            break;
                        default:
                            break;
                    }
                }

                return member;
            }
            else
            {
                return null;
            }
        }

        public static List<DepartmentMember> GetMembersByDepartment(string departmentFIS)
        {
            return daoFactory.GetDepartmentMemberDao().GetMembersByDepartment(departmentFIS);
        }

        public static List<DepartmentMember> GetMembersByDepartment(string[] departmentFISList)
        {
            return daoFactory.GetDepartmentMemberDao().GetMembersByDepartment(departmentFISList);
        }

        public static List<DepartmentMember> GetUniqueByPosition(Position position)
        {
            List<DepartmentMember> nonUniqueMembers = new List<DepartmentMember>();
            List<DepartmentMember> uniqueMembers = new List<DepartmentMember>();

            List<string> departmentFISList = new List<string>();

            //Get a list of all departments assocaited with this position
            foreach (Department d in position.Departments)
            {
                departmentFISList.Add(d.DepartmentFIS);
            }

            //Get all DepartmentMembers for the current position (aka: in the list of positions departments) of the proper type
            nonUniqueMembers = DepartmentMemberBLL.GetMembersByDepartment(departmentFISList.ToArray());

            //Add external members
            foreach (CommitteeMember m in position.CommitteeMembers)
            {
                //Make sure the member is in the FRAC department
                if (m.DepartmentMember.DepartmentFIS == "FRAC")
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

            return uniqueMembers;
            //uniqueMembers.Sort(new DepartmentMemberComparer(sortExpression, sortDirection));
        }

        public static void UpdateAccess(DepartmentMember member, Position position, bool committee, bool faculty, bool reviewer)
        {
            //Now get all committee roles for this member
            List<CommitteeMember> memberAccess = CommitteeMemberBLL.GetByAssociationsPosition(position, member);
                    
            CommitteeMember currentMember = new CommitteeMember();
            currentMember.DepartmentMember = member;
            currentMember.AssociatedPosition = position;

            //First check for Committee Member access
            CommitteeMember currentMemberAccess = DepartmentMemberBLL.MemberInCommitteeListOfType(memberAccess, MemberTypes.CommitteeMember);

            if (currentMemberAccess == null)
            {
                //member is not in the committee list.  If the box is checked, add them to the committee list
                if (committee)
                {
                    CommitteeMember newMemberAccess = new CommitteeMember();
                    newMemberAccess.DepartmentMember = member;
                    newMemberAccess.AssociatedPosition = position;
                    newMemberAccess.MemberType = MemberTypeBLL.GetByID((int)MemberTypes.CommitteeMember);

                    position.CommitteeMembers.Add(newMemberAccess);
                }
            }
            else
            {
                //member is in the committee list.  Remove if the box is unchecked
                if (!committee)
                    position.CommitteeMembers.Remove(currentMemberAccess);
            }

            //Now check for Faculty Member access
            currentMemberAccess = DepartmentMemberBLL.MemberInCommitteeListOfType(memberAccess, MemberTypes.FacultyMember);

            if (currentMemberAccess == null)
            {
                //member is not in the faculty list.  If the box is checked, add them
                if (faculty)
                {
                    CommitteeMember newMemberAccess = new CommitteeMember();
                    newMemberAccess.DepartmentMember = member;
                    newMemberAccess.AssociatedPosition = position;
                    newMemberAccess.MemberType = MemberTypeBLL.GetByID((int)MemberTypes.FacultyMember);

                    position.CommitteeMembers.Add(newMemberAccess);
                }
            }
            else
            {
                //member is in the committee list.  Remove if the box is unchecked
                if (!faculty)
                    position.CommitteeMembers.Remove(currentMemberAccess);
            }

            //Finally check for review member access
            currentMemberAccess = DepartmentMemberBLL.MemberInCommitteeListOfType(memberAccess, MemberTypes.Reviewer);

            if (currentMemberAccess == null)
            {
                //member is not in the reviewer list.  If the box is checked, add them
                if (reviewer)
                {
                    CommitteeMember newMemberAccess = new CommitteeMember();
                    newMemberAccess.DepartmentMember = member;
                    newMemberAccess.AssociatedPosition = position;
                    newMemberAccess.MemberType = MemberTypeBLL.GetByID((int)MemberTypes.Reviewer);

                    position.CommitteeMembers.Add(newMemberAccess);
                }
            }
            else
            {
                //member is in the committee list.  Remove if the box is unchecked
                if (!reviewer)
                    position.CommitteeMembers.Remove(currentMemberAccess);
            }

            //Position position = position;
            PositionBLL.EnsurePersistent(ref position);
        }

        /// <summary>
        /// Searches the committee list and returns the member record that matches the given type
        /// </summary>
        /// <returns>Member if found, else null</returns>
        private static CommitteeMember MemberInCommitteeListOfType(List<CommitteeMember> memberList, MemberTypes type)
        {
            foreach (CommitteeMember member in memberList)
            {
                if (member.MemberType.ID == (int)type) //If the member type matches, return
                    return member;
            }

            return null;
        }

        private class DepartmentMemberComparer : IComparer<DepartmentMember>
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
}
