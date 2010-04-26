using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class User : DomainObject<int>
    {
        private IList<string> _LoginIDs;

        public IList<string> LoginIDs
        {
            get { return _LoginIDs; }
            set { _LoginIDs = value; }
        }

        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _EmployeeID;

        public string EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        private string _StudentID;

        public string StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }

        private bool _Inactive;

        public bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }
        
        public static List<string> FindUCDKerberosIDs(string NameToMatch)
        {
            throw new System.NotImplementedException();
        }
    }
}
