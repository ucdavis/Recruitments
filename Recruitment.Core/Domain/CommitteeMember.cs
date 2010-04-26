using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class CommitteeMember : DomainObject<int>
    {
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private MemberType _MemberType;

        public MemberType MemberType
        {
            get { return _MemberType; }
            set { _MemberType = value; }
        }

        private Position _AssociatedPosition;

        public Position AssociatedPosition
        {
            get { return _AssociatedPosition; }
            set { _AssociatedPosition = value; }
        }

        public CommitteeMember()
        {

        }
	
    }
}
