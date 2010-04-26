using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Profile : DomainObject<int>
    {
        private string _FirstName;

        [StringLengthValidator(50)]
        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _MiddleName;

        [StringLengthValidator(50)]
        public virtual string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }
        private string _LastName;

        [StringLengthValidator(50)]
        public virtual string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _Address1;

        [StringLengthValidator(100)]
        public virtual string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        private string _Address2;

        [StringLengthValidator(100)]
        public virtual string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _City;

        [StringLengthValidator(50)]
        public virtual string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _State;

        [StringLengthValidator(50)]
        public virtual string State
        {
            get { return _State; }
            set { _State = value; }
        }
        private string _Zip;

        [StringLengthValidator(20)]
        public virtual string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Country;

        [StringLengthValidator(50)]
        public virtual string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        private string _CountryCode;

        [StringLengthValidator(5)]
        public virtual string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        
        private string _Phone;

        [StringLengthValidator(20)]
        public virtual string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private DateTime? _LastUpdated;

        public virtual DateTime? LastUpdated
        {
            get { return _LastUpdated; }
            set { _LastUpdated = value; }
        }
        
        private Applicant _AssociatedApplicant;

        [NotNullValidator]
        public virtual Applicant AssociatedApplicant
        {
            get { return _AssociatedApplicant; }
            set { _AssociatedApplicant = value; }
        }

        private IList<Application> _Applications;

        public virtual IList<Application> Applications
        {
            get { return _Applications; }
            set { _Applications = value; }
        }
        
        public Profile()
        {

        }
    }
}
