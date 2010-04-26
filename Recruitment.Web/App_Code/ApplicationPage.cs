using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
// added
using System.Security.Principal;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;
using System.Web.SessionState;
using System.Web.Caching;
using System.Text;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using System.Web.Configuration;
using CAESDO.Recruitment.Core.Domain;


namespace CAESDO.Recruitment.Web
{
    /// <summary>
    /// All pages in the application except Login.aspx will derive from ApplicationPage.
    /// </summary>
    public class ApplicationPage : System.Web.UI.Page
    {
        // Principal to hold user data
        //protected CAESDORoleProvider CAESDORoles = (CAESDORoleProvider)Roles.Provider;
        //protected CAESDOMembershipProvider CAESDOMembership = (CAESDOMembershipProvider)Membership.Provider;
        //protected CAESDOUser CAESDOCurrentUser = new CAESDOUser(HttpContext.Current.User);
        //protected CAESDOPrincipal CAESDOPrincipalUser = new CAESDOPrincipal(HttpContext.Current.User.Identity);
        private User _p;

        public User p
        {
            get {
                if (_p == null && HttpContext.Current.User.Identity.IsAuthenticated)
                    _p = daoFactory.GetUserDao().GetUserByLogin(HttpContext.Current.User.Identity.Name);

                return _p; 
            }
            set { _p = value; }
        }
        
        public IDaoFactory daoFactory
        {
            get { return new NHibernateDaoFactory(); }
        }

        public string FilePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["RecruitmentFilePath"];
            }
        }

        public ErrorReporting eReport = new ErrorReporting(WebConfigurationManager.AppSettings["AppName"],
                                                        WebConfigurationManager.AppSettings["ErrorFromEmail"],
                                                        WebConfigurationManager.AppSettings["ErrorAdminEmail"]);

        public ApplicationPage()
        {
        }

        protected override void OnError(EventArgs e)
        {

            //Might want to rollback the transaction whenever an error gets this far up the stack
            NHibernateSessionManager.Instance.RollbackTransaction();

            //Grab the page context
            HttpContext ctx = HttpContext.Current;

            //Grab the exception that raised this error
            Exception ex = ctx.Server.GetLastError();

            //Only handle HttpException Errors
            if (ex.GetType().Name == "HttpException")
            {
                //Clear the error and redirect to the page the raised this error (getting a fresh copy)
                ctx.Server.ClearError();
                ctx.Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.SESSION));
            }
            else
            {
                if (ex.InnerException != null)
                    eReport.ReportError(ex.InnerException, "OnError");
                else
                    eReport.ReportError(ex, "OnError");

                ctx.Server.ClearError();
                ctx.Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            base.OnError(e); //won't get called
        }

        //#region Events
        ///// <summary>
        ///// Automatically invoked before the page is displayed
        ///// </summary>
        ///// <param name="e">Event Arguments</param>
        //protected override void OnLoad(EventArgs e)
        //{
        //    // Instantiate a new UserSession object
        //    userSession = new UserSession(this.Session);
        //    base.OnLoad(e);
        //}
        //#endregion

    }

}