using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Education : DomainObject<int>, IApplicationStep
    {

        private Application _AssociatedApplication;

        public Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }

        private DateTime _Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string _Institution;

        public string Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }
        private string _Discipline;

        public string Discipline
        {
            get { return _Discipline; }
            set { _Discipline = value; }
        }

        private bool _Complete;

        public bool Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
        }

        public Education()
        {
            ApplicationStepType = ApplicationStepType.Education;
        }
        
        #region IApplicationStep Members


        public bool isComplete()
        {
            return Complete;
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

        private ApplicationStepType _ApplicationStepType;

        public ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
