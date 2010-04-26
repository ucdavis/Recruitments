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
                
        //private int _AccountID;

        //public int AccountID
        //{
        //    get { return _AccountID; }
        //    set { _AccountID = value; }
        //}

        private Profile _Profile;

        public Profile Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }

        private IList<Profile> _Profiles;

        public IList<Profile> Profiles
        {
            get { return _Profiles; }
            set { _Profiles = value; }
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
