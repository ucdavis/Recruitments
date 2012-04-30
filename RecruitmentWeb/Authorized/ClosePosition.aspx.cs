using System;
using System.Web.Security;
using CAESDO.Recruitment.BLL;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Web;

public partial class Authorized_ClosePosition : System.Web.UI.Page
{
    public int? CurrentPositionID
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

    public Position CurrentPosition
    {
        get
        {
            return PositionBLL.GetByID(CurrentPositionID.Value);
        }
    }
    
    public FileType FinalRecruitmentReportFileType
    {
        get
        {
            return FileTypeBLL.GetByName("FinalRecruitmentReport");
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        CheckPositionAccess();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Close the position and upload the final recruitment report
    /// </summary>
    protected void ClosePosition(object sender, EventArgs e)
    {
        Position position = CurrentPosition;

        File finalRecruitmentReport = FileBLL.SavePDF(fileFinalRecruitmentReport, FinalRecruitmentReportFileType);

        if (finalRecruitmentReport != null)
        {
            //Delete the old file
            if (position.FinalRecruitmentReportFile != null) FileBLL.DeletePDF(position.FinalRecruitmentReportFile);

            //Save the new reference
            using (var ts = new TransactionScope())
            {
                position.FinalRecruitmentReportFile = finalRecruitmentReport;
                position.Closed = true;

                PositionBLL.EnsurePersistent(position);

                ts.CommitTransaction();
            }

            Response.Redirect("ClosePositionSuccess.aspx");
        }
        else
        {
            lblFileError.Text = "Uploaded File Must Be In PDF Format";
        }
    }

    private void CheckPositionAccess()
    {
        if (!CurrentPositionID.HasValue)
            Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.QUERY));

        //Check to ensure the position exists and the current user has access
        if (Roles.IsUserInRole("Admin") == false && PositionBLL.VerifyPositionAccess(CurrentPosition) == false)
            Response.Redirect(RecruitmentConfiguration.ErrorPage(RecruitmentConfiguration.ErrorType.AUTH));
   
    }
}
