using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Position : DomainObject<int>
    {
        private string _PositionTitle;

        public string PositionTitle
        {
            get { return _PositionTitle; }
            set { _PositionTitle = value; }
        }
        private string _PositionNumber;

        public string PositionNumber
        {
            get { return _PositionNumber; }
            set { _PositionNumber = value; }
        }
        private string _ShortDescription;

        public string ShortDescription
        {
            get { return _ShortDescription; }
            set { _ShortDescription = value; }
        }
        private int _DescriptionFileID;

        public int DescriptionFileID
        {
            get { return _DescriptionFileID; }
            set { _DescriptionFileID = value; }
        }
        
        private List<string> _DepartmentsFIS;

        public List<string> DepartmentsFIS
        {
            get { return _DepartmentsFIS; }
            set { _DepartmentsFIS = value; }
        }
        private DateTime _DatePosted;

        public DateTime DatePosted
        {
            get { return _DatePosted; }
            set { _DatePosted = value; }
        }
        private DateTime _Deadline;

        public DateTime Deadline
        {
            get { return _Deadline; }
            set { _Deadline = value; }
        }
        private bool _AllowApps;

        public bool AllowApps
        {
            get { return _AllowApps; }
            set { _AllowApps = value; }
        }

        private int _NumReferences;

        public int NumReferences
        {
            get { return _NumReferences; }
            set { _NumReferences = value; }
        }
        private int _NumPublications;

        public int NumPublications
        {
            get { return _NumPublications; }
            set { _NumPublications = value; }
        }
        private bool _CommitteeView;

        public bool CommitteeView
        {
            get { return _CommitteeView; }
            set { _CommitteeView = value; }
        }
        private bool _FacultyView;

        public bool FacultyView
        {
            get { return _FacultyView; }
            set { _FacultyView = value; }
        }
        private bool _Vote;

        public bool Vote
        {
            get { return _Vote; }
            set { _Vote = value; }
        }
        private bool _FinalVote;

        public bool FinalVote
        {
            get { return _FinalVote; }
            set { _FinalVote = value; }
        }
        private string _HRRep;

        public string HRRep
        {
            get { return _HRRep; }
            set { _HRRep = value; }
        }
        private string _HRAreaCode;

        public string HRAreaCode
        {
            get { return _HRAreaCode; }
            set { _HRAreaCode = value; }
        }
        private string _HRPhone;

        public string HRPhone
        {
            get { return _HRPhone; }
            set { _HRPhone = value; }
        }
        private string _HREmail;

        public string HREmail
        {
            get { return _HREmail; }
            set { _HREmail = value; }
        }

        private IList<Application> _AssociatedApplications;

        public IList<Application> AssociatedApplications
        {
            get { return _AssociatedApplications; }
            set { _AssociatedApplications = value; }
        }

        public Position()
        {
            //throw new System.NotImplementedException();
        }

        public void Fill(int PositionID)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
