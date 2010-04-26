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
        private const string STR_ApplicationID = "ApplicationID";
        private const string STR_CurrentApplication = "currentApplication";
        private const string STR_ApplicationSteps = "ApplicationSteps";

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

            set
            {
                Session[STR_CurrentApplication] = value;
            }
        }

        public List<Step> ApplicationSteps
        {
            get
            {
                if (Session[STR_ApplicationSteps] == null)
                    Session[STR_ApplicationSteps] = new List<Step>();

                return (List<Step>)Session[STR_ApplicationSteps];
            }

            set
            {
                Session[STR_ApplicationSteps] = value;
            }
        }

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
            if (!Page.IsPostBack)
            {
                //Load the application steps in first page visit
                LoadSteps();
            }

            rptSteps.DataSource = ApplicationSteps;
            rptSteps.DataBind();
        }

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

            Trace.Write("-- lbtnStep_Click End --");
        }

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
        /// Loads up all application steps, along with state, into a List for the sidebar
        /// </summary>
        /// <remarks>
        /// Method signature for Step() : public Step(string stepName, bool completed, bool selected, bool stepVisible )
        /// </remarks>
        private void LoadSteps()
        {
            Trace.Write("Begin Step Load");

            ApplicationSteps = new List<Step>();

            //First add the home step
            ApplicationSteps.Add(new Step("Home", true, true, true));

            //Now add the contact information (so far no way to tell if complete)
            ApplicationSteps.Add(new Step("Contact Information", false, false, true));

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
                    case "ResearchInterest":
                        hasResearchInterest = true;
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

