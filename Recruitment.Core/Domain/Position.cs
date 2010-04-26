using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Text.RegularExpressions;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Position : DomainObject<int>
    {
        const int STRING_LENGTH = 100;
        const int LONG_STRING = 500;
        
        private string _PositionTitle;

        [StringLengthValidator(1,STRING_LENGTH)]
        public virtual string PositionTitle
        {
            get { return _PositionTitle; }
            set { _PositionTitle = value; }
        }
        private string _PositionNumber;

        [StringLengthValidator(1, STRING_LENGTH)]
        public virtual string PositionNumber
        {
            get { return _PositionNumber; }
            set { _PositionNumber = value; }
        }
        private string _ShortDescription;

        [StringLengthValidator(1, LONG_STRING)]
        public virtual string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        private int _DescriptionFileID;

        [NotNullValidator()]
        public virtual int DescriptionFileID
        {
            get { return _DescriptionFileID; }
            set { _DescriptionFileID = value; }
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

        [NotNullValidator()]
        public virtual string HRRep
        {
            get { return _HRRep; }
            set { _HRRep = value; }
        }
        private string _HRAreaCode;

        [StringLengthValidator(3,3,
            MessageTemplate="Area code must be 3 characters",
            Ruleset="primary")]
        [RegexValidator(@"\d{3}",
            MessageTemplate="Area code must be numbers only",
            Ruleset="primary")]
        public virtual string HRAreaCode
        {
            get { return _HRAreaCode; }
            set { _HRAreaCode = value; }
        }
        private string _HRPhone;

        [StringLengthValidator(7,7, 
            MessageTemplate="Phone number must be 7 characters",
            Ruleset="primary")]
        [RegexValidator(@"\d{7}", 
            MessageTemplate="Phone number must be numbers only",
            Ruleset="primary")]
        public virtual string HRPhone
        {
            get { return _HRPhone; }
            set { _HRPhone = value; }
        }
        private string _HREmail;

        [StringLengthValidator(7, RangeBoundaryType.Inclusive,
            150, RangeBoundaryType.Inclusive,
            MessageTemplate="Email address must be from 7 to 150 characters",
            Ruleset="primary")]
        [ContainsCharactersValidator("@.", ContainsCharacters.All,
            MessageTemplate="Email must have an @ and at least one dot",
            Ruleset="primary")]
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
        
        public Position()
        {
            Closed = false;
            AdminAccepted = false;
        }
    }
}
