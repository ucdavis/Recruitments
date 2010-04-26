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
using CAESDO.Recruitment.Data;

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
            {
                gViewReferences.Visible = false;
                btnUpdateList.Visible = false;
                reqValApplications.Validate();
                return; //Return if the selected value is not a valid applicationID
            }

            gViewReferences.Visible = true; //Show the references grid
            btnUpdateList.Visible = true;

            Application currentApplication = daoFactory.GetApplicationDao().GetById(applicationID, false);

            gViewReferences.DataSource = currentApplication.References;
            gViewReferences.DataBind();
        }
        
        protected void btnUpdateList_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gViewReferences.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //Get the unsolicited check box and then current reference
                    CheckBox cboxUnsolicited = (CheckBox)row.FindControl("chkUnsolicited");
                    Reference currentReference = daoFactory.GetReferenceDao().GetById((int)gViewReferences.DataKeys[row.RowIndex]["id"], false);

                    //Only save the information is it is changed
                    if (currentReference.UnsolicitedReference != cboxUnsolicited.Checked)
                    {
                        using (new NHibernateTransaction())
                        {
                            currentReference.UnsolicitedReference = cboxUnsolicited.Checked;

                            daoFactory.GetReferenceDao().SaveOrUpdate(currentReference);
                        }
                    }
                }
            }

            //Notify the user that the update was successful
            lblResult.Text = "Unsolicited List Updated";
        }
}

}