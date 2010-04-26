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
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class PositionDetails : ApplicationPage
    {
        private const string STR_PositionID = "PositionID";
        private const string STR_DEFAULT_DEPARTMENT = "ADNO";
        
        public int currentPositionID
        {
            get
            {
                CheckQueryString();

                //Try parsing the PositionID query string into an integer.
                //PosID will be number represented in the string or 0 on failure
                int posID = 0;
                int.TryParse(Request.QueryString[STR_PositionID], out posID);

                return posID;
            }
        }

        public Position currentPosition
        {
            get
            {
                return PositionBLL.GetNullableByID(currentPositionID);
            }
        }

        public Theme defaultTheme
        {
            get
            {
                return daoFactory.GetThemeDao().GetById(STR_DEFAULT_DEPARTMENT, false);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            CheckQueryString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if the current position does not have a database association, redirect to an error page
            if (currentPosition == null)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            //Make sure the current's position isn't closed and that it is allowing applications
            if (currentPosition.Closed || !currentPosition.AllowApps)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }

            //Now we have a valid Position, so fill in the corresponding fields
            lblPositionTitle.Text = currentPosition.PositionTitle;
            lblPositionNumber.Text = currentPosition.PositionNumber;

            txtPositionDescription.Text = currentPosition.ShortDescription;

            lblDatePosted.Text = currentPosition.DatePosted.ToShortDateString();
            lblDeadline.Text = currentPosition.Deadline.ToShortDateString();
            lblDepartments.Text = currentPosition.DepartmentList;

            lblNumReferences.Text = currentPosition.NumReferences.ToString();
            lblNumPublications.Text = currentPosition.NumPublications.ToString();

            //lblHRRep.Text = currentPosition.HRRep ?? "N/A";
            //lblHRPhone.Text = currentPosition.HRPhone ?? "N/A";
            //lblHREmail.Text = currentPosition.HREmail ?? "N/A";

            //Setup the StyleSheet
            //First find the "primary department"
            if (currentPosition.PrimaryDepartment == null)
            {
                //If we don't have a primary department, get the default theme
                pnlDepartmentLogo.CssClass = defaultTheme.ThemeName;
            }
            else
            {
                if (currentPosition.PrimaryDepartment.Theme == null)
                {
                    //If the primary department doesn't have a theme, get the default
                    pnlDepartmentLogo.CssClass = defaultTheme.ThemeName;
                }
                else
                {
                    //We have a primary dept and a theme
                    pnlDepartmentLogo.CssClass = currentPosition.PrimaryDepartment.Theme.ThemeName;
                }
            }
        }

        /// <summary>
        /// Check to ensure that they querystring "PositionID" is not null or empty
        /// </summary>
        private void CheckQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString[STR_PositionID]))
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
        }

        /// <summary>
        /// Downloads the current position description
        /// </summary>
        protected void lbtnDownloadPD_Click(object sender, EventArgs e)
        {
            File descriptionFile = currentPosition.DescriptionFile;

            if (descriptionFile == null)
            {
                throw new NullReferenceException(string.Format("Description File Not Found For Position {0}", currentPosition.ID));
            }

            bool transmitSuccess = FileBLL.Transmit(descriptionFile.ID, descriptionFile.FileName);

            if (transmitSuccess == false)
            {
                throw new NullReferenceException(string.Format("Description File Not Found For Position {0}", currentPosition.ID));
            }
        }
        
        /// <summary>
        /// Creates an application on behalf of the currently logged in user, then
        /// redirects to that application
        /// </summary>
        /// <remarks>Checks to make sure the logged in user has an applicant account</remarks>
        protected void btnPositionApply_Click(object sender, EventArgs e)
        {
            Applicant loggedInUser = ApplicantBLL.GetCurrent();

            //Make sure the loggedInUser has an Applicant account
            if (loggedInUser == null)
            {
                Trace.Warn("Not Logged Is As Member");

                FormsAuthentication.SignOut(); //Causes the user to sign out and redirect
                FormsAuthentication.RedirectToLoginPage(Request.Url.AbsolutePath); //Make the user log in
                return;                
            }

            //If the applicant already has an application for this position, redirect to the app page
            foreach (Application app in loggedInUser.MainProfile.Applications)
            {
                if (app.AppliedPosition == currentPosition)
                {
                    Response.Redirect(string.Format("{0}?ApplicationID={1}", "Applicant/App.aspx", app.ID));
                }
            }

            //Now we have a valid applicant, so create the application
            Application newApplication = new Application();

            newApplication.AppliedPosition = currentPosition;
            newApplication.AssociatedProfile = loggedInUser.MainProfile;
            newApplication.Email = loggedInUser.Email;
            newApplication.LastUpdated = DateTime.Now;

            using (new TransactionScope())
            {
                //Now save the new application get get back the ID
                ApplicationBLL.EnsurePersistent(ref newApplication);

                //Redirect to the newly created application
                Response.Redirect(string.Format("{0}?ApplicationID={1}", "Applicant/App.aspx", newApplication.ID.ToString()));
            }
        }
}
}