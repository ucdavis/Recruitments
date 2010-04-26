using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

namespace CAESDO
{
    /// <summary>
    /// Summary description for RecruitmentErrorReporting
    /// </summary>
    public static class RecruitmentErrorReporting
    {
        /// <summary>
        /// Creates a message out of the exception and calling function that
        /// includes debugging information such as the exception source and a
        /// stack trace.  The message is then emailed to the appropriate person
        /// via smtp.ucdavis.edu
        /// </summary>
        /// <param name="excep">The exception that was raised</param>
        /// <param name="callingFunction">The function that the exception is in</param>
        public static void ReportError(Exception excep, string callingFunction)
        {
            string mess = "Recruitments Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            mess += "\n\n Error Called From Function :" + callingFunction;
            //Create an Exception object from the Last error that occurred on the server
            Exception myError = excep;

            mess += "\n\n Error Message :" + myError.Message; //Get the error message
            mess += "\n\n Error Source :" + myError.Source;  //Source of the message
            mess += "\n\n Error Stack Trace :" + myError.StackTrace; //Stack Trace of the error
            mess += "\n\n Error TargetSite :" + myError.TargetSite; //Method where the error occurred


            //Create a Mail Message
                        
            System.Net.Mail.SmtpClient errorMessage = new System.Net.Mail.SmtpClient();
            //errorMessage.Host = WebConfigurationManager.AppSettings["mailHost"].ToString();
            errorMessage.Send(WebConfigurationManager.AppSettings["ErrorFromEmail"].ToString(),
                                WebConfigurationManager.AppSettings["ErrorAdminEmail"].ToString(),
                                "CAESDO Recruitments Error Reporting", mess);

        }
    } 
}
