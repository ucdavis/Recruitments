using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.ComponentModel;
using System.Net;
using CAESDO.Recruitment.Core.Domain;
using CAESDO.Recruitment.BLL;

namespace CAESDO.Recruitment
{
    /// <summary>
    /// Summary description for CatbertManager
    /// </summary>
    [DataObject]
    public class CatbertManager
    {
        static readonly string HASH = WebConfigurationManager.AppSettings["CatbertHash"];
        static readonly string AppName = WebConfigurationManager.AppSettings["AppName"];

        public static CatOps.CatbertWebService catops = new CatOps.CatbertWebService();

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Units[] GetUnits()
        {
            SetSecurityContext();
            
            return catops.GetUnits(HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Roles[] GetRoles()
        {
            SetSecurityContext();
            
            return catops.GetRoles(AppName, HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Users[] SearchNewUsersByLogin(string EmployeeID, string FirstName, string LastName, string LoginID)
        {
            SetSecurityContext();

            return catops.SearchNewUser(EmployeeID, FirstName, LastName, LoginID, HASH);
        }

        public static CatOps.Users[] SearchCampusUser(string loginID)
        {
            SetSecurityContext();

            return catops.SearchNewUser(null, null, null, loginID, HASH);
            //return catops.SearchCampusNewUser(loginID, HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Users[] SearchNewUsersByLogin(string login)
        {
            SetSecurityContext();

            return catops.SearchNewUser(null, null, null, login, HASH);
        }

        public static bool AddUserToRole(CatOps.Users user, CatOps.Roles role)
        {
            SetSecurityContext();

            return catops.AssignPermissions(user.Login, AppName, role.RoleID, HASH);
        }

        public static bool AddUserToRole(string login, int roleID)
        {
            SetSecurityContext();

            return catops.AssignPermissions(login, AppName, roleID, HASH);
        }

        public static bool RemoveUserFromRole(int roleID, string login)
        {
            SetSecurityContext();

            return catops.DeletePermissions(login, AppName, roleID, HASH);
        }

        public static bool AddUserToUnit(string login, int UnitID)
        {
            SetSecurityContext();

            return catops.AddUnit(login, UnitID, HASH);
        }

        public static bool RemoveUserFromUnit(string login, int UnitID)
        {
            SetSecurityContext();

            return catops.DeleteUnit(login, UnitID, HASH);
        }

        public static CatOps.Roles[] GetRolesByUser(string login)
        {
            SetSecurityContext();

            return catops.GetRolesByUser(AppName, login, HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.CatbertUsers[] GetUsersInApplication()
        {
            SetSecurityContext();

            return catops.GetUsersByApplications(AppName, HASH);
        }

        public static int InsertNewUser(string login)
        {
            SetSecurityContext();

            CatOps.Users[] newUsers = CatbertManager.SearchNewUsersByLogin(login);
            if ( newUsers.Length != 1 )
                return -1;

            return catops.InsertNewUser(newUsers[0], HASH);
        }

        public static int InsertNewUser(CatOps.Users user)
        {
            SetSecurityContext();

            return catops.InsertNewUser(user, HASH);
        }

        public static bool VerifyUser(string login)
        {
            SetSecurityContext();

            return catops.VerifyUser(login, HASH);
        }

        public CatbertManager()
        {

        }

        public static void SetSecurityContext()
        {
            CatOps.SecurityContext sc = new CatOps.SecurityContext();

            string username = HttpContext.Current.User.Identity.Name;

            User user = UserBLL.GetCurrent();

            sc.userID = username;
            sc.password = user.UserKey.ToString();

            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            catops.SecurityContextValue = sc;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SetCabertSecurityContextAttribute : Attribute
    {
        public SetCabertSecurityContextAttribute(CatOps.CatbertWebService service)
        {
            CatOps.SecurityContext sc = new CatOps.SecurityContext();

            sc.userID = HttpContext.Current.User.Identity.Name;
            sc.password = "02188896-cb85-41a4-a2a3-060aeec2975b";

            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

            service.SecurityContextValue = sc;
        }
    }

    // Accept all certificates even self signed
    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        { }

        public bool CheckValidationResult(ServicePoint sp,
         System.Security.Cryptography.X509Certificates.X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
    }

}