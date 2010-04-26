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
        public IDaoFactory daoFactory
        {
            get { return new NHibernateDaoFactory(); }
        }

        public ApplicationPage()
        {
        }

        protected override void OnError(EventArgs e)
        {
            //Might want to rollback the transaction whenever an error gets this far up the stack
            //NHibernateSessionManager.Instance.RollbackTransaction();

            HttpContext ctx = HttpContext.Current;

            Exception ex = ctx.Server.GetLastError();

            //Only handle HttpException Errors
            if (ex.GetType().Name == "HttpException")
            {
                StringBuilder errorInfo = new StringBuilder();
                errorInfo.Append("Offending URL: " + ctx.Request.Url.ToString());
                errorInfo.Append("<br>Source: " + ex.Source);
                errorInfo.Append("<br>Type: " + ex.GetType().Name);
                errorInfo.Append("<br>Message: " + ex.Message);
                errorInfo.Append("<br>Stack Trace: " + ex.StackTrace);

                ctx.Response.Write(errorInfo.ToString());

                ctx.Server.ClearError();

                //Try a redirect
                ctx.Response.Redirect(ctx.Request.Url.ToString());
            }
            base.OnError(e);
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