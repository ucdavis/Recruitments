using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Department : DomainObject<int>
    {
        private string _DepartmentFIS;

        public virtual string DepartmentFIS
        {
            get { return _DepartmentFIS; }
            set { _DepartmentFIS = value; }
        }

        private Position _AssociatedPosition;

        public virtual Position AssociatedPosition
        {
            get { return _AssociatedPosition; }
            set { _AssociatedPosition = value; }
        }

        public Department()
        {

        }
    }

    public class Gender : DomainObject<int>
    {
        private string _GenderType;

        public virtual string GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }

        public Gender()
        {

        }
    }

    public class RecruitmentSrc : DomainObject<int>
    {
        private string _RecruitmentSource;

        public virtual string RecruitmentSource
        {
            get { return _RecruitmentSource; }
            set { _RecruitmentSource = value; }
        }

        public RecruitmentSrc()
        {

        }
    }

    public class Ethnicity : DomainObject<int>
    {
        private string _EthnicityValue;

        public virtual string EthnicityValue
        {
            get { return _EthnicityValue; }
            set { _EthnicityValue = value; }
        }

        public Ethnicity()
        {

        }
    }

    public class FileType : DomainObject<int>
    {
        private string _FileTypeName;

        public virtual string FileTypeName
        {
            get { return _FileTypeName; }
            set { _FileTypeName = value; }
        }

        public FileType()
        {

        }
    }

    public class Login : DomainObject<string>
    {
        private User _User;

        public virtual User User
        {
            get { return _User; }
            set { _User = value; }
        }

        public Login()
        {
        }
    }

    public class Unit : DomainObject<int>
    {
        private User _User;

        public virtual User User
        {
            get { return _User; }
            set { _User = value; }
        }

        private string _FullName;

        public virtual string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        private string _PPSCode;

        public virtual string PPSCode
        {
            get { return _PPSCode; }
            set { _PPSCode = value; }
        }

        private string _FISCode;

        public virtual string FISCode
        {
            get { return _FISCode; }
            set { _FISCode = value; }
        }

        public Unit()
        {

        }
    }

    public class ChangeType : DomainObject<int>
    {
        private string _Type;

        public virtual string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

    }

    public enum ChangeTypes
    {
        Save            = 1,
        Delete          = 2,
        Update          = 3
    }

    public class MemberType : DomainObject<int>
    {
        private string _Type;

        public virtual string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public MemberType()
        {

        }
    }

    public enum MemberTypes 
    {
        AllCommittee                = 0,
        CommitteeMember             = 1,
        CommitteeChair              = 2,
        FacultyMember               = 3
    }

    public class ReferSource : DomainObject<int>
    {
        private string _Source;

        public virtual string Source
        {
            get { return _Source; }
            set { _Source = value; }
        }
	
        public ReferSource()
        {
        }

        public ReferSource(string source)
        {
            this.Source = source;
        }
    }
}
