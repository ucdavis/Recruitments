using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;

namespace CAESDO.Recruitment.Web
{
    public partial class _Default : ApplicationPage
    {
        public Application app11
        {
            get
            {
                if (Session["APP"] == null)
                {
                    IApplicationDao appDao = daoFactory.GetApplicationDao();
                    Session["APP"] = appDao.GetById(11, false);
                }

                NHibernateSessionManager.Instance.EnsureFreshness((Application)Session["APP"]);

                return (Application)Session["APP"];
            }

            set
            {
                Session["APP"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
    }
}