using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Reference : DomainObject<int>, IApplicationStep
    {
        private string _Title;

        [IgnoreNulls]
        [StringLengthValidator(0, 50)]
        public virtual string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _FirstName;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName;

        [IgnoreNulls]
        [StringLengthValidator(0, 50)]
        public virtual string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
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

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string AcadTitle
        {
            get { return _AcadTitle; }
            set { _AcadTitle = value; }
        }
        private string _Expertise;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string Expertise
        {
            get { return _Expertise; }
            set { _Expertise = value; }
        }
        private string _Dept;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }
        private string _Institution;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }
        private string _Address1;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        private string _Address2;

        [IgnoreNulls]
        [StringLengthValidator(0, 50)]
        public virtual string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _City;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _State;

        [NotNullValidator]
        [StringLengthValidator(1, 50)]
        public virtual string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _Zip;

        [NotNullValidator]
        [StringLengthValidator(1, 20)]
        public virtual string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Country;

        [NotNullValidator]
        [StringLengthValidator(0, 50)]
        public virtual string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private string _Phone;

        [NotNullValidator]
        [StringLengthValidator(1, 20)]
        public virtual string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        private string _Email;

        [NotNullValidator]
        [StringLengthValidator(1, 100)]
        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private bool _SentEmail;

        [NotNullValidator]
        public virtual bool SentEmail
        {
            get { return _SentEmail; }
            set { _SentEmail = value; }
        }

        private string _UploadID;

        [StringLengthValidator(50)]
        [IgnoreNulls]
        public virtual string UploadID
        {
            get { return _UploadID; }
            set { _UploadID = value; }
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
