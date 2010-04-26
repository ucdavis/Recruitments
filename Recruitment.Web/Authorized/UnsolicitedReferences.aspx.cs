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
using System.Collections.Generic;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_UnsolicitedReferences : ApplicationPage
    {
        public TemplateType UnsolicitedTemplateType
        {
            get
            {
                TemplateType _UnsolicitedTemplateType = new TemplateType();
                _UnsolicitedTemplateType.Type = "Unsolicited";

                return daoFactory.GetTemplateTypeDao().GetUniqueByExample(_UnsolicitedTemplateType);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void dlistApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            int applicationID = 0;

            if (!int.TryParse(dlistApplications.SelectedValue, out applicationID))
                return; //Return if the selected value is not a valid applicationID (this should not happen).

            gViewReferences.Visible = true; //Show the references grid

            Application currentApplication = daoFactory.GetApplicationDao().GetById(applicationID, false);

            gViewReferences.DataSource = currentApplication.References;
            gViewReferences.DataBind();
        }
}

}