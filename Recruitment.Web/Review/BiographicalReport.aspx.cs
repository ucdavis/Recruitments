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
using System.Collections.Generic;

namespace CAESDO.Recruitment.Web
{
    public partial class Review_BiographicalReport : ApplicationPage
    {
        #region CONST STINGS
            private const string STR_ExtensionInterests = "Extension Interests";
            private const string STR_TeachingInterests = "Teaching Interests";
            private const string STR_ConfidentialSurvey = "Confidential Survey";
            private const string STR_EducationInformation = "Education Information";
            private const string STR_CurrentApplication = "currentApplication";
            private const string STR_ApplicationSteps = "ApplicationSteps";
            private const string STR_ContactInformation = "Contact Information";
            private const string STR_References = "References";
            private const string STR_CurrentPosition = "Current Position";
            private const string STR_Resume = "Resume";
            private const string STR_CoverLetter = "Cover Letter";
            private const string STR_ResearchInterests = "Research Interests";
            private const string STR_Transcripts = "Transcripts";
            private const string STR_Publications = "Publications";
            private const string STR_Dissertation = "Dissertation";
            private const string STR_FileType_Transcript = "Transcript";
            private const string STR_FileType_CoverLetter = "CoverLetter";
            private const string STR_FileType_ResearchInterests = "ResearchInterests";
            private const string STR_FileType_ExtensionInterests = "ExtensionInterests";
            private const string STR_FileType_TeachingInterests = "TeachingInterests";
            private const string STR_FileType_Publication = "Publication";
            private const string STR_CV = "CV"; 
        #endregion

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
                {
                    //Only committee members should have access
                    if ( member.MemberType.ID == (int)MemberTypes.CommitteeChair || member.MemberType.ID == (int)MemberTypes.CommitteeMember )
                    {
                        allowedAccess = true;
                        break;
                    }
                }
            }

            if (!allowedAccess)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
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

        /// <summary>
        /// For each application, determine the incomplete steps
        /// </summary>
        protected void gViewBiographicalData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) //Only handle data rows
                return;

            int applicationID = (int)gViewBiographicalData.DataKeys[e.Row.RowIndex]["id"];

            Application currentApplication = daoFactory.GetApplicationDao().GetById(applicationID, false);

            if (currentApplication.Submitted == true) //Don't worry about submitted applications
                return;

            //Place all of the errors into this error string, and mark isComplete false if we fail a step
            List<string> incompleteSteps = new List<string>();

            //Loop through each required step and make sure it is completed
            foreach (ApplicationStepType step in currentApplication.AppliedPosition.Steps)
            {
                switch (step)
                {
                    case ApplicationStepType.CurrentPosition:
                        if (!currentApplication.isComplete(ApplicationStepType.CurrentPosition))
                            incompleteSteps.Add(STR_CurrentPosition);
                        break;
                    case ApplicationStepType.Education:
                        if (!currentApplication.isComplete(ApplicationStepType.Education))
                            incompleteSteps.Add(STR_EducationInformation);
                        break;
                    case ApplicationStepType.Survey:
                        //if (!currentApplication.isComplete(ApplicationStepType.Survey))
                        //    incompleteSteps.Add(STR_ConfidentialSurvey);                        
                        break;
                    case ApplicationStepType.References:
                        //if (!currentApplication.ReferencesComplete)
                        //    incompleteSteps.Add(STR_References);                        
                        break;
                    default:
                        break;
                }
            }

            //Always check references and the confidential survey
            if (!currentApplication.isComplete(ApplicationStepType.Survey))
                incompleteSteps.Add(STR_ConfidentialSurvey);

            if (!currentApplication.ReferencesComplete)
                incompleteSteps.Add(STR_References);

            //Loop through each requested file type and make sure a file of the given type exists
            foreach (FileType type in currentApplication.AppliedPosition.FileTypes)
            {
                switch (type.FileTypeName)
                {
                    case STR_FileType_CoverLetter:
                        //Cover letter is incomplete only if the "coverletter complete" bool is not true AND there are no cover letters
                        if (!currentApplication.CoverLetterComplete && !existsFileOfType(STR_FileType_CoverLetter, currentApplication))
                            incompleteSteps.Add(STR_CoverLetter);
                        break;
                    case STR_FileType_ExtensionInterests:
                        if (!existsFileOfType(STR_FileType_ExtensionInterests, currentApplication))
                            incompleteSteps.Add(STR_ExtensionInterests);
                        break;
                    case STR_FileType_Publication:
                        if (!currentApplication.PublicationsComplete)
                            incompleteSteps.Add(STR_Publications);
                        break;
                    case STR_FileType_ResearchInterests:
                        if (!existsFileOfType(STR_FileType_ResearchInterests, currentApplication))
                            incompleteSteps.Add(STR_ResearchInterests);
                        break;
                    case STR_FileType_TeachingInterests:
                        if (!existsFileOfType(STR_FileType_TeachingInterests, currentApplication))
                            incompleteSteps.Add(STR_TeachingInterests);
                        break;
                    case STR_FileType_Transcript:
                        if (!existsFileOfType(STR_FileType_Transcript, currentApplication))
                            incompleteSteps.Add(STR_Transcripts);
                        break;
                    case STR_Resume:
                        if (!existsFileOfType(STR_Resume, currentApplication))
                            incompleteSteps.Add(STR_Resume);
                        break;
                    case STR_CV:
                        if (!existsFileOfType(STR_CV, currentApplication))
                            incompleteSteps.Add(STR_CV);
                        break;
                    case STR_Dissertation:
                        if (!existsFileOfType(STR_Dissertation, currentApplication))
                            incompleteSteps.Add(STR_Dissertation);
                        break;
                    default:
                        break;
                }
            }
                        
            //If there are any incompleteSteps, alert the user, else complete the application and redirect
            if (incompleteSteps.Count > 0)
            {
                StringBuilder missingSteps = new StringBuilder("Missing: ");
                
                incompleteSteps.Sort();

                foreach (string s in incompleteSteps)
                {
                    missingSteps.Append(s);
                    missingSteps.Append(", ");
                }

                missingSteps.Remove(missingSteps.Length - 2, 2); //Remove the last extra comma and space

                Literal litMissingSteps = e.Row.FindControl("litMissingSteps") as Literal;
                litMissingSteps.Text = missingSteps.ToString();
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

        /// <summary>
        /// Determines if the current application contains a file of the given type
        /// </summary>
        /// <param name="fileTypeName">File type name</param>
        /// <returns>True if a file with the given type exists</returns>
        private bool existsFileOfType(string fileTypeName, Application currentApplication)
        {
            foreach (File f in currentApplication.Files)
            {
                if (f.FileType.FileTypeName == fileTypeName)
                    return true;
            }

            return false;
        }


}


}