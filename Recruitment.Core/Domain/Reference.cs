using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Reference : DomainObject<int>, IApplicationStep
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName;

        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _AcadTitle;

        public string AcadTitle
        {
            get { return _AcadTitle; }
            set { _AcadTitle = value; }
        }
        private string _Expertise;

        public string Expertise
        {
            get { return _Expertise; }
            set { _Expertise = value; }
        }
        private string _Dept;

        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
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
        private string _Phone;

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private Application _AssociatedApplication;

        public Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }
                
        private IList<File> _Files;

        public IList<File> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }

        public Reference()
        {
            ApplicationStepType = ApplicationStepType.References;
        }

        private bool _Complete;

        public bool Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
        }

        public bool isComplete()
        {
            return Complete;
        }

        #region IApplicationStep Members

        private ApplicationStepType _ApplicationStepType;

        public ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }

        #endregion
    }
}
