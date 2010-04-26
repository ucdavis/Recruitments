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
    public partial class Authorized_viewApplications : ApplicationPage
    {
        private const string STR_PositionID = "PositionID";

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
                return daoFactory.GetPositionDao().GetById(currentPositionID, false);
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
}

}