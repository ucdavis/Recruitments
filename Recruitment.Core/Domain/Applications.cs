using System;
using System.Collections.Generic;
using System.Text;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Applications : DomainObject<int>
    {
        #region Properties
        private int _ProfileID;

        public int ProfileID
        {
            get { return _ProfileID; }
            set { _ProfileID = value; }
        }

        private Positions _AppliedPosition;

        public Positions AppliedPosition
        {
            get { return _AppliedPosition; }
            set { _AppliedPosition = value; }
        }

        private bool _Submitted;

        public bool Submitted
        {
            get { return _Submitted; }
            set { _Submitted = value; }
        }

        private DateTime? _SubmitDate;

        public DateTime? SubmitDate
        {
            get { return _SubmitDate; }
            set { _SubmitDate = value; }
        } 
        #endregion

    }
}
