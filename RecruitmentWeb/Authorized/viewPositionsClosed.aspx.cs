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

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_viewPositionsClosed : ApplicationPage
    {
        private const string STR_PositionDetailsURL = "PositionManagement.aspx";
        private const string STR_ViewApplicationsURL = "viewApplications.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnPositionTitle_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;

            Response.Redirect(STR_PositionDetailsURL + "?PositionID=" + lbtn.CommandArgument);
        }

        protected void lbtnApplicationCount_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;

            Response.Redirect(STR_ViewApplicationsURL + "?PositionID=" + lbtn.CommandArgument);
        }
    }

}