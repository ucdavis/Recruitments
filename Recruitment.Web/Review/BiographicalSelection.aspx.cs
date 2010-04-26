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

namespace CAESDO.Recruitment.Web
{
    public partial class Review_BiographicalSelection : ApplicationPage
    {
        public string BiographicalPage
        {
            get
            {
                return Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) + HttpContext.Current.Request.ApplicationPath + "/Review/BiographicalReport.aspx?PositionID=" + dlistPositions.SelectedValue;
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
            string strResult;

            WebResponse response = null;

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Headers.Set("Cookie", Request.Headers["Cookie"]);
            req.Headers.Add("Cache-Control", "no-cache");
            req.Headers.Add("Pragma", "no-cache");

            req.Timeout = 1000; //One second timeout
            req.KeepAlive = false;

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
                response.Close();
            }

            Response.Clear();

            if (output == BioOutputType.Excel)
            {
                //int tableIndex = strResult.IndexOf("<table");
                //strResult = strResult.Substring(tableIndex, strResult.IndexOf("</table>") - tableIndex + "</table>".Length);
                //Control the name that they see
                Response.ContentType = "application/ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "Biographical.xls");
                Response.AddHeader("Content-Length", strResult.Length.ToString());
                //Response.TransmitFile(file.FullName);
            }

            Response.Write(strResult);
            Response.End();
        }
    }

    public enum BioOutputType
    {
        Excel,
        Screen
    }
}