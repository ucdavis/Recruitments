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
    public partial class PositionDetails : ApplicationPage
    {
        private const string STR_PositionID = "PositionID";
        
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

        protected void Page_PreInit(object sender, EventArgs e)
        {
            CheckQueryString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Position currentPosition = daoFactory.GetPositionDao().GetById(currentPositionID, false);

            try
            {
                currentPosition.IsTransient();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                //if the current position does not have a database association, redirect to an error page
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
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

        }

        /// <summary>
        /// Check to ensure that they querystring "PositionID" is not null or empty
        /// </summary>
        private void CheckQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString[STR_PositionID]))
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
        }
    }
}