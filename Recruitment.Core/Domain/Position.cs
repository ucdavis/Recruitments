using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Position : DomainObject<int>
    {
        private string _PositionTitle;

        public virtual string PositionTitle
        {
            get { return _PositionTitle; }
            set { _PositionTitle = value; }
        }
        private string _PositionNumber;

        public virtual string PositionNumber
        {
            get { return _PositionNumber; }
            set { _PositionNumber = value; }
        }
        private string _ShortDescription;

        public virtual string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        private int _DescriptionFileID;

        public virtual int DescriptionFileID
        {
            get { return _DescriptionFileID; }
            set { _DescriptionFileID = value; }
        }
        
        private DateTime _DatePosted;

        public virtual DateTime DatePosted
        {
            get { return _DatePosted; }
            set { _DatePosted = value; }
        }
        private DateTime _Deadline;

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

        public virtual string HRRep
        {
            get { return _HRRep; }
            set { _HRRep = value; }
        }
        private string _HRAreaCode;

        public virtual string HRAreaCode
        {
            get { return _HRAreaCode; }
            set { _HRAreaCode = value; }
        }
        private string _HRPhone;

        public virtual string HRPhone
        {
            get { return _HRPhone; }
            set { _HRPhone = value; }
        }
        private string _HREmail;

        public virtual string HREmail
        {
            get { return _HREmail; }
            set { _HREmail = value; }
        }

        private bool _Closed;

        public bool Closed
        {
            get { return _Closed; }
            set { _Closed = value; }
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
            //throw new System.NotImplementedException();
        }
    }
}
