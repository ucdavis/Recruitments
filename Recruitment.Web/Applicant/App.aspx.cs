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

namespace CAESDO.Recruitment.Web
{
    public partial class App : ApplicationPage
    {
        #region ConstVariables
        private const string STR_ApplicationID = "ApplicationID";
        private const string STR_CurrentApplication = "currentApplication";
        private const string STR_ApplicationSteps = "ApplicationSteps";
        private const string STR_HomeStep = "Home";
                                                   private const string STR_Applicationpdf = "application/pdf"; 
        #endregion

        #region Properties
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
                //Now make sure the application in session matches the query string and that it is not null
                //if ( Session[STR_CurrentApplication] == null || ((Application)Session[STR_CurrentApplication]).ID != currentApplicationID )
                //{
                //    //grab the application from the database
                //    Session[STR_CurrentApplication] = daoFactory.GetApplicationDao().GetById(currentApplicationID, false);
                //}

                //NHibernateSessionManager.Instance.EnsureFreshness(Session[STR_CurrentApplication]);

                //return (Application)Session[STR_CurrentApplication];

                return daoFactory.GetApplicationDao().GetById(currentApplicationID, false);
            }
        }

        public List<Step> ApplicationSteps
        {
            get
            {
                if (Session[STR_ApplicationSteps] == null)
                    return null;  //Session[STR_ApplicationSteps] = new List<Step>();

                return (List<Step>)Session[STR_ApplicationSteps];
            }

            set
            {
                Session[STR_ApplicationSteps] = value;
            }
        } 
        #endregion

        #region Page Methods

        /// <summary>
        /// Page_Init checks to ensure that the query string is valid, the logged in user is an applicant, the given application is valid
        /// and the logged-in user is the owner of this application
        /// </summary>
        /// <remarks>Currently any membership user can view any application for testing</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
            Applicant loggedInUser = daoFactory.GetApplicantDao().GetApplicantByEmail(HttpContext.Current.User.Identity.Name);

            //Make sure the loggedInUser has an Applicant account
            if (loggedInUser == null)
            {
                FormsAuthentication.SignOut(); //Causes the user to sign out and redirect
                FormsAuthentication.RedirectToLoginPage(STR_ApplicationID + currentApplicationID.ToString()); //Make the user log in
                return;
            }

            try
            {
                currentApplication.isValid();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                //if the current application does not have a database association, redirect to an error page
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            Response.Write("Valid user and application " + currentApplication.ID.ToString() + "<br />");

            if (currentApplication.AssociatedProfile.AssociatedApplicant.Email != loggedInUser.Email)
                Response.Write("User trying to access incorrect application");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ApplicationSteps = null; //Clear the steps list on first visit

            //Load the application steps
            LoadSteps();

            rptSteps.DataSource = ApplicationSteps;
            rptSteps.DataBind();
        }

        #endregion

        #region Object Event Handlers

        /// <summary>
        /// Called whenever a tab is clicked -- it will take the user to that tab's content, and change the tab's style to
        /// selected (and all others to unselected)
        /// </summary>
        /// <remarks>The lbtnStep has a command argument which represents the selected step name</remarks>
        protected void lbtnStep_Click(object sender, EventArgs e)
        {
            Trace.Write("-- lbtnStep_Click Begin --");

            //Grab the link button's command argument (the step name)
            string stepName = ((LinkButton)sender).CommandArgument;

            MakeStepActive(stepName);

            DataBindStep(stepName);

            Trace.Write("-- lbtnStep_Click End --");
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
                    //Error downloading file
                }
            }
        }

        /// <summary>
        /// Saves the contact information entered into the form and then changes back to the home screen
        /// </summary>
        protected void btnContactSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return; //Error message

            //Grab the user's profile and update the fields
            Profile currentProfile = currentApplication.AssociatedProfile;

            currentProfile.FirstName = txtContactFirstName.Text;
            currentProfile.MiddleName = txtContactMiddleName.Text;
            currentProfile.LastName = txtContactLastName.Text;

            currentProfile.Address1 = txtContactAddress1.Text;
            currentProfile.Address2 = txtContactAddress2.Text;
            currentProfile.City = txtContactCity.Text;
            currentProfile.State = txtContactState.Text;
            currentProfile.Phone = txtContactPhone.Text;

            //currentProfile.Country = null;
            //currentProfile.CountryCode = null;

            //Update the LastUpdated property (this is how we know a profile has been updated/completed)
            currentProfile.LastUpdated = DateTime.Now;

            //Ensure the unsaved profile is valid before saving
            if (ValidateBO<Profile>.isValid(currentProfile))
            {
                using (new NHibernateTransaction())
                {
                    daoFactory.GetProfileDao().SaveOrUpdate(currentProfile);
                }
            }
            else
            {
                Trace.Warn("Profile Not Valid");
                Trace.Warn(ValidateBO<Profile>.GetValidationResultsAsString(currentProfile));
                //Error message
            }

            ReloadStepListAndSelectHome();

        }

        protected void btnEducationSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return; //Error message

            //Grab the existing education if available, else create a new one
            Education currentEducation = new Education();

            if (currentApplication.Education.Count != 0) //if there is an existing education result
                currentEducation = currentApplication.Education[0];

            //Now set the fields
            currentEducation.Date = DateTime.Parse(txtEducationPHDDate.Text);
            currentEducation.Institution = txtEducationInstitution.Text;
            currentEducation.Discipline = txtEducationDiscipline.Text;

            //Associate this with the current application
            currentEducation.AssociatedApplication = currentApplication;

            currentEducation.Complete = true; //Make sure to complete on save

            //Ensure the education is valid before saving
            if (ValidateBO<Education>.isValid(currentEducation))
            {
                using (new NHibernateTransaction())
                {
                    daoFactory.GetEducationDao().SaveOrUpdate(currentEducation);
                    currentApplication.Education.Add(currentEducation);
                }
            }
            else
            {
                Trace.Warn(ValidateBO<Education>.GetValidationResultsAsString(currentEducation));
            }

            ReloadStepListAndSelectHome();
        }

        protected void btnCurrentPositionSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return; //Error message

            //Grab the existing current position if available, else create a new one
            CurrentPosition currentPosition = new CurrentPosition();

            if (currentApplication.CurrentPositions.Count != 0) //if there is an existing result
                currentPosition = currentApplication.CurrentPositions[0];

            //Now set all of the fields
            currentPosition.Title = txtCurrentPositionTitle.Text;
            currentPosition.Department = txtCurrentPositionDepartment.Text;
            currentPosition.Institution = txtCurrentPositionInstitution.Text;

            currentPosition.Address1 = txtCurrentPositionAddress1.Text;
            currentPosition.Address2 = txtCurrentPositionAddress2.Text;
            currentPosition.City = txtCurrentPositionCity.Text;
            currentPosition.State = txtCurrentPositionState.Text;
            currentPosition.Zip = txtCurrentPositionZip.Text;
            currentPosition.Country = txtCurrentPositionCountry.Text;

            //Associate with the current application and set to complete
            currentPosition.AssociatedApplication = currentApplication;
            currentPosition.Complete = true;

            //Ensure the education is valid before saving
            if (ValidateBO<CurrentPosition>.isValid(currentPosition))
            {
                using (new NHibernateTransaction())
                {
                    daoFactory.GetCurrentPositionDao().SaveOrUpdate(currentPosition);
                    currentApplication.CurrentPositions.Add(currentPosition);
                }
            }
            else
            {
                Trace.Warn(ValidateBO<CurrentPosition>.GetValidationResultsAsString(currentPosition));
            }

            ReloadStepListAndSelectHome();
        }

        protected void btnResumeUpload_Click(object sender, EventArgs e)
        {
            FileType resumeFileType = daoFactory.GetFileTypeDao().GetFileTypeByName("Resume");

            RemoveAllFilesOfType("Resume");

            if (fileResume.HasFile)
            {
                if (fileResume.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File resume = new File();

                    resume.FileName = fileResume.FileName;
                    resume.FileType = resumeFileType;

                    using (new NHibernateTransaction())
                    {
                        resume = daoFactory.GetFileDao().Save(resume);
                    }

                    if (ValidateBO<File>.isValid(resume))
                    {
                        fileResume.SaveAs(FilePath + resume.ID.ToString());

                        currentApplication.Files.Add(resume);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                        }
                    }
                    else
                    {
                        Trace.Warn(ValidateBO<File>.GetValidationResultsAsString(resume));
                    }
                }
            }

            ReloadStepListAndSelectHome();
        }

        protected void btnCoverLetterUpload_Click(object sender, EventArgs e)
        {
            FileType coverLetterFileType = daoFactory.GetFileTypeDao().GetFileTypeByName("CoverLetter");

            RemoveAllFilesOfType("CoverLetter");

            if (fileCoverLetter.HasFile)
            {
                if (fileCoverLetter.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File coverLetter = new File();

                    coverLetter.FileName = fileCoverLetter.FileName;
                    coverLetter.FileType = coverLetterFileType;

                    using (new NHibernateTransaction())
                    {
                        coverLetter = daoFactory.GetFileDao().Save(coverLetter);
                    }

                    if (ValidateBO<File>.isValid(coverLetter))
                    {
                        fileCoverLetter.SaveAs(FilePath + coverLetter.ID.ToString());

                        currentApplication.Files.Add(coverLetter);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                        }
                    }
                    else
                    {
                        Trace.Warn(ValidateBO<File>.GetValidationResultsAsString(coverLetter));
                    }
                }
            }

            ReloadStepListAndSelectHome();
        }

        protected void btnResearchInterestsUpload_Click(object sender, EventArgs e)
        {
            FileType ResearchInterestsFileType = daoFactory.GetFileTypeDao().GetFileTypeByName("ResearchInterests");

            RemoveAllFilesOfType("ResearchInterests");

            if (fileResearchInterests.HasFile)
            {
                if (fileResearchInterests.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File researchInterests = new File();

                    researchInterests.FileName = fileResearchInterests.FileName;
                    researchInterests.FileType = ResearchInterestsFileType;

                    using (new NHibernateTransaction())
                    {
                        researchInterests = daoFactory.GetFileDao().Save(researchInterests);
                    }

                    if (ValidateBO<File>.isValid(researchInterests))
                    {
                        fileResearchInterests.SaveAs(FilePath + researchInterests.ID.ToString());

                        currentApplication.Files.Add(researchInterests);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                        }
                    }
                    else
                    {
                        Trace.Warn(ValidateBO<File>.GetValidationResultsAsString(researchInterests));
                    }
                }
            }

            ReloadStepListAndSelectHome();
        }

        protected void btnTranscriptsUpload_Click(object sender, EventArgs e)
        {
            FileType transcriptsFileType = daoFactory.GetFileTypeDao().GetFileTypeByName("Transcript");

            RemoveAllFilesOfType("Transcript");

            if (fileTranscripts.HasFile)
            {
                if (fileTranscripts.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File transcripts = new File();

                    transcripts.FileName = fileTranscripts.FileName;
                    transcripts.FileType = transcriptsFileType;

                    using (new NHibernateTransaction())
                    {
                        transcripts = daoFactory.GetFileDao().Save(transcripts);
                    }

                    if (ValidateBO<File>.isValid(transcripts))
                    {
                        fileTranscripts.SaveAs(FilePath + transcripts.ID.ToString());

                        currentApplication.Files.Add(transcripts);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                        }
                    }
                    else
                    {
                        Trace.Warn(ValidateBO<File>.GetValidationResultsAsString(transcripts));
                    }
                }
            }

            ReloadStepListAndSelectHome();
        }
        
        protected void btnDissertationUpload_Click(object sender, EventArgs e)
        {
            FileType dissertationFileType = daoFactory.GetFileTypeDao().GetFileTypeByName("Dissertation");

            RemoveAllFilesOfType("Dissertation");

            if (fileDissertation.HasFile)
            {
                if (fileDissertation.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File dissertation = new File();

                    dissertation.FileName = fileDissertation.FileName;
                    dissertation.FileType = dissertationFileType;

                    using (new NHibernateTransaction())
                    {
                        dissertation = daoFactory.GetFileDao().Save(dissertation);
                    }

                    if (ValidateBO<File>.isValid(dissertation))
                    {
                        fileDissertation.SaveAs(FilePath + dissertation.ID.ToString());

                        currentApplication.Files.Add(dissertation);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                        }
                    }
                    else
                    {
                        Trace.Warn(ValidateBO<File>.GetValidationResultsAsString(dissertation));
                    }
                }
            }

            ReloadStepListAndSelectHome();
        }

        protected void btnPublicationsUpload_Click(object sender, EventArgs e)
        {
            FileType publicationsFileType = daoFactory.GetFileTypeDao().GetFileTypeByName("Publication");

            if (filePublications.HasFile)
            {
                if (filePublications.PostedFile.ContentType == STR_Applicationpdf)
                {
                    File publication = new File();

                    publication.FileName = filePublications.FileName;
                    publication.FileType = publicationsFileType;

                    using (new NHibernateTransaction())
                    {
                        publication = daoFactory.GetFileDao().Save(publication);
                    }

                    if (ValidateBO<File>.isValid(publication))
                    {
                        filePublications.SaveAs(FilePath + publication.ID.ToString());

                        currentApplication.Files.Add(publication);

                        using (new NHibernateTransaction())
                        {
                            daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                        }
                    }
                    else
                    {
                        Trace.Warn(ValidateBO<File>.GetValidationResultsAsString(publication));
                    }
                }
            }

            DataBindPublications();
        }

        protected void ibtnPublicationsRemoveFile_Click(object sender, EventArgs e)
        {
            int FileID = 0;
            bool success = false;

            success = int.TryParse(((ImageButton)sender).CommandArgument, out FileID);

            if (success)
            {
                File fileToDelete = daoFactory.GetFileDao().GetById(FileID, false);

                using (new NHibernateTransaction())
                {
                    currentApplication.Files.Remove(fileToDelete);
                    daoFactory.GetFileDao().Delete(fileToDelete);

                    //Delete the file from the file system
                    System.IO.FileInfo file = new System.IO.FileInfo(FilePath + fileToDelete.ID.ToString());
                    file.Delete();

                    //Update the current application
                    daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                }
            }

            DataBindPublications();
        }

        protected void chkPublicationsFinalize_CheckedChanged(object sender, EventArgs e)
        {
            using (new NHibernateTransaction())
            {
                currentApplication.PublicationsComplete = chkPublicationsFinalize.Checked;
            }

            ReloadStepListAndSelectHome();
        }

        #endregion

        #region PrivateFunctions

        /// <summary>
        /// Find the View that corresponds to this step 
        /// </summary>
        /// <remarks>ViewStepID is 'view' plus the StepName with no spaces</remarks>
        private void SetCorrespondingView(string stepName)
        {
            string viewStepName = "view" + stepName.Replace(" ", "");

            View viewStep = mviewSteps.FindControl(viewStepName) as View;

            if (viewStep != null)
            {
                mviewSteps.SetActiveView(viewStep);
            }
        }

        /// <summary>
        /// Reloads the step list and then makes the home step active
        /// </summary>
        private void ReloadStepListAndSelectHome()
        {
            //Reload the steps list
            ApplicationSteps = null;
            LoadSteps();

            //Once you save the profile, set the active view back to the Home screen
            MakeStepActive(STR_HomeStep);
        }

        /// <summary>
        /// Loads up all application steps, along with state, into a List for the sidebar
        /// </summary>
        /// <remarks>
        /// Method signature for Step() : public Step(string stepName, bool completed, bool selected, bool stepVisible )
        /// </remarks>
        private void LoadSteps()
        {
            Trace.Write("Begin Step Load");

            //No need to load the steps if there are already steps available
            if (ApplicationSteps != null)
                return;

            ApplicationSteps = new List<Step>();

            //First add the home step
            ApplicationSteps.Add(new Step(STR_HomeStep, true, true, true));

            //Now add the contact information (information is 'complete' if the LastUpdated field is not null)
            ApplicationSteps.Add(new Step("Contact Information", currentApplication.AssociatedProfile.LastUpdated != null, false, true));

            //Add education
            ApplicationSteps.Add(new Step("Education Information", currentApplication.isComplete(ApplicationStepType.Education), false, true));

            //Add references
            ApplicationSteps.Add(new Step("References", currentApplication.isComplete(ApplicationStepType.References), false, true));

            //Add current position
            ApplicationSteps.Add(new Step("Current Position", currentApplication.isComplete(ApplicationStepType.CurrentPosition), false, true));

            //Add files 
            bool hasResume = false;
            bool hasCoverLetter = false;
            bool hasCV = false;
            bool hasTranscript = false;
            bool hasResearchInterest = false;
            bool hasPublication = false;
            bool hasDissertation = false;

            //Go through each file in the currentApplication and find out if each filetype is available
            foreach (File file in currentApplication.Files)
            {
                switch (file.FileType.FileTypeName)
                {
                    case "Resume":
                        hasResume = true;
                        break;
                    case "CoverLetter":
                        hasCoverLetter = true;
                        break;
                    case "CV":
                        hasCV = true;
                        break;
                    case "Transcript":
                        hasTranscript = true;
                        break;
                    case "ResearchInterests":
                        hasResearchInterest = true;
                        break;
                    case "Publication":
                        hasPublication = true;
                        break;
                    case "Dissertation":
                        hasDissertation = true;
                        break;
                    default:
                        break;
                }
            }

            //Now add each type of file, hiding if necessary
            ApplicationSteps.Add(new Step("Resume", hasResume, false, true));
            ApplicationSteps.Add(new Step("Cover Letter", hasCoverLetter, false, true));
            ApplicationSteps.Add(new Step("Research Interests", hasResearchInterest, false, true));
            ApplicationSteps.Add(new Step("Transcripts", hasTranscript, false, true));
            ApplicationSteps.Add(new Step("Publications", currentApplication.PublicationsComplete, false, true));
            ApplicationSteps.Add(new Step("Dissertation", hasDissertation, false, true));

            //Add the confidential survey
            ApplicationSteps.Add(new Step("Confidential Survey", currentApplication.isComplete(ApplicationStepType.Survey), false, true));

            //ApplicationSteps.Add(new Step("Education Information", true, false, true));

            Trace.Write("End Step Load");
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
        /// Returns a string that specifies the number of publications requested remaining
        /// </summary>
        /// <returns></returns>
        public string NumPublicationsRemainingText()
        {
            //Warn the user if they don't have enough publications
            int numPublicationsRemaining = currentApplication.AppliedPosition.NumPublications - GetFilesOfType("Publication").Count;

            if (numPublicationsRemaining > 0)
                return string.Format("[{0} More Publication{1} Requested]", numPublicationsRemaining, numPublicationsRemaining == 1 ? string.Empty : "s");
            else
                return string.Empty;
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
        /// Removes all files of the given type from the current applicaiton.  This removes the files themselves,
        /// the file info entry and the application files link
        /// </summary>
        private void RemoveAllFilesOfType(string fileTypeName)
        {
            List<File> existingFiles = GetFilesOfType(fileTypeName);

            if (existingFiles.Count != 0)
            {
                using (new NHibernateTransaction())
                {
                    foreach (File existingFile in existingFiles)
                    {
                        currentApplication.Files.Remove(existingFile);
                        daoFactory.GetFileDao().Delete(existingFile);

                        //Delete the file from the file system
                        System.IO.FileInfo file = new System.IO.FileInfo(FilePath + existingFile.ID.ToString());
                        file.Delete();
                    }

                    daoFactory.GetApplicationDao().SaveOrUpdate(currentApplication);
                }
            }
        }

        /// <summary>
        /// Sets a step to be the active step (in style and active view)
        /// </summary>
        /// <param name="stepName">The name of the step to make active</param>
        private void MakeStepActive(string stepName)
        {
            foreach (Step step in ApplicationSteps)
            {
                if (step.StepName == stepName)
                {
                    step.setSelectionStatus(true);

                    SetCorrespondingView(stepName);
                }
                else
                {
                    step.setSelectionStatus(false);
                }
            }

            rptSteps.DataSource = ApplicationSteps;
            rptSteps.DataBind();
        }

        /// <summary>
        /// Handles DataBinding (filling in fields/datagrids/etc) for the given step
        /// </summary>
        /// <param name="stepName">The name of the step to databind</param>
        /// <remarks>This allows for on-demand binding of data, but will reset the data in the given tab (back to the DB values)</remarks>
        private void DataBindStep(string stepName)
        {
            switch (stepName)
            {
                case "Home":
                    break;
                case "Contact Information":
                    DataBindContactInformation();
                    break;
                case "Education Information":
                    DataBindEducationInformation();
                    break;
                case "References":
                    break;
                case "Current Position":
                    DataBindCurrentPositionInformation();
                    break;
                case "Resume":
                    break;
                case "Cover Letter":
                    break;
                case "Research Interests":
                    break;
                case "Transcripts":
                    break;
                case "Confidential Survey":
                    break;
                case "Publications":
                    DataBindPublications();
                    break;
                default:
                    break;
            }
        }

        #region Step DataBinding Methods

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
            //Set the number of required publications
            litPublicationsNum.Text = currentApplication.AppliedPosition.NumPublications.ToString();
            
            //Set the publications complete check box
            chkPublicationsFinalize.Checked = currentApplication.PublicationsComplete;

            //Bind the publications grid with existing files
            rptPublications.DataSource = GetFilesOfType("Publication");
            rptPublications.DataBind();
        }

        #endregion



        #endregion

}

    /// <summary>
    /// Represents a Step object (including Home) to be used for tabbed browsing
    /// </summary>
    public class Step
    {
        private const string STR_AppCheckedImage = "~/Images/appmenuCheck.gif";
        private const string STR_AppUncheckedImage = "~/Images/appmenuX.gif";
        private const string STR_Selected = "selected";
        private const string STR_Unselected = "unselected";

        private bool _StepVisible;

        public bool StepVisible
        {
            get { return _StepVisible; }
            set { _StepVisible = value; }
        }

        private string _ImgURL;

        public string ImgURL
        {
            get { return _ImgURL; }
            set { _ImgURL = value; }
        }

        private string _StepName;

        public string StepName
        {
            get { return _StepName; }
            set { _StepName = value; }
        }

        private string _CSSClass;

        public string CSSClass
        {
            get { return _CSSClass; }
            set { _CSSClass = value; }
        }
        
        public Step()
        {

        }

        public void setSelectionStatus(bool selected)
        {
            if (selected)
                this.CSSClass = STR_Selected;
            else
                this.CSSClass = STR_Unselected;
        }

        public void setCompletionStatus(bool completed)
        {
            if (completed)
                this.ImgURL = STR_AppCheckedImage;
            else
                this.ImgURL = STR_AppUncheckedImage;
        }

        public Step(string stepName, bool completed, bool selected, bool stepVisible )
        {
            this.StepName = stepName;

            this.setCompletionStatus(completed);

            this.setSelectionStatus(selected);            

            this.StepVisible = stepVisible;
        }
    }
}

