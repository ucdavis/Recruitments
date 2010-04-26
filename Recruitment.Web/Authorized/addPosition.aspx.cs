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
using System.Collections.Generic;

namespace CAESDO.Recruitment.Web
{
    public partial class addPosition : ApplicationPage
    {
        private const string STR_DepartmentList = "DepartmentList";

        public List<CAESDO.Recruitment.Core.Domain.Unit> DepartmentList
        {
            get
            {
                if (Session[STR_DepartmentList] == null)
                    return new List<CAESDO.Recruitment.Core.Domain.Unit>();
                else
                    return Session[STR_DepartmentList] as List<CAESDO.Recruitment.Core.Domain.Unit>;
            }
            set
            {
                Session[STR_DepartmentList] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DepartmentList = new List<CAESDO.Recruitment.Core.Domain.Unit>();


        }

        protected void btnCreatePosition_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            Position newPosition = new Position();

            //Parse out all non-string/bool fields (ints/dates)
            int numPublications, numReferences;
            DateTime deadline;

            //Set the posted date to now
            newPosition.DatePosted = DateTime.Now;

            newPosition.PositionTitle = txtPositionTitle.Text;
            newPosition.PositionNumber = txtPositionNumber.Text;

            newPosition.HRRep = string.IsNullOrEmpty(txtHRRep.Text) ? null : txtHRRep.Text;
            newPosition.HRPhone = string.IsNullOrEmpty(txtHRPhone.Text) ? null : txtHRPhone.Text;
            newPosition.HREmail = string.IsNullOrEmpty(txtHREmail.Text) ? null : txtHREmail.Text;

            newPosition.ShortDescription = txtShortDescription.Text;

            newPosition.AllowApps = chkAllowApplications.Checked;
            
            foreach (Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult r in ValidateBO<Position>.GetValidationResults(newPosition))
            {
                txtShortDescription.Text += r.Key + " " + r.Message + Environment.NewLine;
            }
        }

        protected void lbtnAddDepartment_Click(object sender, EventArgs e)
        {
            CAESDO.Recruitment.Core.Domain.Unit selectedUnit = daoFactory.GetUnitDao().GetById(dlistDepartment.SelectedValue, false);

            DepartmentList.Add(selectedUnit);

            repDepartments.DataSource = DepartmentList;
            repDepartments.DataBind();
        }
}
}