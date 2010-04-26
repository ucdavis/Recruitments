using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class CurrentPosition : DomainObject<int>, IApplicationStep
    {
        private Application _AssociatedApplication;

        public virtual Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }
        
        private string _Title;

        public virtual string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Department;

        public virtual string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        private string _Institution;

        public virtual string Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }
        private string _Address1;

        public virtual string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        private string _Address2;

        public virtual string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _City;

        public virtual string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _State;

        public virtual string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _Zip;

        public virtual string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Country;

        public virtual string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private bool _Complete;

        public virtual bool Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
        }


        #region IApplicationStep Members


        public virtual bool isComplete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public CurrentPosition()
        {
            ApplicationStepType = ApplicationStepType.CurrentPosition;
        }

        private ApplicationStepType _ApplicationStepType;

        public virtual ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
