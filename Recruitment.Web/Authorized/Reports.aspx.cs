using System;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Collections.Specialized;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_Reports : ApplicationPage
    {
        private const string STR_Interim = "Interim";
        private const string STR_Survey = "Survey";
        private const string STR_PositionID = "PositionID";
        private const string STR_Bio = "Bio";

        public string InterimPage
        {
            get { return "InterimReport.ascx"; }
        }

        public string SurveyPage
        {
            get { return "RecruitmentSources.ascx"; }
        }

        public string BioPage
        {
            get { return "../Shared/BiographicalReport.ascx"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (dlistType.SelectedValue == STR_Interim)
            {
                this.OutputPage(InterimPage, chkOutputFile.Checked ? ReportOutputType.Word : ReportOutputType.Screen);
            }
            else if (dlistType.SelectedValue == STR_Bio)
            {
                this.OutputPage(BioPage, chkOutputFile.Checked ? ReportOutputType.Excel : ReportOutputType.Screen);
            }
            else if (dlistType.SelectedValue == STR_Survey)
            {
                this.OutputPage(SurveyPage, chkOutputFile.Checked ? ReportOutputType.Word : ReportOutputType.Screen);
            }
        }

        private void OutputPage(string URL, ReportOutputType output)
        {
            StringDictionary parameters = new StringDictionary();
            parameters.Add(STR_PositionID, dlistPositions.SelectedValue);

            Control report;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            Html32TextWriter hw = new Html32TextWriter(sw);
            
            try
            {
                report = LoadControl(URL);
                ((IReportUserControl)report).LoadReport(parameters); //Load the report with parameters
                
                report.RenderControl(hw);
            }
            catch (Exception ex)
            {
                lblReportStatus.Text = "Report Could Not Be Generated.  Please Try Again"; //Most likely timeout -- try again 
                return;
            }

            Response.Clear();

            if (output == ReportOutputType.Word)
            {
                //Control the name that they see
                Response.ContentType = "application/ms-word";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "file.doc");
                Response.AddHeader("Content-Length", sb.Length.ToString());
                //Response.TransmitFile(file.FullName);
            }
            else if (output == ReportOutputType.Excel)
            {
                //Control the name that they see
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "Biographical.xls");
                Response.AddHeader("Content-Length", sb.Length.ToString());                   
            }

            Response.Write(sb.ToString());

            Response.End();
        }
    }

    public enum ReportOutputType
    {
        Word, 
        Excel,
        Screen
    }
}