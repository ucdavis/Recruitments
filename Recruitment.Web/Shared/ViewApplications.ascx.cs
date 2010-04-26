using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Web
{
    public partial class Shared_ViewApplications : System.Web.UI.UserControl
    {
        private const string STR_PositionID = "PositionID";
        private const string STR_ApplicationReviewaspx = "ApplicationReview.aspx";

        public int currentPositionID
        {
            get
            {
                CheckQueryString();

                //Try parsing the PositionID query string into an integer.
                //positionID will be number represented in the string or 0 on failure
                int positionID = 0;
                int.TryParse(Request.QueryString[STR_PositionID], out positionID);

                return positionID;
            }
        }

        public Position currentPosition
        {
            get
            {
                return PositionBLL.GetByID(currentPositionID);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litPositionTitle.Text = currentPosition.PositionTitle;
            }
        }

        public string GetNullSafeFullName(string FullName)
        {
            return ApplicantBLL.GetNullSafeFullName(FullName);
        }

        /// <summary>
        /// Check to ensure that they querystring "PositionID" is not null or empty
        /// </summary>
        private void CheckQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString[STR_PositionID]))
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
        }

        protected void ObjectDataApplications_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["position"] = currentPosition;
        }

        /// <summary>
        /// Redirect to the applicationReview page for this application
        /// </summary>
        /// <param name="sender">Sender has a command argument with the applicationID</param>
        protected void lbtnViewApplication_Click(object sender, EventArgs e)
        {
            string applicationID = ((LinkButton)sender).CommandArgument;

            Response.Redirect(string.Format("{0}?ApplicationID={1}", STR_ApplicationReviewaspx, applicationID));
        }
    } 
}
