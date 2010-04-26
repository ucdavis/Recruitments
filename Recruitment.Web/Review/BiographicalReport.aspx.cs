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
using System.Text;

namespace CAESDO.Recruitment.Web
{
    public partial class Review_BiographicalReport : ApplicationPage
    {
        public int? currentPositionID
        {
            get
            {
                int posID = 0;

                if (int.TryParse(Request.QueryString["PositionID"], out posID))
                {
                    //If the parse succeeded, return the integer
                    return posID;
                }
                else
                {
                    return null;
                }
            }
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
            if ( currentPosition == null )
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.QUERY));

            bool allowedAccess = false;

            foreach (DepartmentMember member in currentPosition.PositionCommittee)
            {
                if (member.LoginID == User.Identity.Name)
                    allowedAccess = true;
            }

            if (!allowedAccess)
            {
                Response.Write("Not Allowed Access");
                //Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //currentPosition.AssociatedApplications[0].Submitted
            if (!IsPostBack)
            {
                gViewBiographicalData.DataSource = currentPosition.AssociatedApplications;
                gViewBiographicalData.DataBind();
            }
        }

        public static string GetPHDAwardedString(DateTime time)
        {
            string phdStatus = string.Empty;

            if (time > DateTime.Now) //If the ph.d. date is in the future
                phdStatus = "Expected ";
            else
                phdStatus = "Awarded ";

            return phdStatus + time.ToString("MMMM yyyy");
        }

        public static string GetEmploymentString(CurrentPosition position)
        {
            if (position == null)
                return string.Empty;

            StringBuilder employmentString = new StringBuilder();
            string br = "<br />";
            string space = " ";

            employmentString.Append(position.Title);
            employmentString.Append(br);

            employmentString.Append(position.Department);
            employmentString.Append(br);

            employmentString.Append(position.Address1);
            employmentString.Append(br);

            if (!string.IsNullOrEmpty(position.Address2))
            {
                employmentString.Append(position.Address2);
                employmentString.Append(br);
            }

            employmentString.Append(position.City);
            employmentString.Append(space);
            
            employmentString.Append(position.State);
            employmentString.Append(space);

            employmentString.Append(position.Zip);

            return employmentString.ToString();
        }
    }


}