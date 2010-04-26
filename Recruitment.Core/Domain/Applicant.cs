using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Applicant : DomainObject<int>
    {
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _CreatedBy;

        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        private bool _IsActive;

        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        private IList<Profile> _Profiles;

        public IList<Profile> Profiles
        {
            get { return _Profiles; }
            set { _Profiles = value; }
        }

        public Profile MainProfile
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

        public void Fill(int AccountID, bool LoadProfile)
        {
            throw new System.NotImplementedException();
        }
    }
}
