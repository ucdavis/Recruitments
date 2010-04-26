using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Principal;
using System.IO;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.Data;
using CAESDO.Recruitment.Core.DataInterfaces;


/// <summary>
/// Summary description for RecruitmentUploadService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RecruitmentUploadService : System.Web.Services.WebService
{
    public string FilePath
    {
        get
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings["RecruitmentFilePath"];
        }
    }

    public IDaoFactory daoFactory
    {
        get { return new NHibernateDaoFactory(); }
    }

    public RecruitmentUploadService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld(string s)
    {
        return "Hello VSTO" + s;
    }

    [WebMethod]
    public string WhoAmI()
    {
        WindowsIdentity win = (WindowsIdentity)User.Identity;
     
        User user = daoFactory.GetUserDao().GetUserBySID(win.User.ToString());

        if (user == null)
            return win.Owner.Value;
        else
            return string.Format("Username {0}, with AuthType {1}", user.LastName, win.AuthenticationType);
    }

    [WebMethod]
    public string ReadFileBytes(byte[] file)
    {
        return file.Length.ToString();
    }

    [WebMethod]
    public void SaveFile(byte[] file)
    {
        System.IO.File.WriteAllBytes(FilePath + "tester.pdf", file);
    }
}

