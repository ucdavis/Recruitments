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
using CAESDO.Recruitment.Data;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_UploadFiles : ApplicationPage
    {
        private const string STR_Publication = "Publication";
        private const string STR_LetterOfRec = "LetterOfRec";
        private const string STR_References = "References";
        private const string STR_Publications = "Publications";

        public Application selectedApplication 
        {
            get {
                int appID = 0;
                
                if ( int.TryParse(dlistApplications.SelectedValue, out appID ) )
                    return ApplicationBLL.GetByID(appID);
                else
                    return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void rlistUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkUnsolicited.Visible = false;
            mViewFileType.ActiveViewIndex = int.Parse(rlistUploadType.SelectedValue);

            if (rlistUploadType.SelectedItem.Text == STR_Publications)
            {
                rptPublications.DataSource = GetFilesOfType(STR_Publication);
                rptPublications.DataBind();
            }
            else if (rlistUploadType.SelectedItem.Text == STR_References)
            {
                chkUnsolicited.Visible = true;
                rptPublications.Visible = false;
            }
            else
            {
                rptPublications.Visible = false;
            }
        }

        protected void dlistApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rlistUploadType.SelectedItem.Text == STR_Publications)
            {
                //If the publications rbtn is checked, then bind the publications grid
                rptPublications.Visible = true;
                rptPublications.DataSource = GetFilesOfType(STR_Publication);
                rptPublications.DataBind();
            }
        }

        protected void ibtnPublicationsRemoveFile_Click(object sender, EventArgs e)
        {
            int FileID = 0;
            bool success = false;

            success = int.TryParse(((ImageButton)sender).CommandArgument, out FileID);

            if (success)
            {
                File fileToDelete = FileBLL.GetByID(FileID);

                using (var ts = new TransactionScope())
                {
                    FileBLL.DeletePDF(fileToDelete);
                    
                    //Update the current application
                    Application application = selectedApplication;

                    application.Files.Remove(fileToDelete);

                    ApplicationBLL.EnsurePersistent(application);

                    ts.CommitTransaction();
                }
            }

            rptPublications.DataSource = GetFilesOfType(STR_Publication);
            rptPublications.DataBind();
        }

        /// <summary>
        /// Uploads a file of the given type into the selected application
        /// </summary>
        protected void btnfileUpload_Click(object sender, EventArgs e)
        {
            if (rlistUploadType.SelectedItem.Text == STR_References )
            {
                UploadReferences();
            }
            else if (rlistUploadType.SelectedItem.Text == STR_Publications)
            {
                UploadPublications();
            }
            else
            {
                UploadFiles();
            }
        }

        private void UploadReferences()
        {
            FileType referenceFileType = FileTypeBLL.GetByName(STR_LetterOfRec);
            Reference selectedReference = ReferenceBLL.GetByID(int.Parse(dlistReferences.SelectedValue));

            //If there is already a reference file, we need to delete it
            if (selectedReference.ReferenceFile != null)
            {
                using (var ts = new TransactionScope())
                {
                    FileBLL.DeletePDF(selectedReference.ReferenceFile);
                    selectedReference.ReferenceFile = null;

                    ReferenceBLL.EnsurePersistent(selectedReference);

                    ts.CommitTransaction();
                }
            }

            if (fileUpload.HasFile)
            {
                using (var ts = new TransactionScope())
                {
                    File file = FileBLL.SavePDF(fileUpload, referenceFileType); //FileBLL.SavePDFWithWatermark(fileUpload, referenceFileType);

                    if (file != null)
                    {
                        selectedReference.ReferenceFile = file;
                        selectedReference.UnsolicitedReference = chkUnsolicited.Checked;

                        ReferenceBLL.EnsurePersistent(selectedReference);

                        lblStatus.Text = "File Uploaded Successfully";
                    }
                    else
                    {
                        lblStatus.Text = "File Upload Did Not Succeed: Ensure That File Is A PDF File";
                    }

                    ts.CommitTransaction();
                }
            }
        }
        
        private void UploadFiles()
        {
            FileType selectedFileType = FileTypeBLL.GetByID(int.Parse(dlistFileTypes.SelectedValue));

            //For all fileTypes except for Publications we should remove existing files
            if (selectedFileType.FileTypeName != STR_Publication && selectedFileType.FileTypeName != STR_LetterOfRec)
                RemoveAllFilesOfType(selectedFileType.FileTypeName);

            File file = FileBLL.SavePDF(fileUpload, selectedFileType);

            using (var ts = new TransactionScope())
            {
                if (file != null)
                {
                    Application application = selectedApplication;

                    application.Files.Add(file);

                    ApplicationBLL.EnsurePersistent(application);

                    lblStatus.Text = "File Uploaded Successfully";
                }
                else
                {
                    lblStatus.Text = "File Upload Did Not Succeed: Ensure That File Is A PDF File";   
                }
                
                ts.CommitTransaction();
            }
        }

        private void UploadPublications()
        {
            FileType publicationsFileType = FileTypeBLL.GetByName(STR_Publication);
            Application application = selectedApplication;

            File file = FileBLL.SavePDF(fileUpload, publicationsFileType);

            using (var ts = new TransactionScope())
            {
                if (file != null)
                {
                    application.Files.Add(file);

                    ApplicationBLL.EnsurePersistent(application);

                    lblStatus.Text = "File Uploaded Successfully";
                }
                else
                {
                    lblStatus.Text = "File Upload Did Not Succeed: Ensure That File Is A PDF File";
                }

                ts.CommitTransaction();
            }

            rptPublications.DataSource = GetFilesOfType(STR_Publication);
            rptPublications.DataBind();
        }

        /// <summary>
        /// Removes all files of the given type from the current applicaiton.  This removes the files themselves,
        /// the file info entry and the application files link
        /// </summary>
        private void RemoveAllFilesOfType(string fileTypeName)
        {
            List<File> existingFiles = GetFilesOfType(fileTypeName);
            Application application = selectedApplication;

            if (existingFiles.Count != 0)
            {
                using (var ts = new TransactionScope())
                {
                    foreach (File existingFile in existingFiles)
                    {
                        application.Files.Remove(existingFile);

                        FileBLL.DeletePDF(existingFile);
                    }

                    ApplicationBLL.EnsurePersistent(application);

                    ts.CommitTransaction();
                }
            }
        }

        /// <summary>
        /// Parses down the currentApplication files list to just contain files of the correct type
        /// </summary>
        /// <param name="fileTypeName">The name of the file type desired</param>
        /// <returns>Just the currentApplication files of the given type</returns>
        private List<File> GetFilesOfType(string fileTypeName)
        {
            return FileBLL.GetByTypeAndApplication(selectedApplication, fileTypeName);
        }

        /// <summary>
        /// Returns a string that specifies the number of publications requested remaining
        /// </summary>
        /// <returns></returns>
        public string NumPublicationsRemainingText()
        {
            if (selectedApplication == null)
                return string.Empty;

            //Warn the user if they don't have enough publications
            int numPublicationsRemaining = selectedApplication.AppliedPosition.NumPublications - GetFilesOfType(STR_Publication).Count;

            if (numPublicationsRemaining > 0)
                return string.Format("[{0} More Publication{1} Requested]", numPublicationsRemaining, numPublicationsRemaining == 1 ? string.Empty : "s");
            else
                return string.Empty;
        }
}

}