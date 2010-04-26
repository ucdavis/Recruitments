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
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;

namespace CAESDO.Recruitment.Web
{
    public partial class Authorized_Reports : ApplicationPage
    {
        private const string STR_Interim = "Interim";
        private const string STR_Survey = "Survey";

        public string InterimPage
        {
            get
            {
                return Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + HttpContext.Current.Request.ApplicationPath + "/Authorized/InterimReport.aspx?PositionID=" + dlistPositions.SelectedValue;
            }
        }

        public string SurveyPage
        {
            get
            {
                return Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + HttpContext.Current.Request.ApplicationPath + "/Authorized/RecruitmentSources.aspx?PositionID=" + dlistPositions.SelectedValue;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (dlistType.SelectedValue == STR_Interim)
            {
                if ( chkOutputFile.Checked )
                    this.OutputPage(InterimPage, ReportOutputType.Word);
                else
                    this.OutputPage(InterimPage, ReportOutputType.Screen);
            }
            else if (dlistType.SelectedValue == STR_Survey)
            {
                if ( chkOutputFile.Checked )
                    this.OutputPage(SurveyPage, ReportOutputType.Word);
                else
                    this.OutputPage(SurveyPage, ReportOutputType.Screen);
            }
        }

        private void OutputPage(string URL, ReportOutputType output)
        {
            StringDictionary parameters = new StringDictionary();
            parameters.Add("PositionID", dlistPositions.SelectedValue);

            Control reportPleaseWork;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            Html32TextWriter hw = new Html32TextWriter(sw);
            
            try
            {
                reportPleaseWork = LoadControl("RecruitmentSources.ascx");
                ((IReportUserControl)reportPleaseWork).LoadReport(parameters); //Load the report with parameters
                
                reportPleaseWork.RenderControl(hw);
            }
            catch (Exception ex)
            {
                lblReportStatus.Text = ex.Message;
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

            Response.Write(sb.ToString());

            Response.End();
        }
    }

    public enum ReportOutputType
    {
        Word, 
        Screen
    }
}