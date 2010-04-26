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
using System.Collections.Generic;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_InterimReport : ApplicationPage
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
            if (currentPosition == null)
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            else if ( !Roles.IsUserInRole("Admin") ) //If the user isn't an admin, check department relationships
            {
                User u = daoFactory.GetUserDao().GetUserByLogin(User.Identity.Name);
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
            if (!IsPostBack)
            {
                if (currentPosition != null)
                {
                    lblDeadline.Text = currentPosition.Deadline.ToLongDateString();
                    lblDepartment.Text = currentPosition.DepartmentList;
                    lblPosition.Text = currentPosition.PositionTitle;
                    lblPosNum.Text = currentPosition.PositionNumber;

                    //Get the SexEthnicityCount for the current position
                    SexEthnicityCount SexEthnicityList = new SexEthnicityCount();

                    gviewSexEthnicity.DataSource = SexEthnicityList.GetSexEthnicityList(currentPosition, null);
                    gviewSexEthnicity.DataBind();

                    gviewApplicants.DataSource = this.GetApplicants(false); //Get the applicants who are not selected for interview
                    gviewApplicants.DataBind();

                    blistInterviewApplicants.DataSource = this.GetApplicants(true); //List the applicants selected for interview
                    blistInterviewApplicants.DataBind();

                    gviewInterviewSexEthnicity.DataSource = SexEthnicityList.GetSexEthnicityList(currentPosition, true);
                    gviewInterviewSexEthnicity.DataBind();
                }
            }
        }

        private List<Profile> GetApplicants(bool selectedForInterview)
        {
            List<Profile> profileList = new List<Profile>();

            foreach (Application app in currentPosition.AssociatedApplications)
            {
                if (app.InterviewList == selectedForInterview)
                {
                    profileList.Add(app.AssociatedProfile);
                }
            }

            return profileList;
        }
    }

    public class SexEthnicityCount
    {
        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private int _AmericanIndianCount;

        public int AmericanIndianCount
        {
            get { return _AmericanIndianCount; }
            set { _AmericanIndianCount = value; }
        }

        private int _AsianCount;

        public int AsianCount
        {
            get { return _AsianCount; }
            set { _AsianCount = value; }
        }

        private int _BlackCount;

        public int BlackCount
        {
            get { return _BlackCount; }
            set { _BlackCount = value; }
        }

        private int _ChicanoCount;

        public int ChicanoCount
        {
            get { return _ChicanoCount; }
            set { _ChicanoCount = value; }
        }

        private int _WhiteCount;

        public int WhiteCount
        {
            get { return _WhiteCount; }
            set { _WhiteCount = value; }
        }

        private int _UnidentifiedCount;

        public int UnidentifiedCount
        {
            get { return _UnidentifiedCount; }
            set { _UnidentifiedCount = value; }
        }

        private int _TotalCount;

        public int TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        }

        public SexEthnicityCount()
        {

        }

        /// <summary>
        /// Increments the ethnicity count for the given ethnicity
        /// </summary>
        private void AddEthnicity(Ethnicity ethnicity, SexEthnicityCount currentSex)
        {
            if (ethnicity == null) //If there is no specified ethnicity, increment the unidentified count
                currentSex.UnidentifiedCount++;
            else
            {
                switch (ethnicity.EthnicityCategory)
                {
                    case "American Indian":
                        currentSex.AmericanIndianCount++;
                        break;
                    case "Asian/ Asian American":
                        currentSex.AsianCount++;
                        break;
                    case "Black/ African American":
                        currentSex.BlackCount++;
                        break;
                    case "Chicano/ Latino/ Hispanic":
                        currentSex.ChicanoCount++;
                        break;
                    case "White":
                        currentSex.WhiteCount++;
                        break;
                    default:
                        currentSex.UnidentifiedCount++;
                        break;
                }
            }

            //No matter which category it falls under, update the total as well
            currentSex.TotalCount++;
        }
        
        /// <summary>
        /// For each sex and ethnicity category (uncluding unidentifed), add up the application pool composition
        /// </summary>
        /// <param name="selectedForInterview">Indicate if the list should contain people selected for interview or only applicants
        /// not selected for interview. A null parameter means select all applicants</param>
        public List<SexEthnicityCount> GetSexEthnicityList(Position position, bool? selectedForInterview)
        {
            SexEthnicityCount men = new SexEthnicityCount();
            SexEthnicityCount woman = new SexEthnicityCount();
            SexEthnicityCount unidentified = new SexEthnicityCount();
            SexEthnicityCount total = new SexEthnicityCount();

            men.Gender = "Men";
            woman.Gender = "Woman";
            unidentified.Gender = "Unidentified";
            total.Gender = "Total";

            foreach (Application app in position.AssociatedApplications)
            {
                
                if (selectedForInterview.HasValue == true)
                {
                    //If we specify a interview designation, make sure the application fits that designation
                    if (app.InterviewList != selectedForInterview)
                        continue;
                }

                //Make sure this application has a survey filled out
                if (app.Surveys != null && app.Surveys.Count > 0)
                {
                    Survey currentSurvey = app.Surveys[0];

                    if (currentSurvey.Gender == null)
                    {
                        //Unidentified
                        this.AddEthnicity(currentSurvey.Ethnicity, unidentified);
                    }
                    else if (currentSurvey.Gender.GenderType == "Male")
                    {
                        this.AddEthnicity(currentSurvey.Ethnicity, men);
                    }
                    else
                    {
                        this.AddEthnicity(currentSurvey.Ethnicity, woman);
                    }

                    //Always update the total as well
                    this.AddEthnicity(currentSurvey.Ethnicity, total);
                }
                else
                {
                    //If the survey is not filled out, place in the unidentified categories
                    this.AddEthnicity(null, unidentified);
                    this.AddEthnicity(null, total);
                }
            }

            List<SexEthnicityCount> result = new List<SexEthnicityCount>();
            result.Add(men);
            result.Add(woman);
            result.Add(unidentified);
            result.Add(total);

            return result;
        }
    }
}