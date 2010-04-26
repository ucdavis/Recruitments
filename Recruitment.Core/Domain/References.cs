using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class References : IApplicationStep
    {
        private List<Reference> _ReferenceList;
        private int _ApplicationID;
        private ApplicationStepType _ApplicationStepType;

        public ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }

        public int ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
        }

        public List<Reference> ReferenceList
        {
            get { return _ReferenceList; }
            set { _ReferenceList = value; }
        }

        #region IApplicationStep Members


        public bool isComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Fill()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Save()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
