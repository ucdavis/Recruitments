using System.Web;
using System;
using System.Text;

namespace CAESDO.Recruitment
{
    /// <summary>
    /// Summary description for TemplateProcessing
    /// </summary>
    public class TemplateProcessing
    {
        private const string STR_UploadReferenceURL = "/UploadReference.aspx";
        private const string STR_PositionDetailsURL = "/PositionDetails.aspx";

        public TemplateProcessing()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private Recruitment.Core.Domain.Reference _reference;
        private Recruitment.Core.Domain.Application _application;
        private bool _includeUploadPortion = false;

        public string ProcessTemplate(Recruitment.Core.Domain.Reference reference, Recruitment.Core.Domain.Application application, string template)
        {
            this._reference = reference;
            this._application = application;

            return this.HandleBody(template);
        }

        public string ProcessTemplate(Recruitment.Core.Domain.Reference reference, Recruitment.Core.Domain.Application application, string template, bool includeUploadPortion)
        {
            this._includeUploadPortion = includeUploadPortion;

            return this.ProcessTemplate(reference, application, template);
        }

        #region Private Methods
        /// <summary>
        /// Iterates through the given template file and replaces any place where there is a template field marked with {}
        /// </summary>
        /// <param name="body">The template text.</param>
        /// <returns>Body text with fields populated with correct values.</returns>
        private string HandleBody(string body)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<html><body>");
            
            string parameter;

            int begindex = body.IndexOf("{"); // Find the beginning of a replacement string
            int endindex;
            while (begindex > 0)
            {
                sb.Append(body.Substring(0, begindex)); // Copy the text that comes before the replacement string to temp
                body = body.Substring(begindex);        // Removes the first part of the string before the {

                endindex = body.IndexOf("}"); // Find the end of a replacement string

                parameter = body.Substring(0, endindex + 1);   // Pulls the text between {}
                body = body.Substring(endindex + 1);    // removes the parameter substring

                sb.Append( this.replaceParameter(parameter));

                begindex = body.IndexOf("{"); // Find the beginning of a replacement string
            }

            sb.Append(body);
            
            if ( this._includeUploadPortion )
                sb.Append(getUploadIDPortion());
            
            sb.Append("</body></html>");

            return sb.ToString();
        }
        /// <summary>
        /// Converts the parameter name into the correct value.
        /// </summary>
        /// <param name="parameter">Parameter name from the template.</param>
        /// <returns>Value that should be put into the space where the field was.</returns>
        private string replaceParameter(string parameter)
        {
            // Trim the {}
            int length = parameter.Length;
            parameter = parameter.Substring(1, length - 2);

            switch (parameter)
            {
                case "ReferenceName" :
                    return this._reference.FullName;
                    //if (this._reference.MiddleName.ToString() != "") // If the field is null, the ToString() will make it a blank string.
                    //    return this._reference.FirstName + " " + this._reference.MiddleName + " " + this._reference.LastName;
                    //else
                    //    return this._reference.FirstName + " " + this._reference.LastName;
                case "ReferenceLastName":
                    return this._reference.LastName;
                case "ReferenceTitle" :
                    return this._reference.Title;
                case "ApplicantName" :
                    if (string.IsNullOrEmpty(this._application.AssociatedProfile.FullName.Trim()))
                        return "Name Not Given";
                    else
                        return this._application.AssociatedProfile.FullName;

                    //if (this._application.AssociatedProfile.MiddleName.ToString() != "") // If the field is null, the ToString() will make it a blank string.
                        //return this._application.AssociatedProfile.FirstName + " " + this._application.AssociatedProfile.MiddleName + " " + this._application.AssociatedProfile.LastName;
                    //else
                      //  return this._application.AssociatedProfile.FirstName + " " + this._application.AssociatedProfile.LastName
                case "RecruitmentAdminName":
                    return this._application.AppliedPosition.HRRep;
                case "RecruitmentAdminEmail":
                    return string.Format("<a href='mailto:{0}'>{0}</a>", this._application.AppliedPosition.HREmail);
                case "PositionContact" :
                    return this._application.AppliedPosition.HRRep;
                case "PositionContactEmail" :
                    return string.Format("<a href='mailto:{0}'>{0}</a>", this._application.AppliedPosition.HREmail);
                    //return this._application.AppliedPosition.HREmail;
                case "PositionContactPhone" :
                    return this._application.AppliedPosition.HRPhone;
                case "Deadline": //deadline and reviewDate are the same token
                case "ReviewDate":
                    return this._application.AppliedPosition.Deadline.ToLongDateString();
                case "PositionTitle": 
                    //return this._application.AppliedPosition.PositionTitle;
                    return this.getPositionLink();
                case "PositionLink":
                    return this.getPositionLink();
                case "PrimaryDepartment":
                    if ( this._application.AppliedPosition.PrimaryDepartment.Unit != null )
                        return this._application.AppliedPosition.PrimaryDepartment.Unit.FullName;
                    break;
                case "UploadLink":
                    this._includeUploadPortion = false; //Don't include the default upload portion since we are including it manually here
                    return getUploadIDPortion(); //now return the uploadID portion
                case "Date":
                    StringBuilder date = new StringBuilder();
                    DateTime dateTime = DateTime.Now;

                    // Will convert the existing date into the following format : May 16, 2007
                    // Instead of something like 5/16/2007
                    date.Append(this.GetMonth(dateTime.Month));
                    date.Append(" " + dateTime.Day);
                    date.Append(", " + dateTime.Year);

                    return date.ToString();
                default:
                    //return string.Empty;
                    break;
            }
#if DEBUG
            return "Error";
#else
            return string.Empty;
#endif
        }

        private string getUploadIDPortion()
        {
            return "<a href='" + HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + HttpContext.Current.Request.ApplicationPath + STR_UploadReferenceURL + "?ID=" + this._reference.UploadID + "'>Click here to upload reference letter</a>";
        }        

        private string getPositionLink()
        {
            return string.Format("<a href='{0}?PositionID={1}'>{2}</a>", 
                HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + HttpContext.Current.Request.ApplicationPath + STR_PositionDetailsURL, 
                this._application.AppliedPosition.ID,
                this._application.AppliedPosition.PositionTitle);
        }

        // Takes an int representation of Month and returns the string name
        private string GetMonth(int month)
        {
            string date = "";

            switch (month)
            {
                case 1:
                    date = "January";
                    break;
                case 2:
                    date = "February";
                    break;
                case 3:
                    date = "March";
                    break;
                case 4:
                    date = "April";
                    break;
                case 5:
                    date = "May";
                    break;
                case 6:
                    date = "June";
                    break;
                case 7:
                    date = "July";
                    break;
                case 8:
                    date = "August";
                    break;
                case 9:
                    date = "September";
                    break;
                case 10:
                    date = "October";
                    break;
                case 11:
                    date = "November";
                    break;
                case 12:
                    date = "December";
                    break;
                default: break;
            }
            return date;
        }
        #endregion
    }
}