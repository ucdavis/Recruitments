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

public partial class MasterPage : System.Web.UI.MasterPage
{
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();

        Response.Redirect(Request.Url.AbsolutePath);
    }
}
