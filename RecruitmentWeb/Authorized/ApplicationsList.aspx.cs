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
using CAESDO.Recruitment.Core.Abstractions;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_ApplicationsList : ApplicationPage
    {
        private const string STR_ApplicationReview = "ApplicationReview.aspx";

        public Position currentPosition
        {
            get
            {
                if (dlistPositions.SelectedIndex == 0)
                    return null;
                else
                    return PositionBLL.GetByID(int.Parse(dlistPositions.SelectedValue));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetNullSafeFullName(string fullName)
        {
            return ApplicantBLL.GetNullSafeFullName(fullName);
        }

        protected void ObjectDataApplications_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["position"] = currentPosition;
        }

        protected void odsReferencesToNotify_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["position"] = currentPosition;
        }

        protected void dlistPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lviewApplications.DataBind();

            if (lviewApplications.Items.Count > 0)
            {
                pnlPositionSelected.Visible = true;

                litReferenceTemplate.Text = currentPosition.ReferenceTemplate.TemplateText;

                lviewReferencesToBeNotified.DataBind();
                //var allReferences = ReferenceBLL.GetReferencesToBeNotified(currentPosition);
                //btnUpdateList.Visible = true;
                //btnEmailReferences.Visible = true;
            }
            else
            {
                pnlPositionSelected.Visible = false;
                //btnUpdateList.Visible = false;
                //btnEmailReferences.Visible = false;
            }
        }

        protected void btnUpdateList_Click(object sender, EventArgs e)
        {
            using (var ts = new TransactionScope())
            {
                foreach (var row in lviewApplications.Items)
                {
                        int applicationID = (int)lviewApplications.DataKeys[row.DataItemIndex]["id"];
                        Application app = ApplicationBLL.GetByID(applicationID);

                        app.InterviewList = ((CheckBox)row.FindControl("chkShortList")).Checked;
                        app.GetReferences = ((CheckBox)row.FindControl("chkReferences")).Checked;
                        app.NoConsideration = ((CheckBox)row.FindControl("chkNoConsideration")).Checked;

                        ApplicationBLL.EnsurePersistent(app);
                }

                ts.CommitTransaction();
            }

            lblResult.Text = "Application List Updated";
            lviewReferencesToBeNotified.DataBind();
        }

        protected void lbtnViewApplication_Click(object sender, EventArgs e)
        {
            string applicationID = ((LinkButton)sender).CommandArgument;

            Response.Redirect(string.Format("{0}?ApplicationID={1}", STR_ApplicationReview, applicationID));
        }

        /// <summary>
        /// For each application with GetReferences = true, all references are emailed (unless they have been emailed previously)
        /// </summary>
        protected void btnEmailReferences_Click(object sender, EventArgs e)
        {
            if (currentPosition == null)
                return;

            btnUpdateList_Click(sender, e); //Always update the list before sending emails to avoid confusion

            List<string> errorReferences = new List<string>();

            var referencesToNotify = ReferenceBLL.GetReferencesToBeNotified(currentPosition);

            foreach (var reference in referencesToNotify)
            {
                //Before sending the email, make sure we have a uid
                this.ensureUniqueID(reference);

                string result = sendReferenceEmail(currentPosition, reference.AssociatedApplication, reference);

                //If a result was returned, then add it to the error emails
                if (!string.IsNullOrEmpty(result))
                    errorReferences.Add(result);
            }

            if (errorReferences.Count == 0)
            {
                lblResult.ForeColor = System.Drawing.Color.Green;
                lblResult.Text = "All References Successfully Emailed";
            }
            else
            {
                StringBuilder delimitedReferenceErrorList = new StringBuilder();

                foreach (string s in errorReferences)
                {
                    if (delimitedReferenceErrorList.Length != 0)
                        delimitedReferenceErrorList.Append(", "); //Add a comma if there are existing emails in the list

                    delimitedReferenceErrorList.Append(s);
                }

                lblResult.ForeColor = System.Drawing.Color.Red;
                lblResult.Text = "The Following References Were Not Sent Emails: " + delimitedReferenceErrorList.ToString();
            }

            lviewReferencesToBeNotified.DataBind();
        }

        /// <summary>
        /// Returns immediately if a uniqueID exists for the given reference, else it generates and saves a new one
        /// into the existing reference's record
        /// </summary>
        private void ensureUniqueID(Reference reference)
        {
            if (string.IsNullOrEmpty(reference.UploadID))
            {                
                //We don't have an uploadID, so create a new GUID and assign it to the reference
                using (var ts = new TransactionScope())
                {
                    reference.UploadID = Guid.NewGuid().ToString();

                    ReferenceBLL.EnsurePersistent(reference);

                    ts.CommitTransaction();
                }
            }
        }

        /// <summary>
        /// Send a email from the given positions's HR Rep to the given reference's email account.  On success the reference is marked as being
        /// sent the email so she will not be sent another.  The automated email account is BCC'd for reference.
        /// </summary>
        /// <returns>Returns null on success, else returns the name of the offending reference and applicant</returns>
        private string sendReferenceEmail(Position position, Application application, Reference reference)
        {
            TemplateProcessing template = new TemplateProcessing();

            //Process the template to get the body text of the email
            string bodyText = template.ProcessTemplate(reference, application, position.ReferenceTemplate.TemplateText, true);

            //Exchange Ops is commented out because it will not send HTML emails currently (also needs MSXML2)
            //Now configure the email host
            //CAESDO.ExchangeOps exops = new ExchangeOps();
            //exops.ConfigureServer(WebConfigurationManager.AppSettings["ServerName"], WebConfigurationManager.AppSettings["Protocol"]);
            //exops.ConfigureEmail(WebConfigurationManager.AppSettings["emailDomainUserName"], WebConfigurationManager.AppSettings["emailUserName"], WebConfigurationManager.AppSettings["emailPassword"]);

            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();

            //Try to send the email -- if there are errors, return the email of the offending reference
            try
            {
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(currentPosition.HREmail, reference.Email, "Reference Request for " + application.AssociatedProfile.FullName, bodyText);
                message.Bcc.Add(WebConfigurationManager.AppSettings["AppMailTo"]); //BCC the recruitments email account
                message.IsBodyHtml = true;

                //System.IO.FileStream descriptionStream = new System.IO.FileStream(FilePath + position.DescriptionFile.ID, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                
                //message.Attachments.Add(new System.Net.Mail.Attachment(descriptionStream, position.DescriptionFile.FileName));

                mail.Send(message);

                //After the message is sent, close the stream.
                //descriptionStream.Close();

                //exops.SendEmail(reference.Email, "Reference Request for Application" + position.PositionTitle, bodyText, WebConfigurationManager.AppSettings["emailFromEmail"]);
            }
            catch (Exception)
            {
                return string.Format("{0} ({1})", reference.FullName, application.AssociatedProfile.FullName);
            }
                        
            //No errors, so save the fact that we sent an email to the current reference
            using (var ts = new TransactionScope())
            {
                reference.SentEmail = true;
                reference.EmailDate = DateTime.Now;

                ReferenceBLL.EnsurePersistent(reference);

                ts.CommitTransaction();
            }

            return null;
        }
    }
}