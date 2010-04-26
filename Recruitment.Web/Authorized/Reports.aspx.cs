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
                return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Authorized/InterimReport.aspx?PositionID=43";
            }
        }

        public string SurveyPage
        {
            get
            {
                return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Authorized/InterimReport.aspx?PositionID=43";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (dlistType.SelectedValue == STR_Interim)
            {
                this.RenderPageInWord(InterimPage);
            }
            else if (dlistType.SelectedValue == STR_Survey)
            {
                this.RenderPageInWord(SurveyPage);
            }
        }

        private void RenderPageInWord(string URL)
        {
            string strResult;
            
            WebResponse response;

            //HttpCookie authCookie = Request.Cookies["FormsAuthDB.AspxAuth"];
            //HttpCookie sessionCookie = Request.Cookies["ASP.NET_SessionId"];
            //HttpCookie userCookie = Request.Cookies["AuthUser"];
            //HttpCookie rolesCookie = Request.Cookies[".ASPXROLES"];

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(URL);
            req.Headers.Set("Cookie", Request.Headers["Cookie"]);
                        
            response = req.GetResponse();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                strResult = reader.ReadToEnd();
                reader.Close();
            }

            //Response.Write(strResult);

            Response.Clear();

            //Control the name that they see
            Response.ContentType = "application/ms-word";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + "file.doc");
            Response.AddHeader("Content-Length", strResult.Length.ToString());
            //Response.TransmitFile(file.FullName);
            Response.Write(strResult);
            Response.End();
        }
    }
}