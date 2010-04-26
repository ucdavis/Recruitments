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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (currentPosition != null)
                {
                    //Get the SexEthnicityCount for the current position
                    SexEthnicityCount SexEthnicityList = new SexEthnicityCount();

                    gviewSexEthnicity.DataSource = SexEthnicityList.GetSexEthnicityList(currentPosition);
                    gviewSexEthnicity.DataBind();
                }
            }
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
        public List<SexEthnicityCount> GetSexEthnicityList(Position position)
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