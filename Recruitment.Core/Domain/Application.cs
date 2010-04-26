using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Application : DomainObject<int>
    {
        //private int _PositionID;
        
        private int _ProfileID;

        public int ProfileID
        {
            get { return _ProfileID; }
            set { _ProfileID = value; }
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
        private List<File> _ApplicationFiles;

        public List<File> ApplicationFiles
        {
            get { return _ApplicationFiles; }
            set { _ApplicationFiles = value; }
        }

        public Application()
        {
            //throw new System.NotImplementedException();
        }

        private List<IApplicationStep> _ApplicationSteps;

        public List<IApplicationStep> ApplicationSteps
        {
            get { return _ApplicationSteps; }
            set { _ApplicationSteps = value; }
        }

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
