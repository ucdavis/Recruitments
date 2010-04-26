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
using System.Text;
using System.IO;
using System.Web.Configuration;
using System.Net;

public partial class Authorized_AdminIndex : System.Web.UI.Page
{
    public string FilePath
    {
        get
        {
            return WebConfigurationManager.AppSettings["RecruitmentFilePath"];
        }
    }

    public string SimplePath
    {
        get
        {
            return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Authorized/InterimReport.aspx?PositionID=43";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(SimplePath);

        for (int i = 0; i < Request.Cookies.Count; i++)
        {
            Response.Write(Request.Cookies[i].Path);
        }
    }

    protected void btnDownloadPage_Click(object sender, EventArgs e)
    {
        string strResult;
        WebResponse response;

        HttpCookie authCookie = Request.Cookies["FormsAuthDB.AspxAuth"];
        HttpCookie sessionCookie = Request.Cookies["ASP.NET_SessionId"];
        HttpCookie userCookie = Request.Cookies["AuthUser"];
        HttpCookie rolesCookie = Request.Cookies[".ASPXROLES"];
                
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(SimplePath);
        req.Headers.Set("Cookie", Request.Headers["Cookie"]);

        //req.CookieContainer = new CookieContainer();
        //req.CookieContainer.Add(new Cookie(authCookie.Name, authCookie.Value, authCookie.Path, authCookie.Name));
        //req.CookieContainer.Add(new Cookie(sessionCookie.Name, sessionCookie.Value, sessionCookie.Path, sessionCookie.Name));
        //req.CookieContainer.Add(new Cookie(userCookie.Name, userCookie.Value, userCookie.Path, userCookie.Name));
        //req.CookieContainer.Add(new Cookie(userCookie.Name, userCookie.Value, userCookie.Path, userCookie.Name));

        response = req.GetResponse();

        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            strResult = reader.ReadToEnd();
            reader.Close();
        }

        //Response.Write(strResult);

        //Response.Clear();

        //Control the name that they see
        //Response.ContentType = "application/ms-word";
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + "file.doc");
        //Response.AddHeader("Content-Length", strResult.Length.ToString());
        //Response.TransmitFile(file.FullName);
        Response.Write(strResult);
        //Response.End();

    }
}