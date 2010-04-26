using System.Web;

namespace CAESDO.Recruitment.Core.Abstractions
{
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
}