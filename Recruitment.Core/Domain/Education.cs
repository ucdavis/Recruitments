using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Education : DomainObject<int>, IApplicationStep
    {

        private Application _AssociatedApplication;

        public virtual Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }

        private DateTime _Date;

        public virtual DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string _Institution;

        public virtual string Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }
        private string _Discipline;

        public virtual string Discipline
        {
            get { return _Discipline; }
            set { _Discipline = value; }
        }

        private bool _Complete;

        public virtual bool Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
        }

        public Education()
        {
            ApplicationStepType = ApplicationStepType.Education;
        }
        
        #region IApplicationStep Members


        public virtual bool isComplete()
        {
            return Complete;
        }

        public virtual void Fill()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void Save()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        private ApplicationStepType _ApplicationStepType;

        public virtual ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
