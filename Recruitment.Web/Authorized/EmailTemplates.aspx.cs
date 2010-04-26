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

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_EmailTemplates : ApplicationPage
    {
        public TemplateType ReminderTemplateType
        {
            get
            {
                TemplateType _ReminderTemplateType = new TemplateType();
                _ReminderTemplateType.Type = "Reminder";

                return daoFactory.GetTemplateTypeDao().GetUniqueByExample(_ReminderTemplateType);
            }
        }

        public Template ReferenceTemplate
        {
            get
            {
                return daoFactory.GetTemplateDao().GetTemplatesByType(ReminderTemplateType)[0];
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
            foreach (GridViewRow row in gViewApplications.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cEmail = (CheckBox)row.FindControl("chkEmailApplicant");

                    if (cEmail.Checked)
                    {
                        //Get the user and email them
                        Application selectedApplication = daoFactory.GetApplicationDao().GetById((int)gViewApplications.DataKeys[row.RowIndex]["id"], false);
                        
                        SmtpClient client = new SmtpClient();
                        MailMessage message = new MailMessage(WebConfigurationManager.AppSettings["emailFromEmail"], 
                                                                selectedApplication.Email, 
                                                                "UC Davis Recruitment Reminder", 
                                                                new TemplateProcessing().ProcessTemplate(null, selectedApplication, ReferenceTemplate.TemplateText)
                                                            );
                        message.IsBodyHtml = true;
                        client.Send(message);
                    }
                }
            }

            //Notify the user that the emails were sent properly
            lblSentEmail.Text = "Email(s) sent successfully";
        }
}
}