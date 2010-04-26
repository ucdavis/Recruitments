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
using CAESDO.Recruitment.Data;

namespace CAESDO.Recruitment.Web
{
    public partial class PositionManagement : ApplicationPage
    {
        private const string STR_DepartmentList = "DepartmentList";

        public int? currentPositionID
        {
            get
            {
                int posID = 0;

                if (int.TryParse(Request.QueryString["PositionID"], out posID))
                {
                    //If the parse succeeded, return the integer
                    return posID;
                }
                else
                {
                    return null;
                }
            }
        }

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

        public Position currentPosition
        {
            get
            {
                if (currentPositionID.HasValue)
                    return daoFactory.GetPositionDao().GetById(currentPositionID.Value, false);
                else
                    return null;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DepartmentList = new List<CAESDO.Recruitment.Core.Domain.Unit>();
            }

            if (currentPositionID.HasValue)
                DataBindExistingPosition();
        }

        protected void btnModifyPosition_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            Position newPosition = new Position();

            if (currentPositionID.HasValue)
                newPosition = currentPosition;
            
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

                    jobDescription = daoFactory.GetFileDao().SaveOrUpdate(jobDescription);

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
            {
                using (new NHibernateTransaction())
                {
                    daoFactory.GetPositionDao().SaveOrUpdate(newPosition);
                }
            }
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

        private void DataBindExistingPosition()
        {
            //Make sure that we have a valid object
            try
            {
                currentPosition.IsTransient();
            }
            catch (NHibernate.ObjectNotFoundException)
            {
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));
            }

            //If we do, databind all of the fields on the form
            //Set the posted date to now
            txtDeadline.Text = currentPosition.Deadline.ToShortDateString();

            txtPositionTitle.Text = currentPosition.PositionTitle;
            txtPositionNumber.Text = currentPosition.PositionNumber;

            txtHRRep.Text = currentPosition.HRRep;
            txtHRPhone.Text = currentPosition.HRPhone;
            txtHREmail.Text = currentPosition.HREmail;

            DepartmentList = new List<CAESDO.Recruitment.Core.Domain.Unit>();
            
            foreach (Department d in currentPosition.Departments)
            {
                CAESDO.Recruitment.Core.Domain.Unit u = daoFactory.GetUnitDao().GetById(d.DepartmentFIS, false);

                DepartmentList.Add(u);
            }

            repDepartments.DataSource = DepartmentList;
            repDepartments.DataBind();
            filePositionDescription.Visible = false;
            reqValPositionDescription.Visible = false;

            txtShortDescription.Text = currentPosition.ShortDescription;

            if (currentPosition.ReferenceTemplate != null)
                ftxtReferenceTemplate.Text = currentPosition.ReferenceTemplate.TemplateText;

            txtPublications.Text = currentPosition.NumPublications.ToString();
            txtReferences.Text = currentPosition.NumReferences.ToString();

            chkAllowApplications.Checked = currentPosition.AllowApps;

            //TODO: All logic for download of job description and replacement
            lbtnDownloadPositionDescription.Visible = true;
            litDownloadPositionDescription.Visible = true;

            //Change the text of the position status literal and then submit button to represent an edit
            litPositionState.Text = "Edit Position";
            btnModifyPosition.Text = "Update!";

        }

        /// <summary>
        /// First removed all existing departments from 
        /// </summary>
        /// <param name="p"></param>
        private void addDepartmentsToPosition(Position p)
        {
            if (p.Departments == null)
                p.Departments = new List<Department>();
            else
                p.Departments.Clear();
                        
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