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
using System.Data.SqlClient;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Web
{
    public partial class CreateUser : ApplicationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void wizCreateUser_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            MembershipCreateStatus status;

            //Only proceed if all of the fields are valid
            if (EmailRequired.IsValid && PasswordCompare.IsValid && PasswordRequired.IsValid && QuestionRequired.IsValid && AnswerRequired.IsValid)
            {

                Membership.CreateUser("SELFCREATED", Password.Text, Email.Text, Question.Text, Answer.Text, true, out status);

                switch (status)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                        ErrorMessage.Text = "The e-mail address that you entered is already in use. Please enter a different e-mail address.";
                        e.Cancel = true;
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        ErrorMessage.Text = string.Format("Password length minimum: {0}. Non-alphanumeric characters required: {1}.", Membership.MinRequiredPasswordLength, Membership.MinRequiredNonAlphanumericCharacters);
                        e.Cancel = true;
                        break;
                    case MembershipCreateStatus.Success:
                        createProfileForUser(Email.Text);
                        FormsAuthentication.RedirectFromLoginPage(Email.Text, false);
                        Response.Redirect(FormsAuthentication.DefaultUrl);
                        break;
                    default:
                        ErrorMessage.Text = "Your account was not created. Please try again.";
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void createProfileForUser(string email)
        {
            Applicant newUser = daoFactory.GetApplicantDao().GetApplicantByEmail(email);

            if (newUser == null)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
                return;
            }

            //Create a blank profile for the logged in user
            Profile blankProfile = new Profile();
            blankProfile.AssociatedApplicant = newUser;

            blankProfile.FirstName = string.Empty;
            blankProfile.LastName = string.Empty;
            blankProfile.Address1 = string.Empty;
            blankProfile.City = string.Empty;
            blankProfile.State = string.Empty;
            
            blankProfile.LastUpdated = null;

            daoFactory.GetProfileDao().Save(blankProfile);
        }
}

}