using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Web
{
    public partial class Shared_ApplicantReport : System.Web.UI.UserControl, IReportUserControl
    {
        public IDaoFactory daoFactory
        {
            get { return new NHibernateDaoFactory(); }
        }

        private int? _currentPositionID;

        public int? currentPositionID
        {
            get { return _currentPositionID; }
            set { _currentPositionID = value; }
        }

        public Position currentPosition
        {
            get
            {
                if (currentPositionID.HasValue)
                    return daoFactory.GetPositionDao().GetById(currentPositionID.Value, false);
                else
                    return null;
            }
        }

        /// <summary>
        /// Page_Init checks to ensure that the query string is valid and the given position is valid
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (currentPosition == null)
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            else if (!Roles.IsUserInRole("Admin")) //If the user isn't an admin, check department relationships
            {
                User u = daoFactory.GetUserDao().GetUserByLogin(HttpContext.Current.User.Identity.Name);
                bool positionAccess = false;

                foreach (Department d in currentPosition.Departments)
                {
                    //Check if the current unit is in the user's units
                    if (u.Units.Contains(d.Unit))
                    {
                        positionAccess = true;
                        break;
                    }
                }

                //We have gone through all the departments, check if the user has access
                if (positionAccess == false)
                    Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var applications = currentPosition.AssociatedApplications;
            //applications[0].AssociatedProfile

            gviewApplications.DataSource = applications;
            gviewApplications.DataBind();
        }

        #region IReportUserControl Members

        public void LoadReport(StringDictionary parameters)
        {
            int posID = 0;

            if (int.TryParse(parameters["PositionID"], out posID))
                currentPositionID = posID;
            else
                currentPositionID = null;

            Page_Init(null, null);
            Page_Load(null, null);
        }

        #endregion
    }
    
}