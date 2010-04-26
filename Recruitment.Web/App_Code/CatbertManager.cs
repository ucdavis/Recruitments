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

        static CatOps.CatOps catops = new CatOps.CatOps();

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Units[] GetUnits()
        {
            return catops.GetUnits(HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Roles[] GetRoles()
        {
            return catops.GetRoles(AppName, HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Users[] SearchNewUsersByLogin(string EmployeeID, string FirstName, string LastName, string LoginID)
        {
            return catops.SearchNewUser(EmployeeID, FirstName, LastName, LoginID, HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.Users[] SearchNewUsersByLogin(string login)
        {
            return catops.SearchNewUser(null, null, null, login, HASH);
        }

        public static bool AddUserToRole(CatOps.Users user, CatOps.Roles role)
        {
            return catops.AssignPermissions(user.Login, AppName, role.RoleID, HASH);
        }

        public static bool AddUserToRole(string login, int roleID)
        {
            return catops.AssignPermissions(login, AppName, roleID, HASH);
        }

        public static bool RemoveUserFromRole(int roleID, string login)
        {
            return catops.DeletePermissions(login, AppName, roleID, HASH);
        }

        public static bool AddUserToUnit(string login, int UnitID)
        {
            return catops.AddUnit(login, UnitID, HASH);
        }

        public static bool RemoveUserFromUnit(string login, int UnitID)
        {
            return catops.DeleteUnit(login, UnitID, HASH);
        }

        public static CatOps.Roles[] GetRolesByUser(string login)
        {
            return catops.GetRolesByUser(AppName, login, HASH);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static CatOps.CatbertUsers[] GetUsersInApplication()
        {
            return catops.GetUsersByApplications(AppName, HASH);
        }

        public static int InsertNewUser(string login)
        {
            CatOps.Users[] newUsers = CatbertManager.SearchNewUsersByLogin(login);
            if ( newUsers.Length != 1 )
                return -1;

            return catops.InsertNewUser(newUsers[0], HASH);
        }

        public static int InsertNewUser(CatOps.Users user)
        {
            return catops.InsertNewUser(user, HASH);
        }

        public static bool VerifyUser(string login)
        {            
            return catops.VerifyUser(login, HASH);
        }

        public CatbertManager()
        {

        }
    }
}