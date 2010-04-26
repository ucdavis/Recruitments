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
    public partial class ViewPositionsPending : ApplicationPage
    {
        private const string STR_PositionDetailsURL = "positionmanagement.aspx";
        private const string STR_ViewApplicationsURL = "viewApplications.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!Roles.IsUserInRole("Admin"))
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }
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

        protected void lbtnAccept_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;

            //Grab the position associated with this button
            Position currentPosition = PositionBLL.GetByID(int.Parse(lbtn.CommandArgument));
            
            //Now set the adminAccepted property to true

            using (var ts = new TransactionScope())
            {
                currentPosition.AdminAccepted = true;
                PositionBLL.EnsurePersistent(ref currentPosition);

                ts.CommitTransaction();
            }

            //Now rebind the grid
            gViewPositions.DataBind();
        }
}
}