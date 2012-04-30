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
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class Applicant_ViewApplicationsInProgress : ApplicationPage
    {
        private const string STR_AppURL = "app.aspx";

        public Applicant currentApplicant
        {
            get
            {
                return ApplicantBLL.GetCurrent();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Applicant loggedInUser = currentApplicant;

            //Make sure the loggedInUser has an Applicant account
            if (loggedInUser == null)
            {
                FormsAuthentication.SignOut(); //Causes the user to sign out and redirect
                FormsAuthentication.RedirectToLoginPage(); //Make the user log in
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ObjectDataApplications_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if ( currentApplicant != null )
                e.InputParameters["applicantProfile"] = currentApplicant.MainProfile;
        }

        protected void lbtnViewApplication_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;

            Response.Redirect(STR_AppURL + "?ApplicationID=" + lbtn.CommandArgument);
        }
}

}