using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public interface IGroup
    {
        int PositionID
        {
            get;
            set;
        }

        List<User> Members
        {
            get;
            set;
        }

        bool isUserInGroup(System.Security.Principal.IPrincipal user);
    }
}
