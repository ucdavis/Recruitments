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

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_ShortList : ApplicationPage
    {
        private const string STR_ApplicationReview = "ApplicationReview.aspx";

        public Position currentPosition
        {
            get
            {
                if (dlistPositions.SelectedIndex == 0)
                    return null;
                else
                    return daoFactory.GetPositionDao().GetById(int.Parse(dlistPositions.SelectedValue), false);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: Create a shortlist table in Recruitments which maps one Position to many applications (use PosXDept as your guide)
            //      Add an IList of Applications to the positions class, and enable cascading.

            // TODO: Loop through all of the checked applicants in the grid and add them to the positions.shortlistApplications list 
            //          (don't forget to clear the list first if there was a previous short list created).
        }

        public string GetNullSafeFullName(string FullName)
        {
            return string.IsNullOrEmpty(FullName.Trim()) ? "Name Not Yet Given" : FullName;
        }

        protected void ObjectDataApplications_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["position"] = currentPosition;
        }

        protected void dlistPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            gviewApplications.DataBind();

            if (gviewApplications.Rows.Count > 0)
                btnUpdateShortList.Visible = true;
            else
                btnUpdateShortList.Visible = false;
        }

        protected void btnUpdateShortList_Click(object sender, EventArgs e)
        {
            using (new NHibernateTransaction())
            {
                foreach (GridViewRow row in gviewApplications.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Application app = daoFactory.GetApplicationDao().GetById((int)gviewApplications.DataKeys[row.RowIndex]["id"], false);

                        app.ShortList = ((CheckBox)row.FindControl("chkShortList")).Checked;

                        daoFactory.GetApplicationDao().SaveOrUpdate(app);
                    }
                }
            }
        }

        protected void lbtnViewApplication_Click(object sender, EventArgs e)
        {
            string applicationID = ((LinkButton)sender).CommandArgument;

            Response.Redirect(string.Format("{0}?ApplicationID={1}", STR_ApplicationReview, applicationID));
        }
}
}