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
    public partial class Review_ApplicationChoice : ApplicationPage
    {
        private const string STR_ApplicationReviewURL = "ApplicationReview.aspx?ApplicationID=";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnApplicationReview_Click(object sender, EventArgs e)
        {
            Response.Redirect(STR_ApplicationReviewURL + dlistApplications.SelectedValue);
        }
}
}