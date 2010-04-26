using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Survey : DomainObject<int>, IApplicationStep
    {
        //private int _ApplicationID;
        //private int _Ethnicity;

        //public int Ethnicity
        //{
        //    get { return _Ethnicity; }
        //    set { _Ethnicity = value; }
        //}
        //private int _Gender;

        //public int Gender
        //{
        //    get { return _Gender; }
        //    set { _Gender = value; }
        //}
        //private int _RecruitmentSrc;

        //public int RecruitmentSrc
        //{
        //    get { return _RecruitmentSrc; }
        //    set { _RecruitmentSrc = value; }
        //}

        //public int ApplicationID
        //{
        //    get { return _ApplicationID; }
        //    set { _ApplicationID = value; }
        //}

        private Gender _Gender;

        public Gender Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private RecruitmentSrc _RecruitmentSrc;

        public RecruitmentSrc RecruitmentSrc
        {
            get { return _RecruitmentSrc; }
            set { _RecruitmentSrc = value; }
        }

        private Ethnicity _Ethnicity;

        public Ethnicity Ethnicity
        {
            get { return _Ethnicity; }
            set { _Ethnicity = value; }
        }

        private string _TribalAffiliation;

        public string TribalAffiliation
        {
            get { return _TribalAffiliation; }
            set { _TribalAffiliation = value; }
        }

        private string _Pub_Advertisement;

        public string Pub_Advertisement
        {
            get { return _Pub_Advertisement; }
            set { _Pub_Advertisement = value; }
        }

        private string _Prof_Organization;

        public string Prof_Organization
        {
            get { return _Prof_Organization; }
            set { _Prof_Organization = value; }
        }

        private string _Other;

        public string Other
        {
            get { return _Other; }
            set { _Other = value; }
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

        public Survey()
        {
            ApplicationStepType = ApplicationStepType.Survey;
        }

        public static Dictionary<int, string> GetEthnicityChoices()
        {
            throw new System.NotImplementedException();
        }

        public static System.Collections.Generic.Dictionary<int, string> GetGenderChoices()
        {
            throw new System.NotImplementedException();
        }

        public static System.Collections.Generic.Dictionary<int, string> GetRecruitmentSrcChoices()
        {
            throw new System.NotImplementedException();
        }

        private ApplicationStepType _ApplicationStepType;

        public ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }
    }
}
