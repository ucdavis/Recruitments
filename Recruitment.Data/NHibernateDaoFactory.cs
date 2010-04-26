using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Data
{
    /// <summary>
    /// Exposes access to NHibernate DAO classes.  Motivation for this DAO
    /// framework can be found at http://www.hibernate.org/328.html.
    /// </summary>
    public class NHibernateDaoFactory : IDaoFactory
    {
        public IApplicationsDao GetApplicationsDao()
        {
            return new ApplicationsDao();
        }

        public IPositionsDao GetPositionsDao()
        {
            return new PositionsDao();
        }

        #region Inline DAO implementations

        /// <summary>
        /// Concrete DAO for accessing instances of <see cref="Customer" /> from DB.
        /// This should be extracted into its own class-file if it needs to extend the
        /// inherited DAO functionality.
        /// </summary>
        public class PositionsDao : AbstractNHibernateDao<Positions, int>, IPositionsDao { }

        public class ApplicationsDao : AbstractNHibernateDao<Applications, int>, IApplicationsDao { }
        #endregion
    }
}
