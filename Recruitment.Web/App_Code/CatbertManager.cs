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

namespace CAESDO.Recruitment
{
    /// <summary>
    /// Summary description for CatbertManager
    /// </summary>
    public class CatbertManager
    {
        static readonly string HASH = WebConfigurationManager.AppSettings["CatbertHash"];
        static readonly string AppName = WebConfigurationManager.AppSettings["AppName"];

        static CatOps.CatOps catops = new CatOps.CatOps();

        public static CatOps.Units[] GetUnits()
        {
            return catops.GetUnits(HASH);
        }

        public static CatOps.Users[] SearchNewUsersByLogin(string login)
        {
            return catops.SearchNewUser(null, null, null, login, HASH);
        }

        public static void AddUserToRole(CatOps.Roles role, CatOps.Users user)
        {
            catops.AssignPermissions(user.Login, AppName, role.RoleID, HASH);
        }

        public static void AddUserToRole(int roleID, string login)
        {
            catops.AssignPermissions(login, AppName, roleID, HASH);
        }

        public static bool RemoveUserFromRole(int roleID, string login)
        {
            return catops.DeletePermissions(login, AppName, roleID, HASH);
        }

        //public static CatOps.Users[] GetUsersInApplication()
        //{
        //    return catops.GetUsersByApplications(AppName, HASH);
        //}

        public CatbertManager()
        {
            CatOps.Users u = new CatOps.Users();
            
            //
            // TODO: Add constructor logic here
            //
        }
    }
}