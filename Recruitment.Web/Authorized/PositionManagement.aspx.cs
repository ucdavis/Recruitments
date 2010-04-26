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
using System.Web.Configuration;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment.Web
{
    public partial class PositionManagement : ApplicationPage
    {
        private const string STR_DepartmentList = "DepartmentList";

        public string PendingPageURL
        {
            get
            {
                return Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + HttpContext.Current.Request.ApplicationPath + "/Authorized/ViewPositionsPending.aspx";
            }
        }

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

        public Position currentPosition
        {
            get
            {
                if (currentPositionID.HasValue)
                    return PositionBLL.GetByID(currentPositionID.Value);
                else
                    return null;
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
                return FileTypeBLL.GetByName("JobDescription");
            }
        }

        public TemplateType ReferenceTemplateType
        {
            get
            {
                return TemplateTypeBLL.GetByName("Reference");
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

            addStepsToPosition(newPosition);

            newPosition.ShortDescription = txtShortDescription.Text;

            if (newPosition.ReferenceTemplate == null)
                newPosition.ReferenceTemplate = new Template();

            newPosition.ReferenceTemplate.TemplateType = ReferenceTemplateType;
            newPosition.ReferenceTemplate.TemplateText = ftxtReferenceTemplate.Text;

            newPosition.NumPublications = int.Parse(txtPublications.Text);
            newPosition.NumReferences = int.Parse(txtReferences.Text);

            newPosition.AllowApps = chkAllowApplications.Checked;
            newPosition.FacultyView = chkAllowFaculty.Checked;
            newPosition.Closed = chkPositionClosed.Checked;

            // Only try modifying the position descriptions if an upload file exists (should be a new position only).
            if (filePositionDescription.HasFile)
            {
                File jobDescriptionFile = null;

                using (new TransactionScope())
                {
                    jobDescriptionFile = FileBLL.SavePDF(filePositionDescription, JobDescriptionFileType);
                }

                if (jobDescriptionFile == null)
                {
                    //Error message: Job Description Must Be a PDF File
                    lblInvalidFileType.Text = " *Job Description Must Be a PDF File";
                    return;
                }
                else
                {
                    newPosition.DescriptionFile = jobDescriptionFile;
                }
            }

            if (newPosition.IsTransient())
            {
                //Since the position is new, send an email to the AppMailTo about the new pending position
                PositionBLL.SendNotificationEmail(newPosition, PendingPageURL);
            }

            using (new TransactionScope())
            {
                PositionBLL.EnsurePersistent(ref newPosition);
            }

            //Redirect to the position modified page
            Response.Redirect("PositionModified.aspx");
        }

        protected void lbtnAddDepartment_Click(object sender, EventArgs e)
        {
            CAESDO.Recruitment.Core.Domain.Unit selectedUnit = UnitBLL.GetByID(dlistDepartment.SelectedValue);

            Department associatedDepartment = new Department();
            associatedDepartment.AssociatedPosition = currentPosition;
            associatedDepartment.DepartmentFIS = selectedUnit.FISCode;
            associatedDepartment.Unit = selectedUnit;
            associatedDepartment.PrimaryDept = DepartmentList.Count == 0;

            if ( DepartmentList.Contains(associatedDepartment) == false )
                DepartmentList.Add(associatedDepartment);

            gviewDepartments.DataSource = DepartmentList;
            gviewDepartments.DataBind();
        }

        protected void btnPositionDescriptionReplace_Click(object sender, EventArgs e)
        {
            Position position = currentPosition;

            File jobDescription = FileBLL.SavePDF(filePositionDescriptionReplace, JobDescriptionFileType);

            if (jobDescription != null)
            {
                //Delete the old file
                FileBLL.DeletePDF(position.DescriptionFile);

                //Save the new reference
                using (new TransactionScope())
                {
                    position.DescriptionFile = jobDescription;

                    PositionBLL.EnsurePersistent(ref position);
                }
            }
            else
            {
                ///TODO: Error Message
            }
        }
        
        protected void lbtnDownloadPositionDescription_Click(object sender, EventArgs e)
        {
            FileBLL.Transmit(currentPosition.DescriptionFile.ID, currentPosition.DescriptionFile.FileName);
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
            //current position should not be null
            if ( currentPosition == null )
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.UNKNOWN));

            if (Roles.IsUserInRole("Admin") == false && daoFactory.GetPositionDao().VerifyPositionAccess(currentPosition) == false)
                Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));

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

            if (currentPosition.Steps.Contains(ApplicationStepType.CurrentPosition))
                chkShowCurrentPosition.Checked = true;

            if (currentPosition.Steps.Contains(ApplicationStepType.Education))
                chkShowEducation.Checked = true;

            filePositionDescription.Visible = false;
            reqValPositionDescription.Visible = false;

            txtShortDescription.Text = currentPosition.ShortDescription;

            if (currentPosition.ReferenceTemplate != null)
                ftxtReferenceTemplate.Text = currentPosition.ReferenceTemplate.TemplateText;

            txtPublications.Text = currentPosition.NumPublications.ToString();
            txtReferences.Text = currentPosition.NumReferences.ToString();

            chkAllowApplications.Checked = currentPosition.AllowApps;
            chkAllowFaculty.Checked = currentPosition.FacultyView;
            chkPositionClosed.Checked = currentPosition.Closed;

            lbtnDownloadPositionDescription.Visible = true;
            litDownloadPositionDescription.Visible = true;
            ibtnReplacePositionDescription.Visible = true;

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
                        int fileTypeID = (int)gviewFileTypes.DataKeys[row.RowIndex]["id"];
                        p.FileTypes.Add(FileTypeBLL.GetByID(fileTypeID));
                    }
                }
            }
        }

        /// <summary>
        /// Adds the major steps to the given position
        /// </summary>
        private void addStepsToPosition(Position p)
        {
            if (p.Steps == null)
                p.Steps = new List<ApplicationStepType>();
            else
                p.Steps.Clear();

            if (chkShowEducation.Checked)
                p.Steps.Add(ApplicationStepType.Education);

            if (chkShowCurrentPosition.Checked)
                p.Steps.Add(ApplicationStepType.CurrentPosition);
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
            return UtilsBLL.BreakCamelCase(CamelString);
        }
    }
}