using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Reference : DomainObject<int>, IApplicationStep
    {
        private string _Title;

        public virtual string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _FirstName;

        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName;

        public virtual string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName;

        public virtual string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public virtual string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleName))
                    return FirstName + " " + LastName;
                else
                    return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        private string _AcadTitle;

        public virtual string AcadTitle
        {
            get { return _AcadTitle; }
            set { _AcadTitle = value; }
        }
        private string _Expertise;

        public virtual string Expertise
        {
            get { return _Expertise; }
            set { _Expertise = value; }
        }
        private string _Dept;

        public virtual string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
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
        private string _Phone;

        public virtual string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _Email;

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private Application _AssociatedApplication;

        public virtual Application AssociatedApplication
        {
            get { return _AssociatedApplication; }
            set { _AssociatedApplication = value; }
        }

        private File _ReferenceFile;

        public virtual File ReferenceFile
        {
            get { return _ReferenceFile; }
            set { _ReferenceFile = value; }
        }
                        
        private IList<File> _Files;

        public virtual IList<File> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }

        public Reference()
        {
            ApplicationStepType = ApplicationStepType.References;
        }

        private bool _Complete;

        public virtual bool Complete
        {
            get { return _Complete; }
            set { _Complete = value; }
        }

        public virtual bool isComplete()
        {
            return Complete;
        }

        #region IApplicationStep Members

        private ApplicationStepType _ApplicationStepType;

        public virtual ApplicationStepType ApplicationStepType
        {
            get { return _ApplicationStepType; }
            set { _ApplicationStepType = value; }
        }

        #endregion
    }
}
