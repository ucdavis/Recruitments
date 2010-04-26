using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class CommitteeMember : DomainObject<int>
    {
        private MemberType _MemberType;

        [NotNullValidator]
        public virtual MemberType MemberType
        {
            get { return _MemberType; }
            set { _MemberType = value; }
        }

        private DepartmentMember _DepartmentMember;

        [NotNullValidator]
        public virtual DepartmentMember DepartmentMember
        {
            get { return _DepartmentMember; }
            set { _DepartmentMember = value; }
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

        public override bool Equals(object obj)
        {
            CommitteeMember member = (CommitteeMember)obj;

            bool MemberMatch = this.DepartmentMember == member.DepartmentMember;
            bool PositionMatch = this.AssociatedPosition == member.AssociatedPosition;
            bool MemberTypeMatch = this.MemberType.Type == member.MemberType.Type;

            return MemberMatch && PositionMatch && MemberTypeMatch;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
