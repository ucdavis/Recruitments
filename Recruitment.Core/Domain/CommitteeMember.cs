using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class CommitteeMember : DomainObject<int>
    {
        private int _UserID;

        [NotNullValidator()]
        public virtual int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _Email;

        [StringLengthValidator(100)]
        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private MemberType _MemberType;

        [NotNullValidator]
        public virtual MemberType MemberType
        {
            get { return _MemberType; }
            set { _MemberType = value; }
        }

        private Position _AssociatedPosition;

        [NotNullValidator]
        public virtual Position AssociatedPosition
        {
            get { return _AssociatedPosition; }
            set { _AssociatedPosition = value; }
        }

        public CommitteeMember()
        {

        }
	
    }
}
