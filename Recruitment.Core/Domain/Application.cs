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

        private ReferSource _ReferSource;

        [NotNullValidator]
        public virtual ReferSource ReferSource
        {
            get { return _ReferSource; }
            set { _ReferSource = value; }
        }

        private string _ReferSourceOther;

        [IgnoreNulls]
        [StringLengthValidator(50)]
        public virtual string ReferSourceOther
        {
            get { return _ReferSourceOther; }
            set { _ReferSourceOther = value; }
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

        private DateTime? _LastUpdated;

        [NotNullValidator]
        public virtual DateTime? LastUpdated
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

        /// <summary>
        /// Using an Generic IEnumerable return to get all steps in this application
        /// </summary>
        /// <returns>Enumerable list of IApplicationSteps</returns>
        public virtual IEnumerable<IApplicationStep> GetSteps()
        {
            foreach (Reference r in References)
            {
                yield return r;
            }

            foreach (Education edu in Education)
            {
                yield return edu;
            }

            foreach (Survey s in Surveys)
            {
                yield return s;
            }

            foreach (CurrentPosition p in CurrentPositions)
            {
                yield return p;
            }
        }

        public virtual bool isComplete()
        {
            foreach (IApplicationStep step in this.GetSteps())
            {
                if (step.isComplete() == false)
                    return false;
            }

            return true;
        }

    }
}
