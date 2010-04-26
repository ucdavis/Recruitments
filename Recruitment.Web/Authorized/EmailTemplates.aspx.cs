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
    public partial class Authorized_EmailTemplates : ApplicationPage
    {
        public TemplateType ReminderTemplateType
        {
            get
            {
                TemplateType _ReminderTemplateType = new TemplateType();
                _ReminderTemplateType.Type = "Reminder";

                return daoFactory.GetTemplateTypeDao().GetUniqueByExample(_ReminderTemplateType);
            }
        }

        public Template ReferenceTemplate
        {
            get
            {
                return daoFactory.GetTemplateDao().GetTemplatesByType(ReminderTemplateType)[0];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litEmailBody.Text = ReferenceTemplate.TemplateText;
            }
        }
    }
}