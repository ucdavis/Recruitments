using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Department : DomainObject<int>
    {
        private string _DepartmentFIS;

        public string DepartmentFIS
        {
            get { return _DepartmentFIS; }
            set { _DepartmentFIS = value; }
        }

        private Position _AssociatedPosition;

        public Position AssociatedPosition
        {
            get { return _AssociatedPosition; }
            set { _AssociatedPosition = value; }
        }

        public Department()
        {

        }
    }

    public class Gender : DomainObject<int>
    {
        private string _GenderType;

        public string GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }

        public Gender()
        {

        }
    }

    public class RecruitmentSrc : DomainObject<int>
    {
        private string _RecruitmentSource;

        public string RecruitmentSource
        {
            get { return _RecruitmentSource; }
            set { _RecruitmentSource = value; }
        }

        public RecruitmentSrc()
        {

        }
    }

    public class Ethnicity : DomainObject<int>
    {
        private string _EthnicityValue;

        public string EthnicityValue
        {
            get { return _EthnicityValue; }
            set { _EthnicityValue = value; }
        }

        public Ethnicity()
        {

        }
    }
}
