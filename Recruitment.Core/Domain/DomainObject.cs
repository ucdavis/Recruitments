namespace CAESDO.Recruitment.Core.Domain
{
    public abstract class DomainObject<IdT>
    {
        /// <summary>
        /// ID may be of type string, int, custom type, etc.
        /// </summary>
        public virtual IdT ID
        {
            get { return id; }
        }

        /// <summary>
        /// Transient objects are not associated with an item already in storage.  For instance,
        /// a <see cref="Customer" /> is transient if its ID is 0.
        /// </summary>
        public virtual bool IsTransient()
        {
            return ID == null || ID.Equals(default(IdT));
        }

        /// <summary>
        /// Set to protected to allow unit tests to set this property via reflection and to allow 
        /// domain objects more flexibility in setting this for those objects with assigned IDs.
        /// </summary>
        protected IdT id = default(IdT);
    }
}
