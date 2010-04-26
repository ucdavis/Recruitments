using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace CAESDO.Recruitment.Web
{
    /// <summary>
    /// Summary description for AD419Configuration
    /// </summary>
    public static class RecruitmentConfiguration
    {
        /// <summary>
        /// Enum of the types of AssociationStatus' that can be passed to the getExpensesBySFN SPROC
        /// </summary>
        public enum ExpenseAssociationStatus
        {
            All = 0,
            Total = 1,
            Associated = 2,
            Unassociated = 3
        }

        /// <summary>
        /// Holds all of the different report types for the Reports tab of the ReportModule
        /// </summary>
        public enum ReportType
        {
            ProjectAD419 = 0,
            DepartmentAD419 = 1,
        }

        /// <summary>
        /// Holds the different sections of the ReportAdministration page
        /// </summary>
        public enum ReportAdministrationType
        {
            Expense, Interdepartmental, Association, CEEntry
        }

        /// <summary>
        /// Holds the different sections of the ReportModule
        /// </summary>
        public enum ReportingModuleType
        {
            ProjectInformation, Associations, Reports
        }

        /// <summary>
        /// Hold the type of errors that can occur in the system.  For use in the ErrorPage and ErrorMessage functions
        /// </summary>
        public enum ErrorType
        {
            DATA, AUTH, MAIL, SESSION, FILE, QUERY, UNKNOWN
        }

        /// <summary>
        /// All of the different LineTypes in GridViews -- this allows them to each be assigned a color in one place,
        /// so it is easy to change and centralized.
        /// </summary>
        public enum LineTypeCode
        {
            Heading, SFN, GroupSum, GrandTotal, FTE, FTETotal, AssociatedGrouping, UnassociatedGrouping
        }

        /// <summary>
        /// Name of the error page (could be read from the web.config in a future version)
        /// </summary>
        private static string ErrorPageName = "~/Error.aspx";

        //Const vars which hold the cell indexes for certain types of data.  Change here if the column orders are
        //ever changed, or if columns are added/deleted
        public const int cellIndexProjectSpent = 3;
        public const int cellIndexRecordsSpent = 4;
        public const int cellIndexProjectsFTE = 4;
        public const int cellIndexRecordsFTE = 5;
        public const int ViewProjectTotals = 4;
        public const int cellIndexSFNExpensesAmmount = 2;

        /// <summary>
        /// Returns the name of the page to redirect the user to for the specified error
        /// </summary>
        /// <param name="errorName">Error Name</param>
        /// <returns>String</returns>
        public static string ErrorPage(ErrorType error)
        {
            if (error == ErrorType.DATA)
                return ErrorPageName + "?reason=data";
            else if (error == ErrorType.AUTH)
                return ErrorPageName + "?reason=auth";
            else if (error == ErrorType.SESSION)
                return ErrorPageName + "?reason=session";
            else if (error == ErrorType.FILE)
                return ErrorPageName + "?reason=file";
            else if (error == ErrorType.QUERY)
                return ErrorPageName + "?reason=query";
            else
                return ErrorPageName + "?reason=unknown";
        }

        /// <summary>
        /// Returns the friendly error message from a given reason
        /// </summary>
        /// <param name="reason">ErrorType enum of from the AD419Configuration class</param>
        /// <returns>string</returns>
        public static string ErrorMessage(RecruitmentConfiguration.ErrorType error)
        {
            if (error == ErrorType.AUTH)
                return "Authentication Error";
            else if (error == ErrorType.DATA)
                return "Data Access Error";
            else if (error == ErrorType.SESSION)
                return "Session Expiration Error";
            else if (error == ErrorType.FILE)
                return "File Not Found Error";
            else if (error == ErrorType.QUERY)
                return "Query String Error";
            else
                return "Unknown Error";
        }

        /// <summary>
        /// Returns the friendly error message from a given reason
        /// </summary>
        /// <param name="reason">The short reason</param>
        /// <returns>string</returns>
        public static string ErrorMessage(string reason)
        {
            if (reason == null || reason == "unknown")
                return "Unknown Error";
            else if (reason == "data")
                return "Data Access Error";
            else if (reason == "auth")
                return "Authentication Error";
            else if (reason == "querystring")
                return "Invalid Selection";
            else if (reason == "session")
                return "Session Expiration";
            else if (reason == "file")
                return "File Not Found Exception";
            else if (reason == "query")
                return "Query String Not Valid";
            else if (reason == "mail")
                return "Email Send Error-- Could Not Reach All Recipients";
            else
                return "Unknown Error";
        }

        /// <summary>
        /// Returns a Color for the specific line code
        /// </summary>
        /// <param name="lineCode">The LineCode</param>
        /// <returns>The Color to paint the background.  For Hex color, use Color.FromName("#xxxxxx")</returns>
        public static Color LineColor(LineTypeCode lineCode)
        {
            switch (lineCode)
            {
                case LineTypeCode.Heading: return Color.FromName("#fdb671");
                case LineTypeCode.SFN: return Color.FromName("#e2ead4");
                case LineTypeCode.GroupSum: return Color.FromName("#c4d0ac");
                case LineTypeCode.GrandTotal: return Color.White;
                case LineTypeCode.FTETotal: return Color.White;
                case LineTypeCode.FTE: return Color.FromName("#d8e2ed");
                case LineTypeCode.AssociatedGrouping: return Color.Green;
                case LineTypeCode.UnassociatedGrouping: return Color.Blue;
                default: return Color.White;
            }
        }
    }
}
