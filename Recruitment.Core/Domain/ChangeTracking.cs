using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace CAESDO.Recruitment.Core.Domain
{
    public class ChangeTracking : DomainObject<int>
    {
        private Guid _TrackingGroupID;

        [NotNullValidator]
        public virtual Guid TrackingGroupID
        {
            get { return _TrackingGroupID; }
            set { _TrackingGroupID = value; }
        }

        private string _ObjectChanged;

        [StringLengthValidator(50)]
        [NotNullValidator]
        public virtual string ObjectChanged
        {
            get { return _ObjectChanged; }
            set { _ObjectChanged = value; }
        }

        private int _ObjectChangedID;

        [NotNullValidator]
        public virtual int ObjectChangedID
        {
            get { return _ObjectChangedID; }
            set { _ObjectChangedID = value; }
        }

        private int _UserID;

        [IgnoreNulls]
        public virtual int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private DateTime _ChangeDate;

        [NotNullValidator]
        public virtual DateTime ChangeDate
        {
            get { return _ChangeDate; }
            set { _ChangeDate = value; }
        }

        private ChangeType _ChangeType;

        [NotNullValidator]
        public virtual ChangeType ChangeType
        {
            get { return _ChangeType; }
            set { _ChangeType = value; }
        }

        private IList<ChangedProperty> _ChangedProperties;

        public virtual IList<ChangedProperty> ChangedProperties
        {
            get { return _ChangedProperties; }
            set { _ChangedProperties = value; }
        }
                
        public ChangeTracking()
        {
            TrackingGroupID = Guid.NewGuid();
            ChangeDate = DateTime.Now;
        }
    }

    public class ChangedProperty : DomainObject<int>
    {
        private string _PropertyChanged;

        public virtual string PropertyChanged
        {
            get { return _PropertyChanged; }
            set { _PropertyChanged = value; }
        }

        private string _PropertyChangedValue;

        public virtual string PropertyChangedValue
        {
            get { return _PropertyChangedValue; }
            set { _PropertyChangedValue = value; }
        }

        private ChangeTracking _AssociatedTracker;

        public virtual ChangeTracking AssociatedTracker
        {
            get { return _AssociatedTracker; }
            set { _AssociatedTracker = value; }
        }
        
        public ChangedProperty()
        {

        }

        public ChangedProperty(string newValue, string propertyName, ChangeTracking associatedTracker)
        {
            this.PropertyChanged = propertyName;
            this.PropertyChangedValue = newValue;
            this.AssociatedTracker = associatedTracker;
        }
    }
}
