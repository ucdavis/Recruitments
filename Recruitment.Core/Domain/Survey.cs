using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Survey : IApplicationStep
    {
        private int _ApplicationID;
        private int _Ethnicity;

        public int Ethnicity
        {
            get { return _Ethnicity; }
            set { _Ethnicity = value; }
        }
        private int _Gender;

        public int Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        private int _RecruitmentSrc;

        public int RecruitmentSrc
        {
            get { return _RecruitmentSrc; }
            set { _RecruitmentSrc = value; }
        }

        public int ApplicationID
        {
            get { return _ApplicationID; }
            set { _ApplicationID = value; }
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
