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

namespace CAESDO.Recruitment.Web
{
    public partial class UploadReference : ApplicationPage
    {
        public Reference currentReference
        {
            get
            {
                return daoFactory.GetReferenceDao().GetReferenceByUploadID(Request.QueryString["ID"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (currentReference == null)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }

            if (!IsPostBack)
            {
                lblInfo.Text = "Reference for " + currentReference.AssociatedApplication.AssociatedProfile.FullName;
            }
        }
    } 
}
