using System;
using System.Web.UI.WebControls;
using CAESDO.Recruitment.Core.Abstractions;
using CAESDO.Recruitment.Core.Domain;
using System.Web.Configuration;
using CAESDO.Recruitment.BLL;
using System.Text;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_EmailTemplates : ApplicationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// After databound, find out if we should show the send emails button
        /// </summary>
        protected void lviewApplications_DataBound(object sender, EventArgs e)
        {
            pnlApplicationsExist.Visible = lviewApplications.Items.Count > 0;
        }

        public string GetNullSafeFullName(string fullName)
        {
            return ApplicantBLL.GetNullSafeFullName(fullName);
        }

        /// <summary>
        /// Send an email to the checked applicants based on whatever template is currently selected (in txtEmailTemplate)
        /// </summary>
        protected void btnSendTemplate_Click(object sender, EventArgs e)
        {
            StringBuilder errorEmails = new StringBuilder();

            //Go through each of the applications
            foreach (var row in lviewApplications.Items)
            {
                CheckBox cEmail = (CheckBox)row.FindControl("chkEmailApplicant");

                if (cEmail.Checked) //Should we send to this applicant?
                {
                    //Get the user and email them
                    int userID = (int)lviewApplications.DataKeys[row.DataItemIndex]["id"];
                    Application selectedApplication = ApplicationBLL.GetByID(userID);

                    var bodyFromTemplate = new TemplateProcessing().ProcessTemplate(null, selectedApplication,
                                                                                    txtEmailTemplate.Text,
                                                                                    false);

                    bool success = MessageBLL.SendMessage(WebConfigurationManager.AppSettings["emailFromEmail"],
                                                            selectedApplication.Email,
                                                            "UC Davis Recruitment Message",
                                                            bodyFromTemplate);

                    if (success == false) errorEmails.AppendFormat("{0}, ", selectedApplication.Email);
                }
            }

            //Notify the user of the message results
            if (errorEmails.Length != 0)
            {
                errorEmails.Remove(errorEmails.Length - 2, 2);
                lblSentEmail.Text = string.Format("Could not send email to the following address(es): {0}", errorEmails.ToString());
            }
            else
            {
                lblSentEmail.Text = "Email(s) sent successfully";
            }
        }
    }
}