using System;
using System.Text;
using System.Web;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Core.Abstractions
{
    public interface ITemplateProcessing
    {
        string ProcessTemplate(Reference reference, Application application, string template, bool includeUploadPortion);
    }

    /// <summary>
    /// Summary description for TemplateProcessing
    /// </summary>
    public class TemplateProcessing : ITemplateProcessing
    {
        private const string STR_PositionDetailsURL = "/PositionDetails.aspx";
        private const string STR_UploadReferenceURL = "/UploadReference.aspx";

        private Application _application;
        private bool _includeUploadPortion = false;
        private Reference _reference;

        public TemplateProcessing()
        {
        }

        #region ITemplateProcessing Members

        public string ProcessTemplate(Reference reference,
                                      Application application, string template,
                                      bool includeUploadPortion)
        {
            _includeUploadPortion = includeUploadPortion;

            return ProcessTemplate(reference, application, template);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Iterates through the given template file and replaces any place where there is a template field marked with {}
        /// </summary>
        /// <param name="body">The template text.</param>
        /// <returns>Body text with fields populated with correct values.</returns>
        private string HandleBody(string body)
        {
            var sb = new StringBuilder();

            sb.Append("<html><body>");

            string parameter;

            int begindex = body.IndexOf("{"); // Find the beginning of a replacement string
            int endindex;
            while (begindex >= 0)
            {
                sb.Append(body.Substring(0, begindex));
                // Copy the text that comes before the replacement string to temp
                body = body.Substring(begindex); // Removes the first part of the string before the {

                endindex = body.IndexOf("}"); // Find the end of a replacement string

                parameter = body.Substring(0, endindex + 1); // Pulls the text between {}
                body = body.Substring(endindex + 1); // removes the parameter substring

                sb.Append(replaceParameter(parameter));

                begindex = body.IndexOf("{"); // Find the beginning of a replacement string
            }

            sb.Append(body);

            if (_includeUploadPortion)
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
                case "ReferenceName":
                    return _reference.FullName;
                    //if (this._reference.MiddleName.ToString() != "") // If the field is null, the ToString() will make it a blank string.
                    //    return this._reference.FirstName + " " + this._reference.MiddleName + " " + this._reference.LastName;
                    //else
                    //    return this._reference.FirstName + " " + this._reference.LastName;
                case "ReferenceLastName":
                    return _reference.LastName;
                case "ReferenceTitle":
                    return _reference.Title;
                case "ApplicantName":
                    if (string.IsNullOrEmpty(_application.AssociatedProfile.FullName.Trim()))
                        return "Name Not Given";
                    else
                        return _application.AssociatedProfile.FullName;

                    //if (this._application.AssociatedProfile.MiddleName.ToString() != "") // If the field is null, the ToString() will make it a blank string.
                    //return this._application.AssociatedProfile.FirstName + " " + this._application.AssociatedProfile.MiddleName + " " + this._application.AssociatedProfile.LastName;
                    //else
                    //  return this._application.AssociatedProfile.FirstName + " " + this._application.AssociatedProfile.LastName
                case "RecruitmentAdminName":
                    return _application.AppliedPosition.HRRep;
                case "RecruitmentAdminEmail":
                    return string.Format("<a href='mailto:{0}'>{0}</a>", _application.AppliedPosition.HREmail);
                case "PositionContact":
                    return _application.AppliedPosition.HRRep;
                case "PositionContactEmail":
                    return string.Format("<a href='mailto:{0}'>{0}</a>", _application.AppliedPosition.HREmail);
                    //return this._application.AppliedPosition.HREmail;
                case "PositionContactPhone":
                    return _application.AppliedPosition.HRPhone;
                case "Deadline": //deadline and reviewDate are the same token
                case "ReviewDate":
                    return _application.AppliedPosition.Deadline.ToLongDateString();
                case "PositionTitle":
                    //return this._application.AppliedPosition.PositionTitle;
                    return getPositionLink();
                case "PositionLink":
                    return getPositionLink();
                case "PrimaryDepartment":
                    if (_application.AppliedPosition.PrimaryDepartment.Unit != null)
                        return _application.AppliedPosition.PrimaryDepartment.Unit.FullName;
                    break;
                case "UploadLink":
                    _includeUploadPortion = false;
                    //Don't include the default upload portion since we are including it manually here
                    return getUploadIDPortion(); //now return the uploadID portion
                case "Date":
                    var date = new StringBuilder();
                    DateTime dateTime = DateTime.Now;

                    // Will convert the existing date into the following format : May 16, 2007
                    // Instead of something like 5/16/2007
                    date.Append(GetMonth(dateTime.Month));
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
            return "<a href='" +
                   HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) +
                   HttpContext.Current.Request.ApplicationPath + STR_UploadReferenceURL + "?ID=" +
                   _reference.UploadID + "'>Click here to upload reference letter</a>";
        }

        private string getPositionLink()
        {
            return string.Format("<a href='{0}?PositionID={1}'>{2}</a>",
                                 HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer,
                                                                               UriFormat.SafeUnescaped) +
                                 HttpContext.Current.Request.ApplicationPath + STR_PositionDetailsURL,
                                 _application.AppliedPosition.ID,
                                 _application.AppliedPosition.PositionTitle);
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
                default:
                    break;
            }
            return date;
        }

        #endregion

        public string ProcessTemplate(Reference reference,
                                      Application application, string template)
        {
            _reference = reference;
            _application = application;

            return HandleBody(template);
        }
    }
}