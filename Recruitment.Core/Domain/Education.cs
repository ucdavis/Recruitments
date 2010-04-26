using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Education : DomainObject<Ethnicity, int>, IApplicationStep
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

        #endregion

        private ApplicationStepType _ApplicationStepType;

        public virtual ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
