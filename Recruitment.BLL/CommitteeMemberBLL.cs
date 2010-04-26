using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;
using System.Web;

namespace CAESDO.Recruitment.BLL
{
    public class CommitteeMemberBLL : GenericBLL<CommitteeMember, int>
    {
        public static List<CommitteeMember> GetByAssociationsPosition(Position associatedPosition, DepartmentMember member)
        {
            return daoFactory.GetCommitteeMemberDao().GetMemberAssociationsByPosition(associatedPosition, member);
        }

        public static bool IsUserMember(MemberTypes memberType)
        {
            return daoFactory.GetCommitteeMemberDao().IsUserMember(memberType);
        }

        public static void CheckAccess(Position position, out bool allowedAccess, out bool reviewerAccess)
        {
            allowedAccess = false;
            reviewerAccess = false;

            foreach (CommitteeMember member in position.CommitteeMembers)
            {
                if (member.DepartmentMember.LoginID == HttpContext.Current.User.Identity.Name)
                {
                    //The user is in this position's access list, but we need to check access status
                    if (member.MemberType.ID == (int)MemberTypes.CommitteeChair || member.MemberType.ID == (int)MemberTypes.CommitteeMember)
                    {
                        //Allow access always if the user is in the committee
                        allowedAccess = true;
                        reviewerAccess = false;
                        break; //If the user is a committee member, break out because they have full access
                    }
                    else //user is faculty or reviewer
                    {
                        if (position.FacultyView) //make sure this position is accepting faculty review
                        {
                            if (member.MemberType.ID == (int)MemberTypes.FacultyMember)
                            {
                                allowedAccess = true; //If the user is a faculty member, break out because they have full access
                                reviewerAccess = false;
                                break;
                            }
                            else if (member.MemberType.ID == (int)MemberTypes.Reviewer)
                            {
                                reviewerAccess = true;  //Don't break with reviewers, because they may be faculty or committee also
                                allowedAccess = true;
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }
}
