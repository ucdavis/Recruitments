using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Profile : DomainObject<int>
    {
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

        private Applicant _AssociatedApplicant;

        public Applicant AssociatedApplicant
        {
            get { return _AssociatedApplicant; }
            set { _AssociatedApplicant = value; }
        }
        
        //private int _AccountID;

        public Profile()
        {
            //throw new System.NotImplementedException();
        }

        public void Fill(int ApplicantID)
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
