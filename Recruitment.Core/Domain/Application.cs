using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Application : DomainObject<int>
    {
        //private int _ProfileID;

        //public int ProfileID
        //{
        //    get { return _ProfileID; }
        //    set { _ProfileID = value; }
        //}

        private Profile _AssociatedProfile;

        public Profile AssociatedProfile
        {
            get { return _AssociatedProfile; }
            set { _AssociatedProfile = value; }
        }
        
        private Position _AppliedPosition;

        public Position AppliedPosition
        {
            get { return _AppliedPosition; }
            set { _AppliedPosition = value; }
        }

        private List<Reference> _References;

        public List<Reference> References
        {
            get { return _References; }
            set { _References = value; }
        }
        private bool _Submitted;

        public bool Submitted
        {
            get { return _Submitted; }
            set { _Submitted = value; }
        }
        private DateTime _SubmitDate;

        public DateTime SubmitDate
        {
            get { return _SubmitDate; }
            set { _SubmitDate = value; }
        }

        private IList<File> _Files;

        public IList<File> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }

        private IList<Survey> _Surveys;

        public IList<Survey> Surveys
        {
            get { return _Surveys; }
            set { _Surveys = value; }
        }
        
        public Application()
        {

        }

        //private List<IApplicationStep> _ApplicationSteps;

        //public List<IApplicationStep> ApplicationSteps
        //{
        //    get { return _ApplicationSteps; }
        //    set { _ApplicationSteps = value; }
        //}

        public bool isComplete()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Fill(int ApplicationID, bool GetFiles, bool GetSteps)
        {
            throw new System.NotImplementedException();
        }

        private int _ApplicationID;
    }
}
