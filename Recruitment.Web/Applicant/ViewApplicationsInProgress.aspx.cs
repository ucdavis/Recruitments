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

namespace CAESDO.Recruitment.Web
{
    public partial class Applicant_ViewApplicationsInProgress : ApplicationPage
    {
        private const string STR_AppURL = "app.aspx";
        public Applicant currentApplicant
        {
            get
            {
                return daoFactory.GetApplicantDao().GetApplicantByEmail(HttpContext.Current.User.Identity.Name);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Application a = new Application();
        }

        protected void ObjectDataApplications_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["applicantProfile"] = currentApplicant.MainProfile;
        }

        protected void lbtnViewApplication_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;

            Response.Redirect(STR_AppURL + "?ApplicationID=" + lbtn.CommandArgument);
        }
}

}