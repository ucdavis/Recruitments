using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Text.RegularExpressions;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Position : DomainObject<int>
    {      
        private string _PositionTitle;

        [NotNullValidator()]
        [StringLengthValidator(1,100)]
        public virtual string PositionTitle
        {
            get { return _PositionTitle; }
            set { _PositionTitle = value; }
        }
        private string _PositionNumber;

        [IgnoreNulls()]
        [StringLengthValidator(0, 20)]
        public virtual string PositionNumber
        {
            get { return _PositionNumber; }
            set { _PositionNumber = value; }
        }
        private string _ShortDescription;

        [IgnoreNulls()]
        public virtual string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        
        private DateTime _DatePosted;

        [NotNullValidator()]
        public virtual DateTime DatePosted
        {
            get { return _DatePosted; }
            set { _DatePosted = value; }
        }
        private DateTime _Deadline;

        [NotNullValidator()]
        public virtual DateTime Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }

        private Template _ReferenceTemplate;

        [NotNullValidator]
        public virtual Template ReferenceTemplate
        {
            get { return _ReferenceTemplate; }
            set { _ReferenceTemplate = value; }
        }
        
        private bool _AllowApps;

        public virtual bool AllowApps
        {
            get { return _AllowApps; }
            set { _AllowApps = value; }
        }

        private int _NumReferences;

        public virtual int NumReferences
        {
            get { return _NumReferences; }
            set { _NumReferences = value; }
        }
        private int _NumPublications;

        public virtual int NumPublications
        {
            get { return _NumPublications; }
            set { _NumPublications = value; }
        }
        private bool _CommitteeView;

        public virtual bool CommitteeView
        {
            get { return _CommitteeView; }
            set { _CommitteeView = value; }
        }
        private bool _FacultyView;

        public virtual bool FacultyView
        {
            get { return _FacultyView; }
            set { _FacultyView = value; }
        }
        private bool _Vote;

        public virtual bool Vote
        {
            get { return _Vote; }
            set { _Vote = value; }
        }
        private bool _FinalVote;

        public virtual bool FinalVote
        {
            get { return _FinalVote; }
            set { _FinalVote = value; }
        }
        private string _HRRep;

        [NotNullValidator]
        [StringLengthValidator(0, 100)]
        public virtual string HRRep
        {
            get { return _HRRep; }
            set { _HRRep = value; }
        }
        private string _HRPhone;

        [RegexValidator(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", 
            MessageTemplate="Phone number must be properly formatted")]
        public virtual string HRPhone
        {
            get { return _HRPhone; }
            set { _HRPhone = value; }
        }
        private string _HREmail;

        [NotNullValidator]
        [StringLengthValidator(7, RangeBoundaryType.Inclusive,
            150, RangeBoundaryType.Inclusive,
            MessageTemplate="Email address must be from 7 to 150 characters")]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            MessageTemplate="Email must be properly formatted")]
        public virtual string HREmail
        {
            get { return _HREmail; }
            set { _HREmail = value; }
        }

        private bool _Closed;

        public virtual bool Closed
        {
            get { return _Closed; }
            set { _Closed = value; }
        }

        private bool _AdminAccepted;

        public virtual bool AdminAccepted
        {
            get { return _AdminAccepted; }
            set { _AdminAccepted = value; }
        }

        private int _DescriptionFileID;

        [IgnoreNulls()]
        public virtual int DescriptionFileID
        {
            get { return _DescriptionFileID; }
            set { _DescriptionFileID = value; }
        }

        private File _DescriptionFile;

        [NotNullValidator]
        public virtual File DescriptionFile
        {
            get { return _DescriptionFile; }
            set { _DescriptionFile = value; }
        }

        private Department _PrimaryDepartment;

        public virtual Department PrimaryDepartment
        {
            get { return _PrimaryDepartment; }
            set { _PrimaryDepartment = value; }
        }

        private IList<FileType> _FileTypes;

        public virtual IList<FileType> FileTypes
        {
            get { return _FileTypes; }
            set { _FileTypes = value; }
        }

        private IList<Department> _Departments;

        public virtual IList<Department> Departments
        {
            get { return _Departments; }
            set { _Departments = value; }
        }

        private IList<Application> _AssociatedApplications;

        public virtual IList<Application> AssociatedApplications
        {
            get { return _AssociatedApplications; }
            set { _AssociatedApplications = value; }
        }

        private IList<CommitteeMember> _CommitteeMembers;

        public virtual IList<CommitteeMember> CommitteeMembers
        {
            get { return _CommitteeMembers; }
            set { _CommitteeMembers = value; }
        }

        public virtual string TitleAndApplicationCount
        {
            get
            {
                return string.Format("{0} ({1} {2})", PositionTitle, ApplicationCount, ApplicationCount == 1 ? "Application" : "Applications");
            }
        }

        public virtual int ApplicationCount
        {
            get { return AssociatedApplications.Count; }
        }

        public virtual string DepartmentList
        {
            get
            {
                StringBuilder deptList = new StringBuilder();

                for (int i = 0; i < Departments.Count; i++)
                {
                    if (i == 0)
                        deptList.Append(Departments[i].Unit == null ? Departments[i].DepartmentFIS : Departments[i].Unit.FullName);
                    else
                    {
                        deptList.Append(", ");
                        deptList.Append(Departments[i].Unit == null ? Departments[i].DepartmentFIS : Departments[i].Unit.FullName);
                    }
                }

                return deptList.ToString();
            }
        }

        public Position()
        {
            Closed = false;
            AdminAccepted = false;
        }
    }
}
