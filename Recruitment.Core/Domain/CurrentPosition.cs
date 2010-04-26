using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class CurrentPosition : DomainObject<int>, IApplicationStep
    {
        private int _ApplicationID;

        private Application _AssociatedApplication;

        public Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }
        
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Department;

        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        private string _Institution;

        public string Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }
        private string _Address1;

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        private string _Address2;

        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _State;

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _Zip;

        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Country;

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private bool _Complete;

        public bool Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
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

        public CurrentPosition()
        {
            ApplicationStepType = ApplicationStepType.CurrentPosition;
        }

        private ApplicationStepType _ApplicationStepType;

        public ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
