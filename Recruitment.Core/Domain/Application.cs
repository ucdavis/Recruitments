using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class Application : DomainObject<int>
    {
        private Profile _AssociatedProfile;

        [NotNullValidator]
        public virtual Profile AssociatedProfile
        {
            get { return _AssociatedProfile; }
            set { _AssociatedProfile = value; }
        }
        
        private Position _AppliedPosition;

        [NotNullValidator]
        public virtual Position AppliedPosition
        {
            get { return _AppliedPosition; }
            set { _AppliedPosition = value; }
        }

        private bool _Submitted;

        [NotNullValidator]
        public virtual bool Submitted
        {
            get { return _Submitted; }
            set { _Submitted = value; }
        }
        private DateTime? _SubmitDate;

        [IgnoreNulls]
        public virtual DateTime? SubmitDate
        {
            get { return _SubmitDate; }
            set { _SubmitDate = value; }
        }

        private DateTime _LastUpdated;

        [NotNullValidator]
        public virtual DateTime LastUpdated
        {
            get { return _LastUpdated; }
            set { _LastUpdated = value; }
        }

        private string _Email;
        
        [StringLengthValidator(50)]
        [NotNullValidator]
        public virtual string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        
        private IList<File> _Files;

        public virtual IList<File> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }

        private IList<Survey> _Surveys;

        public virtual IList<Survey> Surveys
        {
            get { return _Surveys; }
            set { _Surveys = value; }
        }

        private IList<CurrentPosition> _CurrentPositions;

        public virtual IList<CurrentPosition> CurrentPositions
        {
            get { return _CurrentPositions; }
            set { _CurrentPositions = value; }
        }

        private IList<Education> _Education;

        public virtual IList<Education> Education
        {
            get { return _Education; }
            set { _Education = value; }
        }

        private IList<Reference> _References;

        public virtual IList<Reference> References
        {
            get { return _References; }
            set { _References = value; }
        }
                
        public Application()
        {

        }

        /// <summary>
        /// Checks to see if the current Application is valid
        /// </summary>
        /// <returns>true if valid, else false</returns>
        public virtual bool isValid()
        {
            return ValidateBO<Application>.isValid(this);
        }

        public virtual bool isComplete()
        {
            bool Complete = false;

            foreach (Reference r in References)
            {
                Complete = Complete & r.isComplete();
            }

            foreach (Education edu in Education)
            {
                Complete = Complete & edu.isComplete();
            }

            foreach (Survey s in Surveys)
            {
                Complete = Complete & s.isComplete();
            }

            foreach (CurrentPosition p in CurrentPositions)
            {
                Complete = Complete & p.isComplete();
            }

            return Complete;
        }

    }
}
