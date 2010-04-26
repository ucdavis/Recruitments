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
            string strResult;
            
            WebResponse response = null;

            //HttpCookie authCookie = Request.Cookies["FormsAuthDB.AspxAuth"];
            //HttpCookie sessionCookie = Request.Cookies["ASP.NET_SessionId"];
            //HttpCookie userCookie = Request.Cookies["AuthUser"];
            //HttpCookie rolesCookie = Request.Cookies[".ASPXROLES"];

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Headers.Set("Cookie", Request.Headers["Cookie"]);

            req.Timeout = 1000; //One second timeout

            try
            {
                response = req.GetResponse();
            }
            catch (Exception ex)
            {
                lblReportStatus.Text = "Report Could Not Be Generated.  Please Try Again"; //Most likely timeout -- try again                
                return;
            }

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strResult = reader.ReadToEnd();
                reader.Close();
            }

            Response.Clear();

            if ( output == ReportOutputType.Word )
            {                
                //Control the name that they see
                Response.ContentType = "application/ms-word";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "file.doc");
                Response.AddHeader("Content-Length", strResult.Length.ToString());
                //Response.TransmitFile(file.FullName);
            }

            Response.Write(strResult);
            Response.End();
        }
    }

    public enum ReportOutputType
    {
        Word, 
        Screen
    }
}