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
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;
using System.Net.Mail;
using System.Web.Configuration;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_UnsolicitedReferences : ApplicationPage
    {
        public Template UnsolicitedTemplate
        {
            get
            {
                Template unsolicitedTemplate = TemplateBLL.GetFirstByTypeName("Unsolicited");

                if (unsolicitedTemplate == null)
                {
                    throw new NullReferenceException("Unsolicited Template Not Found");
                }

                return unsolicitedTemplate;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litEmailBody.Text = UnsolicitedTemplate.TemplateText;
            }
        }

        protected void dlistApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            int applicationID = 0;

            if (!int.TryParse(dlistApplications.SelectedValue, out applicationID))
            {
                gViewReferences.Visible = false;
                btnUpdateList.Visible = false;
                reqValApplications.Validate();
                return; //Return if the selected value is not a valid applicationID
            }

            gViewReferences.Visible = true; //Show the references grid
            btnUpdateList.Visible = true;

            Application currentApplication = ApplicationBLL.GetByID(applicationID);

            gViewReferences.DataSource = currentApplication.References;
            gViewReferences.DataBind();
        }
        
        protected void btnUpdateList_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gViewReferences.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //Get the unsolicited check box and then current reference
                    CheckBox cboxUnsolicited = (CheckBox)row.FindControl("chkUnsolicited");
                    
                    int referenceID = (int)gViewReferences.DataKeys[row.RowIndex]["id"];
                    Reference currentReference = ReferenceBLL.GetByID(referenceID);

                    //Only save the information if it has changed
                    if (currentReference.UnsolicitedReference != cboxUnsolicited.Checked)
                    {
                        using (new TransactionScope())
                        {
                            currentReference.UnsolicitedReference = cboxUnsolicited.Checked;

                            ReferenceBLL.EnsurePersistent(ref currentReference);
                        }
                    }
                }
            }

            //Notify the user that the update was successful
            lblResult.Text = "Unsolicited List Updated";
        }

        /// <summary>
        /// Sends a unsolicited letter to the selected reference
        /// </summary>
        protected void sendEmail_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;

            int referenceID = int.Parse(btnSender.CommandArgument);

            Reference currentReference = ReferenceBLL.GetByID(referenceID);
                        
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage(WebConfigurationManager.AppSettings["emailFromEmail"],
                                                    currentReference.Email,
                                                    "UC Davis Recruitment Unsolicited Letter Response",
                                                    new TemplateProcessing().ProcessTemplate(currentReference, currentReference.AssociatedApplication, UnsolicitedTemplate.TemplateText, false)
                                                );
            message.IsBodyHtml = true;
            client.Send(message);

            //Record when the unsolicited email was sent out
            using (new TransactionScope())
            {
                currentReference.UnsolicitedEmailDate = DateTime.Now;
                
                ReferenceBLL.EnsurePersistent(ref currentReference);
            }

            lblResult.Text = "Email sent successfully";
        }
    }

}