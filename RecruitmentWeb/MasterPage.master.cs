using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
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
        private const int MajorVersion = 2;
        private const string VersionKey = "Version";

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

        /// <summary>
        /// Grabs the date time stamp and places the version in Cache if it does not exist
        /// and places the version in ViewData
        /// </summary>
        private string GetAssemblyVersion()
        {
            var version = HttpContext.Current.Cache[VersionKey] as string;

            if (string.IsNullOrEmpty(version))
            {
                var assembly = Assembly.GetExecutingAssembly();

                var buildDate = RetrieveLinkerTimestamp(assembly.Location);

                version = string.Format("{0}.{1}.{2}.{3}", MajorVersion, buildDate.Year, buildDate.Month,
                                            buildDate.Day);

                //Insert version into the cache until tomorrow (Today + 1 day)
                HttpContext.Current.Cache.Insert(VersionKey, version, null, DateTime.Today.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
            }

            return version;
        }

        /// <summary>
        /// Grabs the build linker time stamp
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/2050396/getting-the-date-of-a-net-assembly
        /// and
        /// http://www.codinghorror.com/blog/2005/04/determining-build-date-the-hard-way.html
        /// </remarks>
        private DateTime RetrieveLinkerTimestamp(string filePath)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var b = new byte[2048];
            System.IO.FileStream s = null;
            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                    s.Close();
            }
            var dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(BitConverter.ToInt32(b, BitConverter.ToInt32(b, peHeaderOffset) + linkerTimestampOffset));
            return dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populate the questions email and assembly version
                if (litAssemblyVersion != null) litAssemblyVersion.Text = GetAssemblyVersion();

                if (hlinkEmail != null)
                    hlinkEmail.NavigateUrl = "mailto:" + WebConfigurationManager.AppSettings["AppMailTo"] + "?subject=[" + WebConfigurationManager.AppSettings["AppName"] + "] " + GetAssemblyVersion() + " <your question or comment>";
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session["userdetails"] = null;

            Response.Redirect(FormsAuthentication.DefaultUrl);
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