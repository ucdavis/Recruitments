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
using System.Text;
using CAESDO.Recruitment.BLL;
using System.Net.Mail;

namespace CAESDO.Recruitment.Web
{
    public partial class App : ApplicationPage
    {
        #region ConstVariables
        private const string STR_ApplicationID = "ApplicationID";
        private const string STR_ExtensionInterests  = "Extension Interests";
        private const string STR_TeachingInterests = "Teaching Interests"; 
        private const string _Transcript = "Transcript";
        private const string STR_ConfidentialSurvey = "Confidential Survey";
        private const string STR_EducationInformation = "Education Information";
        private const string STR_CurrentApplication = "currentApplication";
        private const string STR_ApplicationSteps = "ApplicationSteps";
        private const string STR_HomeStep = "Home";

        private const string STR_Applicationpdf = "application/pdf";
        
        private const string STR_ContactInformation = "Contact Information";
        private const string STR_References = "References";
        private const string STR_CurrentPosition = "Current Position";
        private const string STR_Resume = "Resume";
        private const string STR_CoverLetter = "Cover Letter";
        private const string STR_ResearchInterests = "Research Interests";
        private const string STR_Transcripts = "Transcripts";
        private const string STR_Publications = "Publications";
        private const string STR_Dissertation = "Dissertation";
        private const string STR_FileType_Transcript = "Transcript";
        private const string STR_FileType_CoverLetter = "CoverLetter";
        private const string STR_FileType_ResearchInterests = "ResearchInterests";
        private const string STR_FileType_ExtensionInterests = "ExtensionInterests";
        private const string STR_FileType_TeachingInterests = "TeachingInterests";
        private const string STR_FileType_Publication = "Publication";
        private const string STR_CV = "CV";
        private const string STR_URL_ApplicationSubmitted = "ApplicationSubmitted.aspx";
        
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
                return ApplicationBLL.GetNullableByID(currentApplicationID);
            }
        }

        public List<Step> ApplicationSteps
        {
            get
            {
                if (Session[STR_ApplicationSteps] == null)
                    return null;  //Session[STR_ApplicationSteps] = new List<Step>();

                return Session[STR_ApplicationSteps] as List<Step>;
            }

            set
            {
                Session[STR_ApplicationSteps] = value;
            }
        }

        private System.Globalization.CultureInfo englishCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        #endregion

        #region Page Methods

        /// <summary>
        /// Page_Init checks to ensure that the query string is valid, the logged in user is an applicant, the given application is valid
        /// and the logged-in user is the owner of this application
        /// </summary>
        /// <remarks>Currently any membership user can view any application for testing</remarks>
        protected void Page_Init(object sender, EventArgs e)
        {
            Applicant loggedInUser = ApplicantBLL.GetCurrent();

            //Make sure the loggedInUser has an Applicant account
            if (loggedInUser == null)
            {
                FormsAuthentication.SignOut(); //Causes the user to sign out and redirect
                FormsAuthentication.RedirectToLoginPage(STR_ApplicationID + currentApplicationID.ToString()); //Make the user log in
                return;
            }

            if (currentApplication == null)
            {
                //if the current application does not have a database association, redirect to an error page
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            //Make sure the application has not been submitted
            if (currentApplication.Submitted)
            {
                Response.Redirect(STR_URL_ApplicationSubmitted);
            }

            //Trace.Write("Valid user and application " + currentApplication.ID.ToString() + Environment.NewLine);

            if (currentApplication.AssociatedProfile.AssociatedApplicant.Email != loggedInUser.Email)
            {
                //Trace.Write("User trying to access incorrect application");
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ApplicationSteps = null; //Clear the steps list on first visit

            //Load the application steps
            LoadSteps();

            //Bind the home step
            DataBindHome();

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
            //Grab the link button's command argument (the step name)
            string stepName = ((LinkButton)sender).CommandArgument;

            //Trace.Write("-- lbtnStep_Click Begin " + stepName + "--");

            MakeStepActive(stepName);

            DataBindStep(stepName);

            //Trace.Write("-- lbtnStep_Click End --");
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
            Application application = currentApplication;

            currentProfile.FirstName = txtContactFirstName.Text;
            currentProfile.MiddleName = txtContactMiddleName.Text;
            currentProfile.LastName = txtContactLastName.Text;

            currentProfile.Address1 = txtContactAddress1.Text;
            currentProfile.Address2 = txtContactAddress2.Text;
            currentProfile.City = txtContactCity.Text;
            currentProfile.Zip = txtContactZip.Text;
            currentProfile.Country = txtContactCountry.Text;
            currentProfile.State = txtContactState.Text;
            currentProfile.Phone = txtContactPhone.Text;

            //currentProfile.Country = null;
            //currentProfile.CountryCode = null;

            //Update the LastUpdated property (this is how we know a profile has been updated/completed)
            currentProfile.LastUpdated = DateTime.Now;

            //Update the email of the current application
            application.LastUpdated = DateTime.Now;
            application.Email = txtContactEmail.Text;

            using (var ts = new TransactionScope())
            {
                ProfileBLL.EnsurePersistent(currentProfile);
                ApplicationBLL.EnsurePersistent(application);

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_ContactInformation, true);
        }

        /// <summary>
        /// Loop through all defined rules for this application's position and make sure they are all met.
        /// After verification, mark the application as submitted
        /// </summary>
        protected void btnApplicationFinalize_Click(object sender, EventArgs e)
        {
            //Place all of the errors into this error string, and mark isComplete false if we fail a step
            List<string> incompleteSteps = new List<string>();
            
            //Make sure they updated their profile at some point
            if (currentApplication.AssociatedProfile.LastUpdated == null)
                incompleteSteps.Add(STR_ContactInformation);

            //Loop through each required step and make sure it is completed
            foreach (ApplicationStepType step in currentApplication.AppliedPosition.Steps)
            {
                switch (step)
                {
                    case ApplicationStepType.CurrentPosition:
                        if (!currentApplication.isComplete(ApplicationStepType.CurrentPosition))
                            incompleteSteps.Add(STR_CurrentPosition);
                        break;
                    case ApplicationStepType.Education:
                        if (!currentApplication.isComplete(ApplicationStepType.Education))
                            incompleteSteps.Add(STR_EducationInformation);
                        break;
                    case ApplicationStepType.Survey:
                        //if (!currentApplication.isComplete(ApplicationStepType.Survey))
                        //    incompleteSteps.Add(STR_ConfidentialSurvey);                        
                        break;
                    case ApplicationStepType.References:
                        //if (!currentApplication.ReferencesComplete)
                        //    incompleteSteps.Add(STR_References);                        
                        break;
                    default:
                        break;
                }
            }

            //Always check references and the confidential survey
            if (!currentApplication.isComplete(ApplicationStepType.Survey))
                incompleteSteps.Add(STR_ConfidentialSurvey);

            if (!currentApplication.ReferencesComplete)
                incompleteSteps.Add(STR_References);  

            //Loop through each requested file type and make sure a file of the given type exists
            foreach (FileType type in currentApplication.AppliedPosition.FileTypes)
            {
                switch (type.FileTypeName)
                {
                    case STR_FileType_CoverLetter:
                        //Cover letter is incomplete only if the "coverletter complete" bool is not true AND there are no cover letters
                        if (!currentApplication.CoverLetterComplete && !existsFileOfType(STR_FileType_CoverLetter))
                            incompleteSteps.Add(STR_CoverLetter);
                        break;
                    case STR_FileType_ExtensionInterests:
                        if (!existsFileOfType(STR_FileType_ExtensionInterests))
                            incompleteSteps.Add(STR_ExtensionInterests);
                        break;
                    case STR_FileType_Publication:
                        if (!currentApplication.PublicationsComplete)
                            incompleteSteps.Add(STR_Publications);
                        break;
                    case STR_FileType_ResearchInterests:
                        if (!existsFileOfType(STR_FileType_ResearchInterests))
                            incompleteSteps.Add(STR_ResearchInterests);
                        break;
                    case STR_FileType_TeachingInterests:
                        if (!existsFileOfType(STR_FileType_TeachingInterests))
                            incompleteSteps.Add(STR_TeachingInterests);
                        break;
                    case STR_FileType_Transcript:
                        if (!existsFileOfType(STR_FileType_Transcript))
                            incompleteSteps.Add(STR_Transcripts);
                        break;
                    case STR_Resume:
                        if (!existsFileOfType(STR_Resume))
                            incompleteSteps.Add(STR_Resume);
                        break;
                    case STR_CV:
                        if (!existsFileOfType(STR_CV))
                            incompleteSteps.Add(STR_CV);
                        break;
                    case STR_Dissertation:
                        if (!existsFileOfType(STR_Dissertation))
                            incompleteSteps.Add(STR_Dissertation);
                        break;
                    default:
                        break;
                }
            }

            //If there are any incompleteSteps, alert the user, else complete the application and redirect
            if (incompleteSteps.Count > 0)
            {
                StringBuilder finalizeErrorString = new StringBuilder("You must complete the following step(s) before finalizing your application: ");
                finalizeErrorString.Append("<br />");
                finalizeErrorString.Append("<span style=\"color:Red\">");

                incompleteSteps.Sort();

                foreach (string s in incompleteSteps)
                {
                    finalizeErrorString.Append(s);
                    finalizeErrorString.Append("<br />");
                }

                finalizeErrorString.Append("</span>");

                litApplicationFinalizeStatus.Text = finalizeErrorString.ToString();
            }
            else
            {
                using (var ts = new TransactionScope())
                {
                    currentApplication.SubmitDate = DateTime.Now;
                    currentApplication.Submitted = true;

                    ts.CommitTransaction();
                }

                //Application was successfully submitted, so send a confirmation email
                System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();

                //Try to send the email -- if there are errors, ignore (we'd rather have the application completed then fail on the emailing)
                try
                {
                    string subject = "Application submission confirmation";
                    
                    StringBuilder bodyText = new StringBuilder();
                    bodyText.AppendFormat("Your application for the {0} position at the University of California, Davis, has been successfully received.", currentApplication.AppliedPosition.PositionTitle);
                    bodyText.Append("  We appreciate your interest in our position.");

                    MailMessage message = new MailMessage(currentApplication.AppliedPosition.HREmail, currentApplication.Email, subject, bodyText.ToString());
                    message.IsBodyHtml = true;

                    mail.Send(message); //Send the message

                }
                catch (Exception) { } //Continue on failure

                Response.Redirect(STR_URL_ApplicationSubmitted);
            }
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
            currentEducation.Date = DateTime.Parse(txtEducationPHDDate.Text, englishCulture);
            //currentEducation.Date = DateTime.Parse(txtEducationPHDDate.Text);

            currentEducation.Institution = txtEducationInstitution.Text;
            currentEducation.Discipline = txtEducationDiscipline.Text;

            //Associate this with the current application
            currentEducation.AssociatedApplication = currentApplication;

            currentEducation.Complete = true; //Make sure to complete on save

            using (var ts = new TransactionScope())
            {
                EducationBLL.EnsurePersistent(currentEducation);
                currentApplication.Education.Add(currentEducation);

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_EducationInformation, true);
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

            using (var ts = new TransactionScope())
            {
                CurrentPositionBLL.EnsurePersistent(currentPosition);
                currentApplication.CurrentPositions.Add(currentPosition);

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_CurrentPosition, true);
        }

        protected void btnResumeUpload_Click(object sender, EventArgs e)
        {            
            ReloadStepListAndSelectHome(STR_Resume, AddUniqueFileToApplication(fileResume, STR_Resume));
        }

        protected void btnCVUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_CV, AddUniqueFileToApplication(fileCV, STR_CV));
        }

        protected void btnCoverLetterUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_CoverLetter, AddUniqueFileToApplication(fileCoverLetter, STR_FileType_CoverLetter));
        }

        protected void chkCoverLetterOption_CheckedChanged(object sender, EventArgs e)
        {
            using (var ts = new TransactionScope())
            {
                currentApplication.CoverLetterComplete = chkCoverLetterOption.Checked;

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_CoverLetter, true);
        }

        protected void btnResearchInterestsUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_ResearchInterests, AddUniqueFileToApplication(fileResearchInterests, STR_FileType_ResearchInterests));
        }

        protected void btnExtensionInterestsUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_ExtensionInterests, AddUniqueFileToApplication(fileExtensionInterests, STR_FileType_ExtensionInterests));
        }

        protected void btnTeachingInterestsUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_TeachingInterests, AddUniqueFileToApplication(fileTeachingInterests, STR_FileType_TeachingInterests));
        }

        protected void btnTranscriptsUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_Transcripts, AddUniqueFileToApplication(fileTranscripts, STR_FileType_Transcript));
        }
        
        protected void btnDissertationUpload_Click(object sender, EventArgs e)
        {
            ReloadStepListAndSelectHome(STR_Dissertation, AddUniqueFileToApplication(fileDissertation, STR_Dissertation));
        }

        protected void btnPublicationsUpload_Click(object sender, EventArgs e)
        {
            FileType publicationsFileType = FileTypeBLL.GetByName(STR_FileType_Publication);

            File uploadedFile = null;

            using (var ts = new TransactionScope())
            {
                uploadedFile = FileBLL.SavePDF(filePublications, publicationsFileType);

                ts.CommitTransaction();
            }

            if (uploadedFile != null)
            {
                Application application = currentApplication;

                using (var ts = new TransactionScope())
                {
                    application.Files.Add(uploadedFile);
                    application.LastUpdated = DateTime.Now;

                    ApplicationBLL.EnsurePersistent(application);

                    ts.CommitTransaction();
                }
            }
            else
            {
                ///TODO: Error message
            }

            DataBindPublications();
        }

        protected void ibtnPublicationsRemoveFile_Click(object sender, EventArgs e)
        {
            int fileID = 0;
            bool success = false;

            success = int.TryParse(((ImageButton)sender).CommandArgument, out fileID);

            if (success)
            {
                File fileToDelete = FileBLL.GetByID(fileID);
                Application application = currentApplication;

                using (var ts = new TransactionScope())
                {
                    FileBLL.DeletePDF(fileToDelete);

                    application.LastUpdated = DateTime.Now;
                    application.Files.Remove(fileToDelete);

                    //Update the current application
                    ApplicationBLL.EnsurePersistent(application);

                    ts.CommitTransaction();
                }
            }

            DataBindPublications();
        }

        protected void chkPublicationsFinalize_CheckedChanged(object sender, EventArgs e)
        {
            using (var ts = new TransactionScope())
            {
                currentApplication.PublicationsComplete = chkPublicationsFinalize.Checked;

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_Publications, true);
        }

        protected void btnConfidentialSurveyAccept_Click(object sender, EventArgs e)
        {
            int genderID = 0;
            string ethnicity = null;
            List<SurveyXRecruitmentSrc> RecruitmentSources = new List<SurveyXRecruitmentSrc>();

            foreach (ListItem item in rbtnConfidentialSurveySex.Items)
            {
                if (item.Selected) //Find the selected gender and set the genderID to that value
                    genderID = int.Parse(item.Value);
            }

            //Instead of grabbing each radio button individually, loop through the control tree of the current view and look for radio buttons
            foreach (Control c in viewConfidentialSurvey.Controls)
            {
                if (c is RadioButton)
                {
                    RadioButton ethnicityButton = (RadioButton)c;

                    if (ethnicityButton.Checked && ethnicityButton.GroupName == "Ethnicity")
                    {
                        ethnicity = ethnicityButton.Text;
                        break;
                    }
                }
            }

            //Look through each item in the recruitment source repeater
            foreach (RepeaterItem item in rptRecruitmentSource.Items)
            {
                CheckBox cbox = item.FindControl("chkRecruitmentSource") as CheckBox;

                if (cbox != null && cbox.Checked) //if we found a checked checkbox
                {
                    SurveyXRecruitmentSrc recruitmentSource = new SurveyXRecruitmentSrc();
                    RecruitmentSrc example = new RecruitmentSrc();

                    Label source = item.FindControl("lblRecruitmentSource") as Label;
                    TextBox specific = item.FindControl("txtSpecify") as TextBox;

                    if (source != null) //Make sure we have a valid source
                    {
                        example.RecruitmentSource = source.Text;

                        //Grab the matching recruitment source out (ignore the allow specify field)
                        recruitmentSource.RecruitmentSrc = RecruitmentSrcBLL.GetUniqueByExample(example, "AllowSpecify");
                        recruitmentSource.RecruitmentSrcOther = string.IsNullOrEmpty(specific.Text) ? null : specific.Text;

                        RecruitmentSources.Add(recruitmentSource);
                    }
                }
            }

            //Save ethnicity, gender, and all of the recruitment sources
            Application application = currentApplication;

            using (var ts = new TransactionScope())
            {
                //First make sure we have a survey 
                if (application.Surveys.Count == 0)
                {
                    Survey newSurvey = new Survey();
                    newSurvey.AssociatedApplication = application;
                    application.Surveys.Add(newSurvey);
                }

                Survey currentSurvey = application.Surveys[0];

                //Save the gender if the ID was set (non-zero), else save that is was null
                if (genderID > 0)
                {
                    currentSurvey.Gender = GenderBLL.GetByID(genderID);
                }
                else
                {
                    currentSurvey.Gender = null;
                }

                //Save the ethnicity 
                if (ethnicity != null)
                {
                    Ethnicity chosenEthnicity = new Ethnicity();
                    chosenEthnicity.EthnicityValue = ethnicity;

                    currentSurvey.Ethnicity = EthnicityBLL.GetUniqueByExample(chosenEthnicity);
                    currentSurvey.TribalAffiliation = string.IsNullOrEmpty(txtAmericanIndian.Text) ? null : txtAmericanIndian.Text;
                }
                else
                {
                    currentSurvey.Ethnicity = null;
                    currentSurvey.TribalAffiliation = null;
                }

                //Save each recruitment source & clear out existing sources
                if (currentSurvey.RecruitmentSources == null)
                    currentSurvey.RecruitmentSources = new List<SurveyXRecruitmentSrc>();

                currentSurvey.RecruitmentSources.Clear();

                foreach (SurveyXRecruitmentSrc chosenRecruitmentSource in RecruitmentSources)
                {
                    chosenRecruitmentSource.AssociatedSurvey = currentSurvey;

                    currentSurvey.RecruitmentSources.Add(chosenRecruitmentSource);
                    //Trace.Write(string.Format("Checked the source {0} with additional info {1}", s.RecruitmentSrc.RecruitmentSource, s.RecruitmentSrcOther) + Environment.NewLine);
                }
                
                //Finally, set this step to complete
                currentSurvey.Complete = true;
                currentSurvey.AssociatedApplication = application;

                ApplicationBLL.EnsurePersistent(application);

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_ConfidentialSurvey, true);
        }

        protected void btnReferencesAddUpdate_Click(object sender, EventArgs e)
        {
            Button referenceButton = sender as Button;

            //Grab the referenceID from the command argument (0 if it doesn't parse)
            int referenceID = 0;
            
            int.TryParse(referenceButton.CommandArgument, out referenceID);

            Reference currentReference = new Reference();

            //If we have a positive int, get the associated reference
            if (referenceID > 0)
                currentReference = ReferenceBLL.GetByID(referenceID);

            //Now fill in the current reference with the form fields
            currentReference.Title = txtReferencesTitle.Text;
            currentReference.FirstName = txtReferencesFirstName.Text;
            currentReference.LastName = txtReferencesLastName.Text;

            currentReference.AcadTitle = txtReferencesAcadTitle.Text;
            currentReference.Expertise = txtReferencesExpertise.Text;

            currentReference.Dept = txtReferencesDepartment.Text;
            currentReference.Institution = txtReferencesInstitute.Text;

            currentReference.Address1 = txtReferencesAddress1.Text;
            currentReference.Address2 = txtReferencesAddress2.Text;
            currentReference.City = txtReferencesCity.Text;
            currentReference.State = txtReferencesState.Text;
            currentReference.Zip = txtReferencesZip.Text;
            currentReference.Country = txtReferencesCountry.Text;

            currentReference.Phone = txtReferencesPhone.Text;
            currentReference.Email = txtReferencesEmail.Text;

            currentReference.Complete = true;

            //Now save this reference by adding it to the current application if it is new, or by replacing the old copy if it exists
            currentReference.AssociatedApplication = currentApplication;

            using (var ts = new TransactionScope())
            {
                currentApplication.LastUpdated = DateTime.Now;
                ReferenceBLL.EnsurePersistent(currentReference);

                ts.CommitTransaction();
            }

            //Set the button text back to 'Add Reference' and the Command Argument to 0
            btnReferencesAddUpdate.Text = "Add Reference";
            btnReferencesAddUpdate.CommandArgument = "0";

            //Clear out any iformation entered in
            ClearTextBoxesInPanel(pnlReferencesEntry);

            //Now DataBind the step to show any changes
            DataBindReferences();
        }

        protected void btnReferencesCancel_Click(object sender, EventArgs e)
        {
            //Set the button text back to 'Add Reference' and the Command Argument to 0
            btnReferencesAddUpdate.Text = "Add Reference";
            btnReferencesAddUpdate.CommandArgument = "0";

            ClearTextBoxesInPanel(pnlReferencesEntry);
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
            
            //Now set the command argument of the update button to the ID of the currentReference, and change the text as well
            btnReferencesAddUpdate.Text = "Update Reference";
            btnReferencesAddUpdate.CommandArgument = currentReference.ID.ToString();

            //Now show the popup control
            mpopupReferencesEntry.Show();
        }

        protected void gviewReferences_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView referenceGrid = sender as GridView;

            //Grab the reference from the dataKey ID
            int currentReferenceID = (int)referenceGrid.DataKeys[e.RowIndex]["ID"];
            Reference currentReference = ReferenceBLL.GetByID(currentReferenceID);

            //Remove the reference from the currentApplication
            using (var ts = new TransactionScope())
            {
                currentApplication.LastUpdated = DateTime.Now;
                ReferenceBLL.Remove(currentReference);

                ts.CommitTransaction();
            }
            
            DataBindReferences();
        }
        
        protected void chkReferencesComplete_CheckedChanged(object sender, EventArgs e)
        {
            //Update the complete status
            using (var ts = new TransactionScope())
            {
                currentApplication.ReferencesComplete = chkReferencesComplete.Checked;

                ts.CommitTransaction();
            }

            ReloadStepListAndSelectHome(STR_References, true);
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
        private void ReloadStepListAndSelectHome(string fromStepName, bool success)
        {
            //Set the home status for user feedback
            if (success)
            {
                lblApplicationStepStatus.ForeColor = System.Drawing.Color.Green;
                lblApplicationStepStatus.Text = string.Format("{0} successfully completed", fromStepName);

                //Update the lastupdated timestamp
                using (var ts = new TransactionScope())
                {
                    currentApplication.LastUpdated = DateTime.Now;

                    ts.CommitTransaction();
                }
            }
            else
            {
                lblApplicationStepStatus.ForeColor = System.Drawing.Color.Red;
                lblApplicationStepStatus.Text = string.Format("Error occurred saving {0}.  Please return to the {0} tab and try again. If the problem persists, please contact applicaion support", fromStepName);
            }

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
            ApplicationSteps.Add(new Step(STR_ContactInformation, currentApplication.AssociatedProfile.LastUpdated != null, false, true));

            //Add education
            ApplicationSteps.Add(new Step(STR_EducationInformation, currentApplication.isComplete(ApplicationStepType.Education), false, currentApplication.AppliedPosition.Steps.Contains(ApplicationStepType.Education)));

            //Add references
            ApplicationSteps.Add(new Step(STR_References, currentApplication.ReferencesComplete, false, true));

            //Add current position
            ApplicationSteps.Add(new Step(STR_CurrentPosition, currentApplication.isComplete(ApplicationStepType.CurrentPosition), false, currentApplication.AppliedPosition.Steps.Contains(ApplicationStepType.CurrentPosition)));

            //Add files 
            bool hasResume = false;
            bool hasCoverLetter = false;
            bool hasCV = false;
            bool hasTranscript = false;
            bool hasResearchInterest = false;
            bool hasTeachingInterests = false;
            bool hasExtensionInterests = false;
            bool hasPublication = false;
            bool hasDissertation = false;

            //Go through each file in the currentApplication and find out if each filetype is available
            foreach (File file in currentApplication.Files)
            {
                switch (file.FileType.FileTypeName)
                {
                    case STR_Resume:
                        hasResume = true;
                        break;
                    case STR_FileType_CoverLetter:
                        hasCoverLetter = true;
                        break;
                    case STR_CV:
                        hasCV = true;
                        break;
                    case STR_FileType_Transcript:
                        hasTranscript = true;
                        break;
                    case STR_FileType_ResearchInterests:
                        hasResearchInterest = true;
                        break;
                    case STR_FileType_Publication:
                        hasPublication = true;
                        break;
                    case STR_Dissertation:
                        hasDissertation = true;
                        break;
                    case STR_FileType_TeachingInterests:
                        hasTeachingInterests = true;
                        break;
                    case STR_FileType_ExtensionInterests:
                        hasExtensionInterests = true;
                        break;
                    default:
                        break;
                }
            }

            //Now add each type of file, hiding if necessary
            ApplicationSteps.Add(new Step(STR_CV, hasCV, false, isFileTypeRequested(STR_CV)));
            ApplicationSteps.Add(new Step(STR_Resume, hasResume, false, isFileTypeRequested(STR_Resume)));
            ApplicationSteps.Add(new Step(STR_CoverLetter, hasCoverLetter || currentApplication.CoverLetterComplete, false, isFileTypeRequested(STR_FileType_CoverLetter)));
            ApplicationSteps.Add(new Step(STR_ResearchInterests, hasResearchInterest, false, isFileTypeRequested(STR_FileType_ResearchInterests)));
            ApplicationSteps.Add(new Step(STR_ExtensionInterests, hasExtensionInterests, false, isFileTypeRequested(STR_FileType_ExtensionInterests)));
            ApplicationSteps.Add(new Step(STR_TeachingInterests, hasTeachingInterests, false, isFileTypeRequested(STR_FileType_TeachingInterests)));
            ApplicationSteps.Add(new Step(STR_Transcripts, hasTranscript, false, isFileTypeRequested(STR_FileType_Transcript)));
            ApplicationSteps.Add(new Step(STR_Publications, currentApplication.PublicationsComplete, false, isFileTypeRequested(STR_FileType_Publication)));
            //ApplicationSteps.Add(new Step(STR_Dissertation, hasDissertation, false, true));

            //Add the confidential survey
            ApplicationSteps.Add(new Step(STR_ConfidentialSurvey, currentApplication.isComplete(ApplicationStepType.Survey), false, true));

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
        private bool DownloadFile(int fileID)
        {
            File fileToDownload = FileBLL.GetByID(fileID);

            return FileBLL.Transmit(fileToDownload.ID, fileToDownload.FileName);
        }

        /// <summary>
        /// Function which will upload the file in the fileUpload control into the current application
        /// as the given fileType.  Note that this will remove all other files of the fileType from the application
        /// </summary>
        /// <param name="fileUpload">The file upload control containing the file</param>
        /// <param name="fileTypeName">The fileType to upload as</param>
        /// <returns>True on success, else false</returns>
        private bool AddUniqueFileToApplication(FileUpload fileUpload, string fileTypeName)
        {
            FileType FileType = FileTypeBLL.GetByName(fileTypeName);

            RemoveAllFilesOfType(fileTypeName);

            using (var ts = new TransactionScope())
            {
                File savedFile = FileBLL.SavePDF(fileUpload, FileType);

                if (savedFile != null)
                {
                    Application application = currentApplication;

                    application.Files.Add(savedFile);

                    ApplicationBLL.EnsurePersistent(application);

                    ts.CommitTransaction();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns a string that specifies the number of publications requested remaining
        /// </summary>
        /// <returns></returns>
        public string NumPublicationsRemainingText()
        {
            //Warn the user if they don't have enough publications
            int numPublicationsRemaining = currentApplication.AppliedPosition.NumPublications - GetFilesOfType(STR_FileType_Publication).Count;

            if (numPublicationsRemaining > 0)
                return string.Format("[{0} More Publication{1} Requested]", numPublicationsRemaining, numPublicationsRemaining == 1 ? string.Empty : "s");
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns a string that specifies the number of references requested remaining
        /// </summary>
        public string NumReferencesRemainingText()
        {
            //Warn the user if they don't have enough references
            int numReferencesRemaining = currentApplication.AppliedPosition.NumReferences - currentApplication.References.Count;

            if (numReferencesRemaining > 0)
                return string.Format("[{0} More Reference{1} Requested]", numReferencesRemaining, numReferencesRemaining == 1 ? string.Empty : "s");
            else
                return string.Empty;
        }

        /// <summary>
        /// Checks the currentApplication's appliedPosition to see if the supplied 
        /// FileType is in the requested fileTypes list
        /// </summary>
        /// <param name="fileTypeName">FileTypeName string</param>
        /// <returns>True if the fileType is a requested one, else false</returns>
        private bool isFileTypeRequested(string fileTypeName)
        {
            FileType type = new FileType();
            type.FileTypeName = fileTypeName;

            return currentApplication.AppliedPosition.FileTypes.Contains(type);
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
        /// Determines if the current application contains a file of the given type
        /// </summary>
        /// <param name="fileTypeName">File type name</param>
        /// <returns>True if a file with the given type exists</returns>
        private bool existsFileOfType(string fileTypeName)
        {
            foreach (File f in currentApplication.Files)
            {
                if (f.FileType.FileTypeName == fileTypeName)
                    return true;
            }

            return false;
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
                using (var ts = new TransactionScope())
                {
                    Application application = currentApplication;

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
        /// Loops through a panel's controls to clear out any textboxes in that panel
        /// </summary>
        /// <param name="panel">The containing panel</param>
        private void ClearTextBoxesInPanel(Panel panel)
        {
            foreach (Control c in panel.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = string.Empty;
            }
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
                case STR_HomeStep:
                    DataBindHome();
                    break;
                case STR_ContactInformation:
                    DataBindContactInformation();
                    break;
                case STR_EducationInformation:
                    DataBindEducationInformation();
                    break;
                case STR_References:
                    DataBindReferences();
                    break;
                case STR_CurrentPosition:
                    DataBindCurrentPositionInformation();
                    break;
                case STR_Resume:
                    break;
                case STR_CoverLetter:
                    DataBindCoverLetter();
                    break;
                case STR_ResearchInterests:
                    break;
                case STR_Transcripts:
                    break;
                case STR_ConfidentialSurvey:
                    break;
                case STR_Publications:
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
            txtContactZip.Text = currentProfile.Zip;
            txtContactCountry.Text = currentProfile.Country;

            txtContactPhone.Text = currentProfile.Phone;
            txtContactEmail.Text = currentApplication.Email;
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

            txtEducationPHDDate.Text = string.Format(englishCulture, "{0:d}", currentEducation.Date);
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
            rptPublications.DataSource = GetFilesOfType(STR_FileType_Publication);
            rptPublications.DataBind();
        }

        /// <summary>
        /// Have to bind the current application's existing references to the grid
        /// </summary>
        private void DataBindReferences()
        {
            lblReferencesRemaining.Text = NumReferencesRemainingText();

            //Bind the Grid
            gviewReferences.DataSource = currentApplication.References;
            gviewReferences.DataBind();

            //Show the complete status
            chkReferencesComplete.Checked = currentApplication.ReferencesComplete;
        }

        /// <summary>
        /// Populate current position information
        /// </summary>
        private void DataBindHome()
        {
            lblApplicationPositionTitle.Text = currentApplication.AppliedPosition.PositionTitle;
            lblApplicationPositionTitle2.Text = currentApplication.AppliedPosition.PositionTitle;
            lblApplicationDeadline.Text = currentApplication.AppliedPosition.Deadline.ToShortDateString();
        }

        /// <summary>
        /// Populate the Cover Letter checkbox to allow for an 'optional' cover letter
        /// </summary>
        private void DataBindCoverLetter()
        {
            chkCoverLetterOption.Checked = currentApplication.CoverLetterComplete;
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

