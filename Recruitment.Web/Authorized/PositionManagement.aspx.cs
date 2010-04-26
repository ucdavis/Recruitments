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

        public IList<Department> DepartmentList
        {
            get
            {
                if (Session[STR_DepartmentList] == null)
                    return new List<Department>();
                else
                    return Session[STR_DepartmentList] as IList<Department>;
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

        public TemplateType ReferenceTemplateType
        {
            get
            {
                TemplateType _ReferenceTemplateType = new TemplateType();
                _ReferenceTemplateType.Type = "Reference";

                return daoFactory.GetTemplateTypeDao().GetUniqueByExample(_ReferenceTemplateType);
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
                DepartmentList = new List<Department>();
                
                if (currentPositionID.HasValue)
                    DataBindExistingPosition();
            }
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

            if (!updatePrimaryDepartmentStatus())
            {
                lblPrimaryDeptErrorMessage.Text = "You must select exactly one primary department for this position";
                return;
            }            
                
            addDepartmentsToPosition(newPosition);

            addFileTypesToPosition(newPosition);

            newPosition.ShortDescription = txtShortDescription.Text;

            if (newPosition.ReferenceTemplate == null)
                newPosition.ReferenceTemplate = new Template();

            newPosition.ReferenceTemplate.TemplateType = ReferenceTemplateType;
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

            Department associatedDepartment = new Department();
            associatedDepartment.AssociatedPosition = currentPosition;
            associatedDepartment.DepartmentFIS = selectedUnit.FISCode;
            associatedDepartment.Unit = selectedUnit;

            if ( DepartmentList.Contains(associatedDepartment) == false )
                DepartmentList.Add(associatedDepartment);

            gviewDepartments.DataSource = DepartmentList;
            gviewDepartments.DataBind();
        }
        
        protected void cboxPrimary_CheckedChanged(object sender, EventArgs e)
        {
            if (!updatePrimaryDepartmentStatus())
            {
                lblPrimaryDeptErrorMessage.Text = "You must select exactly one primary department for this position";
            }
        }

        protected void gviewDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Grab the department FIS code from the datakey
            Department dept = new Department();
            dept.DepartmentFIS = (string)gviewDepartments.DataKeys[e.RowIndex]["DepartmentFIS"];
            
            //Remove the corresponding department from the DepartmentList
            DepartmentList.Remove(dept);

            gviewDepartments.DataSource = DepartmentList;
            gviewDepartments.DataBind();

            e.Cancel = true;
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

            DepartmentList = new List<Department>();
            
            foreach (Department d in currentPosition.Departments)
            {
                DepartmentList.Add(d);
            }

            gviewDepartments.DataSource = DepartmentList;
            gviewDepartments.DataBind();

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
        /// Updates the DepartmentList to have the proper Primary department status
        /// </summary>
        /// <returns>False if the number of primary departments checks does not exactly equal 1</returns>
        /// <remarks>Acts on gviewDepartments</remarks>
        private bool updatePrimaryDepartmentStatus()
        {
            int numPrimaryDepartmentsChecked = 0;
            string primaryDepartmentFIS = string.Empty;

            foreach (GridViewRow row in gviewDepartments.Rows)
            {
                //For each datarow, find the checkbox
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cbox = row.FindControl("cboxPrimary") as CheckBox;

                    if (cbox.Checked)
                    {
                        //If the checkbox is checked, increment the numPrimaryDepartmentsChecked variable and record the FIS code
                        numPrimaryDepartmentsChecked++;

                        primaryDepartmentFIS = gviewDepartments.DataKeys[row.RowIndex]["DepartmentFIS"] as string;
                    }
                }
            }

            //Now if numPrimaryDepartmentsChecked != 1, return false, else update the DepartmentList
            if (numPrimaryDepartmentsChecked != 1)
                return false;
            else
            {
                //Update the department list with the chosen department
                Department chosenDepartment = new Department();
                chosenDepartment.DepartmentFIS = primaryDepartmentFIS;

                foreach (Department d in DepartmentList)
                {
                    if (d.DepartmentFIS == chosenDepartment.DepartmentFIS)
                        d.PrimaryDept = true;
                    else
                        d.PrimaryDept = false;
                }

                return true;
            }
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
                        
            foreach (Department d in DepartmentList)
            {
                //Foreach unit, add it to the department list                
                Department dept = new Department();
                dept.AssociatedPosition = p;    //Associate the departments with the given positiopns
                dept.DepartmentFIS = d.DepartmentFIS;
                dept.PrimaryDept = d.PrimaryDept;

                p.Departments.Add(dept);
            }
        }

        /// <summary>
        /// Loops through the gviewFileTypes grid and adds any checked fileTypes to the fileType list
        /// </summary>
        private void addFileTypesToPosition(Position p)
        {
            if (p.FileTypes == null)
                p.FileTypes = new List<FileType>();
            else
                p.FileTypes.Clear();

            foreach (GridViewRow row in gviewFileTypes.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //If this datarow is checked, add it to the position's fileTypes
                    CheckBox cbox = (CheckBox)row.FindControl("chkFileType");

                    if (cbox.Checked)
                    {
                        p.FileTypes.Add(daoFactory.GetFileTypeDao().GetById((int)gviewFileTypes.DataKeys[row.RowIndex]["id"], false));
                    }
                }
            }
        }

        /// <summary>
        /// Determines if the given FileTypeName is already in the current position.
        /// If this is a new position, always return false
        /// </summary>
        /// <param name="FileTypeName">The file type</param>
        protected bool doesFileTypeExistInPosition(string FileTypeName)
        {
            if (currentPositionID.HasValue == false)
                return false;

            FileType type = new FileType();
            type.FileTypeName = FileTypeName;

            return currentPosition.FileTypes.Contains(type);
        }

        /// <summary>
        /// helper method to convert CamelCaseString to Camel Case String
        /// by inserting spaces
        /// </summary>
        /// <see cref="http://www.developer.com/net/asp/article.php/3609991"/>
        protected string BreakCamelCase(string CamelString)
        {
            string output = string.Empty;
            bool SpaceAdded = true;

            for (int i = 0; i < CamelString.Length; i++)
            {
                if (CamelString.Substring(i, 1) ==
                    CamelString.Substring(i, 1).ToLower())
                {
                    output += CamelString.Substring(i, 1);
                    SpaceAdded = false;
                }
                else
                {
                    if (!SpaceAdded)
                    {
                        output += " ";
                        output += CamelString.Substring(i, 1);
                        SpaceAdded = true;
                    }
                    else
                        output += CamelString.Substring(i, 1);
                }
            }
            return output;
        }
}
}