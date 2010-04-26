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
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;
using System.Web.Configuration;

namespace CAESDO.Recruitment.Web
{

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private const string STR_CurrentUserType = "currentUserType";
        private const string STR_CreateUserURL = "~/Login/CreateUser.aspx";

        #region UserProperties

        private User _p;

        public User p
        {
            get
            {
                if (_p == null && HttpContext.Current.User.Identity.IsAuthenticated)
                    _p = daoFactory.GetUserDao().GetUserByLogin(HttpContext.Current.User.Identity.Name);

                return _p;
            }
            set { _p = value; }
        }

        private IDaoFactory daoFactory
        {
            get { return new NHibernateDaoFactory(); }
        }

        public string LoggedInUserName
        {
            get
            {
                string userName = string.Empty;

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (p == null)
                    {
                        userName = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        userName = p.FirstName + " " + p.LastName;
                    }
                }

                return userName;
            }
        }

        public string AssemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();        

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populate the questions email and assembly version

                litAssemblyVersion.Text = AssemblyVersion;

                hlinkEmail.NavigateUrl = "mailto:" + WebConfigurationManager.AppSettings["AppMailTo"] + "?subject=[" + WebConfigurationManager.AppSettings["AppName"] + "] " + AssemblyVersion + " <your question or comment>";
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            Response.Redirect(Request.Url.AbsolutePath);
        }

        protected void lbtnSignIn_Click(object sender, EventArgs e)
        {
            //Redirect the user to sign in
            Response.Redirect(FormsAuthentication.LoginUrl);
        }

        protected void lbtnCreate_Click(object sender, EventArgs e)
        {
            //Redirect the user to the create account page
            Response.Redirect(STR_CreateUserURL);
        }
}
}