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
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_RecruitmentSources : System.Web.UI.UserControl, IReportUserControl
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
            SurveyResults surveys = new SurveyResults();
            surveys.ParseSurveyResults(currentPosition);

            List<SurveyResults> surveyResults = new List<SurveyResults>();
            surveyResults.Add(surveys);

            gviewSourcesCount.DataSource = surveyResults;
            gviewSourcesCount.DataBind();

            gviewSourcesText.DataSource = surveyResults;
            gviewSourcesText.DataBind();
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

    public class SurveyResults
    {
        private int _PublicationCount;

        public int PublicationCount
        {
            get { return _PublicationCount; }
            set { _PublicationCount = value; }
        }

        private int _ProfessionalOrgCount;

        public int ProfessionalOrgCount
        {
            get { return _ProfessionalOrgCount; }
            set { _ProfessionalOrgCount = value; }
        }

        private int _UCDAnnouncementCount;

        public int UCDAnnouncementCount
        {
            get { return _UCDAnnouncementCount; }
            set { _UCDAnnouncementCount = value; }
        }

        private int _InquiryCount;

        public int InquiryCount
        {
            get { return _InquiryCount; }
            set { _InquiryCount = value; }
        }

        private int _FriendCount;

        public int FriendCount
        {
            get { return _FriendCount; }
            set { _FriendCount = value; }
        }

        private int _OtherCount;

        public int OtherCount
        {
            get { return _OtherCount; }
            set { _OtherCount = value; }
        }

        private int _TotalCount;

        public int TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        }

        private string _PublicationSources;

        public string PublicationSources
        {
            get { return _PublicationSources; }
            set { _PublicationSources = value; }
        }

        private string _ProfessionalOrgSources;

        public string ProfessionalOrgSources
        {
            get { return _ProfessionalOrgSources; }
            set { _ProfessionalOrgSources = value; }
        }

        private string _OtherSources;

        public string OtherSources
        {
            get { return _OtherSources; }
            set { _OtherSources = value; }
        }

        public SurveyResults()
        {

        }

        //private void AddSources(Survey survey)
        //{
        //    if (!string.IsNullOrEmpty(survey.Pub_Advertisement)) //If the publication advertisment is specified
        //        this.SafeAdd(this.PublicationSources, survey.Pub_Advertisement);

        //    if (!string.IsNullOrEmpty(survey.Prof_Organization))
        //        this.SafeAdd(this.ProfessionalOrgSources, survey.Prof_Organization);

        //    if (!string.IsNullOrEmpty(survey.Other))
        //        this.SafeAdd(this.OtherSources, survey.Other);
        //}

        /// <summary>
        /// returns a string to be added to an existing string, separated by BRs if necessary
        /// </summary>
        private string SafeAdd(string source, string newText)
        {
            if (string.IsNullOrEmpty(source))
                return newText;
            else
                return "<br />" + newText;
        }

        private void AddSource(SourceType type, string sourceText)
        {
            if (string.IsNullOrEmpty(sourceText))
                return;

            switch (type)
            {
                case SourceType.Publication:
                    this.PublicationSources += this.SafeAdd(this.PublicationSources, sourceText);
                    break;
                case SourceType.ProfessionalOrg:
                    this.ProfessionalOrgSources += this.SafeAdd(this.ProfessionalOrgSources, sourceText);
                    break;
                case SourceType.Other:
                    this.OtherSources += this.SafeAdd(this.OtherSources, sourceText);
                    break;

            }
        }

        public void ParseSurveyResults(Position position)
        {
            foreach (Application app in position.AssociatedApplications)
            {
                if (app.Submitted == false) continue; //Don't count unsubmitted applications

                if (app.Surveys != null && app.Surveys.Count > 0) //Make sure surveys exist and the app was submitted
                {
                    Survey currentSurvey = app.Surveys[0];

                    //Add up all of the recruitment sources
                    foreach (SurveyXRecruitmentSrc recSource in currentSurvey.RecruitmentSources)
                    {
                        switch (recSource.RecruitmentSrc.RecruitmentSource)
                        {
                            case "Publication Advertisement":
                                this.PublicationCount++;
                                this.AddSource(SourceType.Publication, recSource.RecruitmentSrcOther);
                                break;
                            case "Professional Organization":
                                this.ProfessionalOrgCount++;
                                this.AddSource(SourceType.ProfessionalOrg, recSource.RecruitmentSrcOther);
                                break;
                            case "UC Davis Position Announcement":
                                this.UCDAnnouncementCount++;
                                break;
                            case "General Inquiry":
                                this.InquiryCount++;
                                break;
                            case "Friend or Colleague":
                                this.FriendCount++;
                                break;
                            case "Other":
                                this.OtherCount++;
                                this.AddSource(SourceType.Other, recSource.RecruitmentSrcOther);
                                break;
                            default:
                                break;
                        }
                    }

                    //Add the current survey's sources
                    //this.AddSources(currentSurvey);
                }

                this.TotalCount++;
            }
        }
    }

    public enum SourceType
    {
        Publication,
        ProfessionalOrg,
        Other
    }

}