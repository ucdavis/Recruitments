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

namespace CAESDO.Recruitment.Web
{
    public partial class CreateUser : System.Web.UI.Page
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
                        break;
                    default:
                        ErrorMessage.Text = "Your account was not created. Please try again.";
                        e.Cancel = true;
                        break;
                }
            }
        }
}

}