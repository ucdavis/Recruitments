using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class DepartmentMember : DomainObject<DepartmentMember, int>
    {        
        private string _DepartmentFIS;

        [NotNullValidator]
        [StringLengthValidator(4)]
        public virtual string DepartmentFIS
        {
            get { return _DepartmentFIS; }
            set { _DepartmentFIS = value; }
        }

        private Unit _Unit;

        public virtual Unit Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

        private string _OtherDepartmentName;

        [IgnoreNulls]
        [StringLengthValidator(50)]
        public virtual string OtherDepartmentName
        {
            get { return _OtherDepartmentName; }
            set { _OtherDepartmentName = value; }
        }
                        
        private string _LoginID;

        [NotNullValidator]
        [StringLengthValidator(50)]
        public virtual string LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }

        private string _FirstName;

        [IgnoreNulls]
        [StringLengthValidator(50)]
        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;

        [IgnoreNulls]
        [StringLengthValidator(50)]
        public virtual string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private bool _Inactive;

        [NotNullValidator]
        public virtual bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }

        public DepartmentMember()
        {

        }

        /// <summary>
        /// Two department members are equal if their loginIDs and Departments match
        /// </summary>
        public override bool Equals(object obj)
        {
            DepartmentMember member = (DepartmentMember)obj;
            
            bool LoginMatch = this.LoginID == member.LoginID;
            bool DepartmentMatch = this.DepartmentFIS == member.DepartmentFIS;
            //bool MemberTypeMatch = this.MemberType.Type == member.MemberType.Type;

            return LoginMatch && DepartmentMatch;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
