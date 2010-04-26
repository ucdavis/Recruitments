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

            NHibernate.ISession session = (NHibernate.ISession)HttpContext.Current.Items["CONTEXT_SESSION"];

            Trace.Write(session.IsOpen.ToString());

            //Add education
            ApplicationSteps.Add(new Step("Education Information", currentApplication.isComplete(ApplicationStepType.Education), false, true));

            //Add references
            ApplicationSteps.Add(new Step("References", currentApplication.isComplete(ApplicationStepType.References), false, true));
            
            //Add resume
            
            ApplicationSteps.Add(new Step("Resume", false, false, true));

            ApplicationSteps.Add(new Step("Home", true, false, true));
            ApplicationSteps.Add(new Step("Contact Information", false, false, true));
            ApplicationSteps.Add(new Step("Education Information", true, false, true));
            ApplicationSteps.Add(new Step("Home", true, false, true));
            ApplicationSteps.Add(new Step("Contact Information", false, false, true));
            ApplicationSteps.Add(new Step("Education Information", true, false, true));

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

        public Step(string stepName, bool completed, bool selected, bool stepVisible )
        {
            this.StepName = stepName;

            if (completed)
                this.ImgURL = STR_AppCheckedImage;
            else
                this.ImgURL = STR_AppUncheckedImage;

            if (selected)
                this.CSSClass = STR_Selected;
            else
                this.CSSClass = STR_Unselected;

            this.StepVisible = stepVisible;
        }
    }
}

