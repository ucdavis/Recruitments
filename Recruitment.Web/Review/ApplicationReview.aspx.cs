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

namespace CAESDO.Recruitment.Web
{
    public partial class Review_ApplicationReview : ApplicationPage
    {
        private const string STR_ApplicationID = "ApplicationID";
        private const string STR_FileType_Transcript = "Transcript";
        private const string STR_FileType_CoverLetter = "CoverLetter";
        private const string STR_FileType_ResearchInterests = "ResearchInterests";
        private const string STR_FileType_Publication = "Publication";

        public int currentApplicationID
        {
            get
            {
                CheckQueryString();

                //Try parsing the ApplicationID query string into an integer.
                //AppID will be number represented in the string or 0 on failure
                int appID = 0;
                int.TryParse(Request.QueryString[STR_ApplicationID], out appID);

                return appID;
            }
        }

        /// <summary>
        /// The current application just uses the daoFactory to get the application out by ID -- no Session storage used because
        /// the Dao factory manages a cache per session (might want to update that later if performance is not acceptable).e
        /// </summary>
        public Application currentApplication
        {
            get
            {
                return daoFactory.GetApplicationDao().GetById(currentApplicationID, false);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
        }

        /// <summary>
        /// Page_Init checks to ensure that the query string is valid, the logged in user is an admin or equivalent, the given application is valid
        /// </summary>
        /// <remarks>TODO: Currently any user can view any application for testing</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                currentApplication.isValid();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                //if the current application does not have a database association, redirect to an error page
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            bool allowedAccess = false;

            foreach (DepartmentMember member in currentApplication.AppliedPosition.PositionCommittee)
            {                
                if (member.LoginID == User.Identity.Name)
                    allowedAccess = true;
            }
                        
            if (!allowedAccess)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }

