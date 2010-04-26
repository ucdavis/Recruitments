using System;
using System.Collections.Generic;
using System.Text;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.BLL
{
    public class UnitBLL : GenericBLL<Unit, string>
    {

    }

    public class UserBLL : GenericBLL<User, int>
    {
        public static User GetByLogin(string login)
        {
            return daoFactory.GetUserDao().GetUserByLogin(login);
        }

        public static User GetCurrent()
        {
            return UserBLL.GetByLogin(System.Web.HttpContext.Current.User.Identity.Name);
        }
    }
}
