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

namespace CAESDO.Recruitment.Web
{
    public partial class Review_Index : ApplicationPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            bool CommitteeMember = daoFactory.GetCommitteeMemberDao().IsUserMember(MemberTypes.AllCommittee);
            bool FacultyOrReviewMember = daoFactory.GetCommitteeMemberDao().IsUserMember(MemberTypes.FacultyMember) || daoFactory.GetCommitteeMemberDao().IsUserMember(MemberTypes.Reviewer);
            
            pnlCommitteeAccess.Visible = CommitteeMember;
            pnlFacultyAccess.Visible = FacultyOrReviewMember;
                        
            //If the user is neither, redirect them to the error page
            if (!CommitteeMember && !FacultyOrReviewMember)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}