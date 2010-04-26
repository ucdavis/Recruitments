using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class FacultyReviewGroup : IGroup
    {
        #region IGroup Members

        public virtual int PositionID
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public virtual List<User> Members
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public virtual bool isUserInGroup(System.Security.Principal.IPrincipal user)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

    }
}
