using System;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.BLL;
using System.Text;
using System.Net.Mail;

namespace CAESDO.Recruitment.Web
{
    public partial class UploadReference : ApplicationPage
    {
        private const string STR_LetterOfRec = "LetterOfRec";
        private const string UploadReferenceErrorURL = "UploadReferenceError.aspx";
        private const string UploadReferenceFileExistsErrorURL = "UploadReferenceFileExistsError.aspx";
        private const string UploadReferenceSuccessURL = "UploadReferenceSuccess.aspx";

        public Reference currentReference
        {
            get
            {
                return ReferenceBLL.GetByUploadID(Request.QueryString["ID"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //make sure the current reference exists and doesn't have a file already
            if (currentReference == null)
            {
                Response.Redirect(UploadReferenceErrorURL);
            }
            else if (currentReference.ReferenceFile != null)
            {
                Response.Redirect(UploadReferenceFileExistsErrorURL);
            }

            if (!IsPostBack)
            {
                lblInfo.Text = "Reference for " + currentReference.AssociatedApplication.AssociatedProfile.FullName;
            }
        }

        protected void btnUploadReference_Click(object sender, EventArgs e)
        {
            this.UploadReferences();
        }

        private void UploadReferences()
        {
            FileType referenceFileType = FileTypeBLL.GetByName(STR_LetterOfRec);

            if (fileUploadReference.HasFile)
            {
                if (FileBLL.IsPostedFilePDF(fileUploadReference.PostedFile))
                {
                    var referenceFile = FileBLL.SavePDF(fileUploadReference, referenceFileType); //FileBLL.SavePDFWithWatermark(fileUploadReference, referenceFileType);

                    if (ValidateBO<File>.isValid(referenceFile))
                    {
                        currentReference.ReferenceFile = referenceFile;

                        using (var ts = new TransactionScope())
                        {
                            ReferenceBLL.EnsurePersistent(currentReference);

                            ts.CommitTransaction();
                        }
                    
                        //Send confirmation email after success -- if there are errors, ignore
                        try
                        {
                            var mail = new SmtpClient();

                            string subject = "Reference Upload Confirmation";
                            
                            StringBuilder bodyText = new StringBuilder();

                            bodyText.AppendFormat("Your reference letter for {0}, who applied to the {1} position at the University of California, has been successfully received.", currentReference.AssociatedApplication.AssociatedProfile.FullName, currentReference.AssociatedApplication.AppliedPosition.PositionTitle);
                            bodyText.AppendFormat("  We appreciate your comments.", currentReference.AssociatedApplication.AssociatedProfile.LastName);
                            
                            MailMessage message = new MailMessage(currentReference.AssociatedApplication.AppliedPosition.HREmail, currentReference.Email, subject, bodyText.ToString());
                            message.IsBodyHtml = true;

                            mail.Send(message); //Send the message

                        }
                        catch { } //Continue on failure

                        Response.Redirect(UploadReferenceSuccessURL);
                    }
                    else
                    {
                        lblUploadStatus.Text = "There was an unexpected error uploading your file";
                    }
                }
                else
                {
                    lblUploadStatus.Text = "Please upload a file in PDF format";
                }
            }
        }
    } 
}
