using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class User : DomainObject<User, int>
    {
        private IList<Login> _LoginIDs;

        public virtual IList<Login> LoginIDs
        {
            get { return _LoginIDs; }
            set { _LoginIDs = value; }
        }

        private IList<Unit> _Units;

        public virtual IList<Unit> Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        
        private string _FirstName;

        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;

        public virtual string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _EmployeeID;

        public virtual string EmployeeID
        {
            get { return _EmployeeID; }
            set { _EmployeeID = value; }
        }

        private string _StudentID;

        public virtual string StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }

        private string _SID;

        public virtual string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }

        private Guid _UserKey;

        public virtual Guid UserKey
        {
            get { return _UserKey; }
            set { _UserKey = value; }
        }
                
        private bool _Inactive;

        public virtual bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }
        
        public static List<string> FindUCDKerberosIDs(string NameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public User()
        {

        }
    }
}
