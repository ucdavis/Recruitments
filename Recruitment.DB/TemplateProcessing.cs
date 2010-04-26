using System;
using System.Data;
using System.Configuration;

using System.Text;
using System.Data.SqlClient;

namespace CAESDO.Recruitment
{
    /// <summary>
    /// Summary description for TemplateProcessing
    /// </summary>
    public class TemplateProcessing
    {
        public TemplateProcessing()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string template = "Dear {ApplicantName}<i></i>,<br>  <br> Your application for the {PositionTitle} position with the "
                                   + "Department of {PrimaryDepartment} at the University of California, Davis has not been completed. While the position is open until filled, review of completed"
                                   + " applications will begin on {ReviewDate}. To ensure your application is considered, please complete your application prior to this date. <br> <br>"
                                   + "If you feel you have received this message in error, or have questions regarding your application status, please contact us at <a href=\"mailto:{RecruitmentAdminEmail}\">{RecruitmentAdminEmail}</a>.";

        private UserInfo userInfo;

        public string ProcessTemplate(UserInfo info)
        {
            userInfo = info;

            return this.HandleBody(template).Replace("'", "''");
        }

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

                sb.Append(this.replaceParameter(parameter));

                begindex = body.IndexOf("{"); // Find the beginning of a replacement string
            }

            sb.Append(body);
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
                case "ApplicantName":
                    return userInfo.FullName;
                case "Deadline":
                case "ReviewDate":
                    return userInfo.Position.Deadline.ToLongDateString();
                case "PositionTitle":
                    return userInfo.Position.PositionTitle;
                case "RecruitmentAdminEmail":
                    return userInfo.Position.RecruitmentEmail;
                case "PrimaryDepartment":
                    return userInfo.Position.PrimaryDepartmentName;
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
    }

    public class UserInfo
    {
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        private PositionInfo _Position;

        public PositionInfo Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public UserInfo(SqlDataReader rdr)
        {
            Email = rdr["Email"].ToString();
            FirstName = rdr["FirstName"].ToString();
            LastName = rdr["LastName"].ToString();

            Position = new PositionInfo();
            Position.Deadline = (DateTime)rdr["Deadline"];
            Position.PositionTitle = rdr["PositionTitle"].ToString();
            Position.RecruitmentEmail = rdr["RecruitmentEmail"].ToString();
            Position.PrimaryDepartmentName = (string)rdr["FullName"];
        }
    }

    public class PositionInfo
    {
        private string _PositionTitle;

        public string PositionTitle
        {
            get { return _PositionTitle; }
            set { _PositionTitle = value; }
        }

        private DateTime _Deadline;

        public DateTime Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }

        private string _RecruitmentEmail;

        public string RecruitmentEmail
        {
            get { return _RecruitmentEmail; }
            set { _RecruitmentEmail = value; }
        }

        private string _PrimaryDepartmentName;

        public string PrimaryDepartmentName
        {
            get { return _PrimaryDepartmentName; }
            set { _PrimaryDepartmentName = value; }
        }
                
        public PositionInfo()
        {

        }
    }
}