            //Trace.Write("Valid user and application " + currentApplication.ID.ToString() + Environment.NewLine);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Bind all of the information for the current application
                DataBindContactInformation();
                DataBindEducationInformation();
                DataBindCurrentPositionInformation();
                DataBindReferences();
                DataBindFiles();
            }
        }

        protected void lbtnPublicationFile_Click(object sender, EventArgs e)
        {
            int FileID = 0;
            bool success = false;

            success = int.TryParse(((LinkButton)sender).CommandArgument, out FileID);

            if (success)
            {
                if (DownloadFile(FileID) == false)
                {
                    eReport.ReportError(new ApplicationException("Publication File Not Found: ID=" + FileID.ToString()), "lbtnPublicationFile_Click");
                    Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.FILE));
                }
            }
        }

        protected void lbtnReferenceFile_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;

            int fileID = 0;

            if (int.TryParse(lbtn.CommandArgument, out fileID))
            {
                DownloadFile(fileID);
            }
            else
            {
                eReport.ReportError(new ApplicationException("Reference File Not Found: ID=" + fileID.ToString() ), "lbtnReferenceFile_Click");
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.FILE));
            }
        }

        protected void lbtnFileDownload_Click(object sender, EventArgs e)
        {
            int FileID = 0;
            bool success = false;

            success = int.TryParse(((LinkButton)sender).CommandArgument, out FileID);

            if (success)
            {
                if (DownloadFile(FileID) == false)
                {
                    eReport.ReportError(new ApplicationException("File Not Found: ID=" + FileID.ToString()), "lbtnFileDownload_Click");
                    Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.FILE));
                }
            }
        }

        protected void btnDownloadAll_Click(object sender, EventArgs e)
        {
            string fileName = currentApplication.AssociatedProfile.LastName + "-Application" + currentApplication.ID.ToString() + ".pdf";
            string destination = FilePath + fileName;

            if (currentApplication.Files.Count == 0)
                return;

            try
            {
                int f = 0;

                PdfReader reader = new PdfReader(FilePath + currentApplication.Files[f].ID.ToString());

                int n = reader.NumberOfPages;

                //Response.Write(string.Format("There are {0} pages in the original docuemnt", n));

                Document document = new Document(reader.GetPageSizeWithRotation(1));

                PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(destination, System.IO.FileMode.Create));

                document.Open();

                PdfContentByte cb = writer.DirectContent;
                PdfImportedPage page;
                int rotation;

                while (f < currentApplication.Files.Count)
                {
                    int i = 0;
                    while (i < n)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        page = writer.GetImportedPage(reader, i);
                        
                        rotation = reader.GetPageRotation(i);
                        
                        if (rotation == 90 || rotation == 270)
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        else
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        
                        BaseFont bf = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        cb.BeginText();
                        cb.SetFontAndSize(bf, 12f);
                        cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER,currentApplication.Files[f].FileType.FileTypeName + " -- " + currentApplication.Files[f].FileName, reader.GetPageSizeWithRotation(i).Width / 2, reader.GetPageSizeWithRotation(i).Height - 12f, 0);
                        //cb.ShowText(currentApplication.Files[f].FileType.FileTypeName);
                        cb.EndText();
                    }
                    f++;

                    if (f < currentApplication.Files.Count)
                    {
                        reader = new PdfReader(FilePath + currentApplication.Files[f].ID.ToString());

                        n = reader.NumberOfPages;
                    }
                }

                document.Close();
            }
            catch (Exception ex)
            {
                lblDownloadAllStatus.Text = "Files could not be combined successfully.";
                return;
            }

            //If we got this far, no exceptions have been made
            System.IO.FileInfo file = new System.IO.FileInfo(destination);

            Response.Clear();

            //Control the name that they see
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            Response.AddHeader("Content-Length", file.Length.ToString());
            //Response.TransmitFile(path + FileID.ToString());
            Response.TransmitFile(file.FullName);
            Response.Flush();

            //Now remove the temporary file
            file.Delete();

            Response.End();            
        }

        /// <summary>
        /// Called from the Refences Grid whenever a row is selected (Edited).
        /// Populates the reference's information into the info table and then shows the popup
        /// </summary>
        protected void gviewReferences_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView referenceGrid = sender as GridView;

            //Grab the reference from the dataKey ID
            Reference currentReference = daoFactory.GetReferenceDao().GetById((int)referenceGrid.SelectedDataKey["ID"], false);

            //Now fill in the form fields with the current reference
            txtReferencesTitle.Text = currentReference.Title;
            txtReferencesFirstName.Text = currentReference.FirstName;
            txtReferencesLastName.Text = currentReference.LastName;

            txtReferencesAcadTitle.Text = currentReference.AcadTitle;
            txtReferencesExpertise.Text = currentReference.Expertise;

            txtReferencesDepartment.Text = currentReference.Dept;
            txtReferencesInstitute.Text = currentReference.Institution;

            txtReferencesAddress1.Text = currentReference.Address1;
            txtReferencesAddress2.Text = currentReference.Address2;
            txtReferencesCity.Text = currentReference.City;
            txtReferencesState.Text = currentReference.State;
            txtReferencesZip.Text = currentReference.Zip;
            txtReferencesCountry.Text = currentReference.Country;

            txtReferencesPhone.Text = currentReference.Phone;
            txtReferencesEmail.Text = currentReference.Email;

            //Now show the popup control
            mpopupReferencesEntry.Show();
        }

        /// <summary>
        /// Check to ensure that they querystring "ApplicationID" is not null or empty
        /// </summary>
        private void CheckQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString[STR_ApplicationID]))
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
        }

        /// <summary>
        /// Downloads the file given by FileID
        /// </summary>
        /// <param name="FileID">FileID of the File to Download</param>
        /// <returns>true if file is found successfully</returns>
        private bool DownloadFile(int FileID)
        {
            string fileName = string.Empty;

            File fileToDownload = daoFactory.GetFileDao().GetById(FileID, false);

            try
            {
                fileName = fileToDownload.FileName;
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                return false; //file download did not succeed
            }

            System.IO.FileInfo file = new System.IO.FileInfo(FilePath + fileToDownload.ID.ToString());

            if (file.Exists)
            {
                Response.Clear();

                //Control the name that they see
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
                Response.AddHeader("Content-Length", file.Length.ToString());
                //Response.TransmitFile(path + FileID.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Parses down the currentApplication files list to just contain files of the correct type
        /// </summary>
        /// <param name="fileTypeName">The name of the file type desired</param>
        /// <returns>Just the currentApplication files of the given type</returns>
        private List<File> GetFilesOfType(string fileTypeName)
        {
            List<File> correctTypeFiles = new List<File>();

            foreach (File f in currentApplication.Files)
            {
                if (f.FileType.FileTypeName == fileTypeName)
                    correctTypeFiles.Add(f);
            }

            return correctTypeFiles;
        }

        /// <summary>
        /// helper method to convert CamelCaseString to Camel Case String
        /// by inserting spaces
        /// </summary>
        /// <see cref="http://www.developer.com/net/asp/article.php/3609991"/>
        protected string BreakCamelCase(string CamelString)
        {
            string output = string.Empty;
            bool SpaceAdded = true;

            for (int i = 0; i < CamelString.Length; i++)
            {
                if (CamelString.Substring(i, 1) ==
                    CamelString.Substring(i, 1).ToLower())
                {
                    output += CamelString.Substring(i, 1);
                    SpaceAdded = false;
                }
                else
                {
                    if (!SpaceAdded)
                    {
                        output += " ";
                        output += CamelString.Substring(i, 1);
                        SpaceAdded = true;
                    }
                    else
                        output += CamelString.Substring(i, 1);
                }
            }
            return output;
        }

        /// <summary>
        /// Returns true if a file exists, else false
        /// </summary>
        protected bool GetRefernceFileStatusString(int referenceID)
        {
            Reference reference = daoFactory.GetReferenceDao().GetById(referenceID, false);

            if (reference.ReferenceFile != null)
                return true;
            else
                return false;
        }

        #region DataBindingMethods

        /// <summary>
        /// Bind the profile (contact info) fields to the appropriate text boxes
        /// </summary>
        private void DataBindContactInformation()
        {
            //Grab the Profile
            Profile currentProfile = currentApplication.AssociatedProfile;

            //Set the corresponding fields
            txtContactFirstName.Text = currentProfile.FirstName;
            txtContactMiddleName.Text = currentProfile.MiddleName;
            txtContactLastName.Text = currentProfile.LastName;

            txtContactAddress1.Text = currentProfile.Address1;
            txtContactAddress2.Text = currentProfile.Address2;
            txtContactCity.Text = currentProfile.City;
            txtContactState.Text = currentProfile.State;

            txtContactPhone.Text = currentProfile.Phone;
        }

        /// <summary>
        /// Bind the education fields for the primary education result (there should only be one allowed by the program)
        /// </summary>
        private void DataBindEducationInformation()
        {
            if (!currentApplication.AppliedPosition.Steps.Contains(ApplicationStepType.Education))
            {
                pnlEducationInformation.Visible = false;
                return;
            }

            //Only databind if there is an education available
            if (currentApplication.Education.Count == 0)
                return;

            Education currentEducation = currentApplication.Education[0];

            txtEducationPHDDate.Text = currentEducation.Date.ToShortDateString();
            txtEducationDiscipline.Text = currentEducation.Discipline;
            txtEducationInstitution.Text = currentEducation.Institution;
        }

        /// <summary>
        /// Bind the current position for the primary CP result (there should only be one allowed by the program)
        /// </summary>
        private void DataBindCurrentPositionInformation()
        {
            if (!currentApplication.AppliedPosition.Steps.Contains(ApplicationStepType.CurrentPosition))
            {
                pnlCurrentPosition.Visible = false;
                return;
            }

            //Only databindd if there is a current position available
            if (currentApplication.CurrentPositions.Count == 0)
                return;

            CurrentPosition currentPosition = currentApplication.CurrentPositions[0];

            txtCurrentPositionTitle.Text = currentPosition.Title;
            txtCurrentPositionDepartment.Text = currentPosition.Department;
            txtCurrentPositionInstitution.Text = currentPosition.Institution;

            txtCurrentPositionAddress1.Text = currentPosition.Address1;
            txtCurrentPositionAddress2.Text = currentPosition.Address2;
            txtCurrentPositionCity.Text = currentPosition.City;
            txtCurrentPositionState.Text = currentPosition.State;
            txtCurrentPositionZip.Text = currentPosition.Zip;
            txtCurrentPositionCountry.Text = currentPosition.Country;
        }

        /// <summary>
        /// Bind the existing publications to the grid
        /// </summary>
        private void DataBindPublications()
        {
            //Bind the publications grid with existing files
            rptPublications.DataSource = GetFilesOfType(STR_FileType_Publication);
            rptPublications.DataBind();
        }

        /// <summary>
        /// Have to bind the current application's existing references to the grid
        /// </summary>
        private void DataBindReferences()
        {
            //Bind the Grid
            gviewReferences.DataSource = currentApplication.References;
            gviewReferences.DataBind();
        }

        /// <summary>
        /// Generates a list of all files associated with this application
        /// </summary>
        private void DataBindFiles()
        {
            List<File> fileList = new List<File>(currentApplication.Files);
            fileList.Sort(new ReviewFileTypeComparer(SortDirection.Ascending));

            gviewFiles.DataSource = fileList;
            gviewFiles.DataBind();
        }

        #endregion

}

    /// <summary>
    /// Comparer that uses the FileTypeName of a file for comparison
    /// </summary>
    public class ReviewFileTypeComparer : IComparer<File>
    {
        private SortDirection sortDirection;

        public SortDirection SortDirection
        {
            get { return this.sortDirection; }
            set { this.sortDirection = value; } 
        }

        public ReviewFileTypeComparer(SortDirection sortDirection)
        {
            this.SortDirection = sortDirection;
        }

        #region IComparer<File> Members

        public int Compare(File x, File y)
        {
            if (sortDirection == SortDirection.Ascending)
                return x.FileType.FileTypeName.CompareTo(y.FileType.FileTypeName);
            else
                return y.FileType.FileTypeName.CompareTo(x.FileType.FileTypeName);
        }

        #endregion
    }
}
