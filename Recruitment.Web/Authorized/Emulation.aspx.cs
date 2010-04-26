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

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_Emulation : ApplicationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginID_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();

            FormsAuthentication.Initialize();

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                txtLoginID.Text,
                DateTime.Now,
                DateTime.Now.AddMinutes(15),
                false,
                String.Empty,
                FormsAuthentication.FormsCookiePath);

            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

            Response.Cookies.Add(cookie);

            Response.Redirect(FormsAuthentication.DefaultUrl);
        }
}
}