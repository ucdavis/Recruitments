using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Department : DomainObject<Department, int>
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

        private Unit _Unit;

        public virtual Unit Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }

        private bool _PrimaryDept;

        public virtual bool PrimaryDept
        {
            get { return _PrimaryDept; }
            set { _PrimaryDept = value; }
        }

        public Department()
        {

        }

        public override bool Equals(object obj)
        {
            return (this.DepartmentFIS == ((Department)obj).DepartmentFIS);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Theme : DomainObject<Theme, string>
    {
        private string _ThemeName;

        public virtual string ThemeName
        {
            get { return _ThemeName; }
            set { _ThemeName = value; }
        }

        public Theme()
        {

        }
    }

    public class Gender : DomainObject<Gender, int>
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

    public class RecruitmentSrc : DomainObject<RecruitmentSrc, int>
    {
        private string _RecruitmentSource;

        public virtual string RecruitmentSource
        {
            get { return _RecruitmentSource; }
            set { _RecruitmentSource = value; }
        }

        private bool _AllowSpecify;

        public virtual bool AllowSpecify
        {
            get { return _AllowSpecify; }
            set { _AllowSpecify = value; }
        }
        
        public RecruitmentSrc()
        {

        }
    }

    public class Ethnicity : DomainObject<Ethnicity, int>
    {
        private string _EthnicityValue;

        public virtual string EthnicityValue
        {
            get { return _EthnicityValue; }
            set { _EthnicityValue = value; }
        }

        private string _EthnicityCategory;

        public virtual string EthnicityCategory
        {
            get { return _EthnicityCategory; }
            set { _EthnicityCategory = value; }
        }

        public Ethnicity()
        {

        }
    }

    public class FileType : DomainObject<FileType, int>
    {
        private string _FileTypeName;

        public virtual string FileTypeName
        {
            get { return _FileTypeName; }
            set { _FileTypeName = value; }
        }

        private bool _ApplicationFile;

        public virtual bool ApplicationFile
        {
            get { return _ApplicationFile; }
            set { _ApplicationFile = value; }
        }
        
        public FileType()
        {

        }

        public override bool Equals(object obj)
        {
            return (this.FileTypeName == ((FileType)obj).FileTypeName);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Login : DomainObject<Login, string>
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

    public class Unit : DomainObject<Unit, string>
    {
        //private User _User;

        //public virtual User User
        //{
        //    get { return _User; }
        //    set { _User = value; }
        //}

        private string _ShortName;

        public virtual string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
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

        private int _UnitID;

        public virtual int UnitID
        {
            get { return _UnitID; }
            set { _UnitID = value; }
        }

        public virtual string SchoolCode { get; set; }

        public Unit(string fisCode)
        {
            this.id = fisCode;
        }

        public Unit()
        {

        }
    }

    public class ChangeType : DomainObject<ChangeType, int>
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

    public class MemberType : DomainObject<MemberType, int>
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
        FacultyMember               = 3,
        Reviewer                    = 4
    }

    public class SurveyXRecruitmentSrc : DomainObject<SurveyXRecruitmentSrc, int>
    {
        private Survey _AssociatedSurvey;

        public virtual Survey AssociatedSurvey
        {
            get { return _AssociatedSurvey; }
            set { _AssociatedSurvey = value; }
        }

        private RecruitmentSrc _RecruitmentSrc;

        public virtual RecruitmentSrc RecruitmentSrc
        {
            get { return _RecruitmentSrc; }
            set { _RecruitmentSrc = value; }
        }

        private string _RecruitmentSrcOther;

        public virtual string RecruitmentSrcOther
        {
            get { return _RecruitmentSrcOther; }
            set { _RecruitmentSrcOther = value; }
        }

        public SurveyXRecruitmentSrc()
        {

        }
    }
    
    public class TemplateType : DomainObject<TemplateType, int>
    {
        private string _Type;

        public virtual string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public virtual bool IsEmailTemplate { get; set; }

        public TemplateType()
        {

        }
    }

    public class Template : DomainObject<Template, int>
    {
        private string _TemplateText;

        public virtual string TemplateText
        {
            get { return _TemplateText; }
            set { _TemplateText = value; }
        }

        private TemplateType _TemplateType;

        public virtual TemplateType TemplateType
        {
            get { return _TemplateType; }
            set { _TemplateType = value; }
        }

        public Template()
        {

        }
    }
}
