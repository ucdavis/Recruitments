using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Survey : DomainObject<int>, IApplicationStep
    {
        private Gender _Gender;

        public virtual Gender Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private RecruitmentSrc _RecruitmentSrc;

        public virtual RecruitmentSrc RecruitmentSrc
        {
            get { return _RecruitmentSrc; }
            set { _RecruitmentSrc = value; }
        }

        private Ethnicity _Ethnicity;

        public virtual Ethnicity Ethnicity
        {
            get { return _Ethnicity; }
            set { _Ethnicity = value; }
        }

        private Application _AssociatedApplication;

        public virtual Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }
        
        private string _TribalAffiliation;

        public virtual string TribalAffiliation
        {
            get { return _TribalAffiliation; }
            set { _TribalAffiliation = value; }
        }

        private string _Pub_Advertisement;

        public virtual string Pub_Advertisement
        {
            get { return _Pub_Advertisement; }
            set { _Pub_Advertisement = value; }
        }

        private string _Prof_Organization;

        public virtual string Prof_Organization
        {
            get { return _Prof_Organization; }
            set { _Prof_Organization = value; }
        }

        private string _Other;

        public virtual string Other
        {
            get { return _Other; }
            set { _Other = value; }
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
            return Complete;
        }

        #endregion

        public Survey()
        {
            ApplicationStepType = ApplicationStepType.Survey;
        }

        private ApplicationStepType _ApplicationStepType;

        public virtual ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
