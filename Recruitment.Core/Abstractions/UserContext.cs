using System;
using System.Web;
using System.Security.Principal;

namespace CAESDO.Recruitment.Core.Abstractions
{
    /*
    public class UserContext : IUserContext
    {
        public bool IsUserInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public string Name()
        {
            return HttpContext.Current.User.Identity.Name;
        }
    }
     */

    public class UserContext : IPrincipal
    {
        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public IIdentity Identity
        {
            get { return HttpContext.Current.User.Identity; }
        }
    }
}