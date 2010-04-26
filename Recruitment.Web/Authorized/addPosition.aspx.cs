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

        public IList<CAESDO.Recruitment.Core.Domain.Unit> DepartmentList
        {
            get
            {
                if (Session[STR_DepartmentList] == null)
                    return new List<CAESDO.Recruitment.Core.Domain.Unit>();
                else
                    return Session[STR_DepartmentList] as IList<CAESDO.Recruitment.Core.Domain.Unit>;
            }
            set
            {
                Session[STR_DepartmentList] = value;
            }
        }

        public FileType JobDescriptionFileType
        {
            get
            {
                FileType _JobDescriptionFileType = new FileType();
                _JobDescriptionFileType.FileTypeName = "JobDescription";

                return daoFactory.GetFileTypeDao().GetUniqueByExample(_JobDescriptionFileType);
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
            
            //Set the posted date to now
            newPosition.DatePosted = DateTime.Now;
            newPosition.Deadline = DateTime.Parse(txtDeadline.Text);

            newPosition.PositionTitle = txtPositionTitle.Text;
            newPosition.PositionNumber = txtPositionNumber.Text;

            newPosition.HRRep = string.IsNullOrEmpty(txtHRRep.Text) ? null : txtHRRep.Text;
            newPosition.HRPhone = string.IsNullOrEmpty(txtHRPhone.Text) ? null : txtHRPhone.Text;
            newPosition.HREmail = string.IsNullOrEmpty(txtHREmail.Text) ? null : txtHREmail.Text;

            //newPosition.Departments = DepartmentList;
            addDepartmentsToPosition(newPosition);

            newPosition.ShortDescription = txtShortDescription.Text;

            if (newPosition.ReferenceTemplate == null)
                newPosition.ReferenceTemplate = new ReferenceTemplate();

            newPosition.ReferenceTemplate.TemplateText = ftxtReferenceTemplate.Text;

            newPosition.NumPublications = int.Parse(txtPublications.Text);
            newPosition.NumReferences = int.Parse(txtReferences.Text);

            newPosition.AllowApps = chkAllowApplications.Checked;

            if (filePositionDescription.HasFile)
            {
                if (filePositionDescription.PostedFile.ContentType == "application/pdf")
                {
                    File jobDescription = new File();

                    jobDescription.FileName = filePositionDescription.FileName;
                    jobDescription.FileType = JobDescriptionFileType;

                    jobDescription = daoFactory.GetFileDao().Save(jobDescription);

                    if (ValidateBO<File>.isValid(jobDescription))
                    {
                        filePositionDescription.SaveAs(FilePath + jobDescription.ID.ToString());

                        newPosition.DescriptionFile = jobDescription;
                    }
                }
                else
                {
                    //Error message: Job Description Must Be a PDF File
                }
            }
            
            if (ValidateBO<Position>.isValid(newPosition))
                daoFactory.GetPositionDao().Save(newPosition);
            else
            {
                //Error message
            }
        }

        protected void lbtnAddDepartment_Click(object sender, EventArgs e)
        {
            CAESDO.Recruitment.Core.Domain.Unit selectedUnit = daoFactory.GetUnitDao().GetById(dlistDepartment.SelectedValue, false);

            DepartmentList.Add(selectedUnit);

            repDepartments.DataSource = DepartmentList;
            repDepartments.DataBind();
        }

        private void addDepartmentsToPosition(Position p)
        {
            p.Departments = new List<Department>(); //Create a fresh list of departments

            foreach (CAESDO.Recruitment.Core.Domain.Unit u in DepartmentList)
            {
                //Foreach unit, add it to the department list

                Department dept = new Department();
                dept.AssociatedPosition = p;    //Associate the departments with the given positiopns
                dept.DepartmentFIS = u.FISCode;

                p.Departments.Add(dept);
            }
        }
}
}