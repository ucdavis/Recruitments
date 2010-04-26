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
    public partial class Shared_ApplicationReview : System.Web.UI.UserControl
    {
        private const string STR_ApplicationID = "ApplicationID";
        private const string STR_FileType_Transcript = "Transcript";
        private const string STR_FileType_CoverLetter = "CoverLetter";
        private const string STR_FileType_ResearchInterests = "ResearchInterests";
        private const string STR_FileType_Publication = "Publication";
        private const int INT_REFERENCE_FILE_COLUMN = 7;

        public bool AdministrativeAccess { get; set; }

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
                return ApplicationBLL.GetNullableByID(currentApplicationID);
            }
        }

        /// <summary>
        /// Page_Init checks to ensure that the query string is valid, the logged in user is an admin or equivalent, the given application is valid
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (currentApplication == null)
            {
                //if the current application does not have a database association, redirect to an error page
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            if (AdministrativeAccess) //Only allow in administrative access
            {
                //Check User Permissions if the user isn't an admin
                if (!Roles.IsUserInRole("Admin"))
                {
                    if (PositionBLL.VerifyPositionAccess(currentApplication.AppliedPosition) == false)
                    {
                        //If the user does not have position access, redirect to the not authorized page
                        Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
                    }
                }
            }
            else //Use committee rules
            {
                bool allowedAccess = false;
                bool reviewerAccess = false;

                CommitteeMemberBLL.CheckAccess(currentApplication.AppliedPosition, out allowedAccess, out reviewerAccess);

                if (!allowedAccess)
                {
                    Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
                }

                if (reviewerAccess)
                    gviewReferences.Columns[INT_REFERENCE_FILE_COLUMN].Visible = false;
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
                    throw new ApplicationException("Publication File Not Found: ID=" + FileID.ToString());
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
                throw new ApplicationException("Reference File Not Found: ID=" + fileID.ToString());
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
                    throw new ApplicationException("Publication File Not Found: ID=" + FileID.ToString());
                }
            }
        }

        protected void btnDownloadAll_Click(object sender, EventArgs e)
        {
            bool success = FileBLL.DownloadByApplication(currentApplication);

            if (!success)
            {
                lblDownloadAllStatus.Text = "Files could not be combined successfully.";
            }
        }

        /// <summary>
        /// Called from the Refences Grid whenever a row is selected (Edited).
        /// Populates the reference's information into the info table and then shows the popup
        /// </summary>
        protected void gviewReferences_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView referenceGrid = sender as GridView;

            //Grab the reference from the dataKey ID
            int currentReferenceID = (int)referenceGrid.SelectedDataKey["ID"];
            Reference currentReference = ReferenceBLL.GetByID(currentReferenceID);

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
            File fileToDownload = FileBLL.GetNullableByID(FileID);

            if (fileToDownload == null)
            {
                return false;
            }
            else
            {
                return FileBLL.Transmit(fileToDownload.ID, fileToDownload.FileName);
            }
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
            return UtilsBLL.BreakCamelCase(CamelString);
        }

        /// <summary>
        /// Returns true if a file exists, else false
        /// </summary>
        protected bool GetRefernceFileStatusString(int referenceID)
        {
            Reference reference = ReferenceBLL.GetByID(referenceID);

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
            txtContactZip.Text = currentProfile.Zip;

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

            gviewFiles.DataSource = FileBLL.Sort(fileList, SortDirection.Ascending);
            gviewFiles.DataBind();
        }

        #endregion

    } 
}
