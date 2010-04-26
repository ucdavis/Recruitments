using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Applicant : DomainObject<int>
    {
        private string _Email;

        [NotNullValidator]
        [StringLengthValidator(50)]
        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //Maybe this should be a User type instead of an int
        private int _CreatedBy;

        [NotNullValidator]
        public virtual int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private bool _IsActive;

        [NotNullValidator]
        public virtual bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private IList<Profile> _Profiles;

        public virtual IList<Profile> Profiles
        {
            get { return _Profiles; }
            set { _Profiles = value; }
        }

        public virtual Profile MainProfile
        {
            get
            {
                if (Profiles.Count != 0)
                    return Profiles[0];
                else
                    return null;
            }
        }
        
        public Applicant()
        {
        }
    }
}
