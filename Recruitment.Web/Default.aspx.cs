using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using System.Web.Configuration;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class _Default : ApplicationPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Position p = PositionBLL.GetByID(1);
            Application app = ApplicationBLL.GetByID(1);
            Reference r = ReferenceBLL.GetByID(1);

            sendReferenceEmail(p, app, r);
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
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("srkirkland@ucdavis.edu", "srkirkland@ucdavis.edu", "Reference Request for " + application.AssociatedProfile.FullName, bodyText);
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

            return null;
        }
    }
}