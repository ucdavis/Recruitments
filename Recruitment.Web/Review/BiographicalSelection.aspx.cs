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
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text;

namespace CAESDO.Recruitment.Web
{
    public partial class Review_BiographicalSelection : ApplicationPage
    {
        private const string STR_PositionID = "PositionID";
        public string BiographicalPage
        {
            get
            {
                return "BiographicalReport.ascx";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDisplayReport_Click(object sender, EventArgs e)
        {
            if (chkOutputToFile.Checked)
                this.OutputPage(BiographicalPage, BioOutputType.Excel);
            else
                this.OutputPage(BiographicalPage, BioOutputType.Screen);
        }

        private void OutputPage(string URL, BioOutputType output)
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
                lblReportStatus.Text = "Report Could Not Be Generated.  Please Try Again";
                return;
            }


            Response.Clear();

            if (output == BioOutputType.Excel)
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

    public enum BioOutputType
    {
        Excel,
        Screen
    }
}