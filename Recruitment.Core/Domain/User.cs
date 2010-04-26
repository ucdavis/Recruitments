using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class User
    {
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int _LoginID;

        public int LoginID
        {
            get { return _LoginID; }
            set { _LoginID = value; }
        }

        public static List<string> FindUCDKerberosIDs(string NameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public void Fill()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
