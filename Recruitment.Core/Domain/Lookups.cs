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

        public virtual string GetTypeByEnum(MemberTypes type)
        {
            switch (type)
            {
                case MemberTypes.AllCommittee:
                    return "AllCommittee";
                case MemberTypes.CommitteeChair:
                    return "CommitteeChair";
                case MemberTypes.CommitteeMember:
                    return "CommitteeMember";
                case MemberTypes.FacultyMember:
                    return "FacultyMember";
                default:
                    return "Unknown";
            }
        }
    }

    public enum MemberTypes 
    {
        AllCommittee,
        CommitteeMember,
        CommitteeChair,
        FacultyMember
    }
}
