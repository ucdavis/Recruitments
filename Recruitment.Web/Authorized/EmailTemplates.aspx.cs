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
using System.Net.Mail;
using System.Web.Configuration;
using CAESDO.Recruitment.BLL;
using System.Text;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_EmailTemplates : ApplicationPage
    {
        public Template ReferenceTemplate
        {
            get
            {
                Template referenceTemplate = TemplateBLL.GetFirstByTypeName("Reminder");

                if (referenceTemplate == null)
                {
                    throw new NullReferenceException("Reminder Template Not Found");
                }

                return referenceTemplate;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litEmailBody.Text = ReferenceTemplate.TemplateText;
            }
        }

        /// <summary>
        /// Should loop through the email list and email everyone with a checked name
        /// </summary>
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            StringBuilder errorEmails = new StringBuilder();

            foreach (var row in lviewApplications.Items)
            {
                CheckBox cEmail = (CheckBox)row.FindControl("chkEmailApplicant");

                if (cEmail.Checked)
                {
                    //Get the user and email them
                    int userID = (int)lviewApplications.DataKeys[row.DataItemIndex]["id"];
                    Application selectedApplication = ApplicationBLL.GetByID(userID);

                    try
                    {
                        SmtpClient client = new SmtpClient();
                        MailMessage message = new MailMessage(WebConfigurationManager.AppSettings["emailFromEmail"],
                                                                selectedApplication.Email,
                                                                "UC Davis Recruitment Reminder",
                                                                new TemplateProcessing().ProcessTemplate(null, selectedApplication, ReferenceTemplate.TemplateText, false)
                                                            );
                        message.IsBodyHtml = true;
                        client.Send(message);
                    }
                    catch
                    {
                        errorEmails.AppendFormat("{0}, ", selectedApplication.Email);
                    }
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

        /// <summary>
        /// After databound, find out if we should show the send emails button
        /// </summary>
        protected void lviewApplications_DataBound(object sender, EventArgs e)
        {
            if (lviewApplications.Items.Count > 0)
                btnSendEmail.Visible = true;
            else
                btnSendEmail.Visible = false;
        }

        public string GetNullSafeFullName(string fullName)
        {
            return ApplicantBLL.GetNullSafeFullName(fullName);
        }
}
}