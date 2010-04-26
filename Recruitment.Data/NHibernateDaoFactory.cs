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
        public IApplicationDao GetApplicationDao()
        {
            return new ApplicationDao();
        }

        public IPositionDao GetPositionDao()
        {
            return new PositionDao();
        }

        public IApplicantDao GetApplicantDao()
        {
            return new ApplicantDao();
        }

        public IProfileDao GetProfileDao()
        {
            return new ProfileDao();
        }

        #region Inline DAO implementations

        /// <summary>
        /// Concrete DAO for accessing instances of <see cref="Customer" /> from DB.
        /// This should be extracted into its own class-file if it needs to extend the
        /// inherited DAO functionality.
        /// </summary>
        public class PositionDao : AbstractNHibernateDao<Position, int>, IPositionDao { }

        public class ApplicationDao : AbstractNHibernateDao<Application, int>, IApplicationDao { }

        public class ApplicantDao : AbstractNHibernateDao<Applicant, int>, IApplicantDao { }

        public class ProfileDao : AbstractNHibernateDao<Profile, int>, IProfileDao { }

        #endregion

    }
}
