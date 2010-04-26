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
    public partial class addPosition : ApplicationPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreatePosition_Click(object sender, EventArgs e)
        {
            Position newPosition = new Position();

            //Parse out all non-string/bool fields (ints/dates)
            int numPublications, numReferences;
            DateTime deadline;

            //Set the posted date to now
            newPosition.DatePosted = DateTime.Now;

            newPosition.PositionTitle = txtPositionTitle.Text;
            newPosition.PositionNumber = txtPositionNumber.Text;

            newPosition.HRRep = txtHRRep.Text;
            newPosition.HRPhone = txtHRPhone.Text;
            newPosition.HREmail = txtHREmail.Text;

            newPosition.ShortDescription = txtShortDescription.Text;

            newPosition.AllowApps = chkAllowApplications.Checked;

            foreach (Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult r in ValidateBO<Position>.GetValidationResults(newPosition))
            {
                txtShortDescription.Text += r.Key + " " + r.Message + Environment.NewLine;
            }
        }
}
}