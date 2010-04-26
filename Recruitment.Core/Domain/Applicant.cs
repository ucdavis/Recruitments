using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Applicant : DomainObject<int>
    {
        private string _Email;

        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _CreatedBy;

        public virtual string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private bool _IsActive;

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
            //throw new System.NotImplementedException();
        }

        public virtual void Fill(int AccountID, bool LoadProfile)
        {
            throw new System.NotImplementedException();
        }
    }
}
