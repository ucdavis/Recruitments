using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;

namespace CAESDO.Recruitment.Data
{
    /// <summary>
    /// Exposes access to NHibernate DAO classes.  Motivation for this DAO
    /// framework can be found at http://www.hibernate.org/328.html.
    /// </summary>
    public class NHibernateDaoFactory : IDaoFactory
    {
        #region Dao Retrieval Operations
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

        public IDepartmentDao GetDepartmentDao()
        {
            return new DepartmentDao();
        }

        public ISurveyDao GetSurveyDao()
        {
            return new SurveyDao();
        }

        public IRecruitmentSrcDao GetRecruitmentSrcDao()
        {
            return new RecruitmentSrcDao();
        }

        public IEthnicityDao GetEthnicityDao()
        {
            return new EthnicityDao();
        }

        public IGenderDao GetGenderDao()
        {
            return new GenderDao();
        }

        public IFileDao GetFileDao()
        {
            return new FileDao();
        }

        public IEducationDao GetEducationDao()
        {
            return new EducationDao();
        }

        public ICommitteeMemberDao GetCommitteeMemberDao()
        {
            return new CommitteeMemberDao();
        }

        public IUserDao GetUserDao()
        {
            return new UserDao();
        }

        public IFileTypeDao GetFileTypeDao()
        {
            return new FileTypeDao();
        }

        public IMemberTypeDao GetMemberTypeDao()
        {
            return new MemberTypeDao();
        }

        public IChangeTrackingDao GetChangeTrackingDao()
        {
            return new ChangeTrackingDao();
        }

        public IChangeTypeDao GetChangeTypeDao()
        {
            return new ChangeTypeDao();
        }

        public IReferSourceDao GetReferSourceDao()
        {
            return new ReferSourceDao();
        }

        public IThemeDao GetThemeDao()
        {
            return new ThemeDao();
        }

        public IUnitDao GetUnitDao()
        {
            return new UnitDao();
        }

        #endregion

        #region Inline DAO implementations

        /// <summary>
        /// Concrete DAO for accessing instances of <see cref="Customer" /> from DB.
        /// This should be extracted into its own class-file if it needs to extend the
        /// inherited DAO functionality.
        /// </summary>
        public class PositionDao : AbstractNHibernateDao<Position, int>, IPositionDao {
            public List<Position> GetAllPositionsByStatus(bool Closed)
            {
                IQuery query = NHibernateSessionManager.Instance.GetSession().CreateQuery(NHQueries.GetAllPositionsByStatus)
                            .SetBoolean("Closed", Closed);

                return query.List<Position>() as List<Position>;

                //ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Position))
                //    .Add(Expression.Eq("Closed", Closed));

                //return criteria.List<Position>() as List<Position>;
            }

            public List<Position> GetAllPositionsByStatus(bool Closed, bool AdminAccepted)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Position))
                    .Add(Expression.Eq("Closed", Closed))
                    .Add(Expression.Eq("AdminAccepted", AdminAccepted));

                return criteria.List<Position>() as List<Position>;
            }
        }

        public class ApplicationDao : AbstractNHibernateDao<Application, int>, IApplicationDao { }

        public class ApplicantDao : AbstractNHibernateDao<Applicant, int>, IApplicantDao { }

        public class ProfileDao : AbstractNHibernateDao<Profile, int>, IProfileDao { }

        public class DepartmentDao : AbstractNHibernateDao<Department, int>, IDepartmentDao { }

        public class SurveyDao : AbstractNHibernateDao<Survey, int>, ISurveyDao { }

        public class RecruitmentSrcDao : AbstractNHibernateDao<RecruitmentSrc, int>, IRecruitmentSrcDao { }

        public class EthnicityDao : AbstractNHibernateDao<Ethnicity, int>, IEthnicityDao { }

        public class GenderDao : AbstractNHibernateDao<Gender, int>, IGenderDao { }

        public class FileDao : AbstractNHibernateDao<File, int>, IFileDao { }

        public class EducationDao : AbstractNHibernateDao<Education, int>, IEducationDao { }

        public class CommitteeMemberDao : AbstractNHibernateDao<CommitteeMember, int>, ICommitteeMemberDao {

            public List<CommitteeMember> GetAllByMemberType(Position associatedPosition, MemberTypes type)
            {

                //string queryString = "from CAESDO.Recruitment.Core.Domain.CommitteeMember as CM where PositionID = :PositionID "
                //               + " and (MemberTypeID = :MemberTypeID or MemberTypeID = :MemberTypeSecondaryID)";
                
                int MemberTypeID, MemberTypeSecondaryID;

                //If we want all committee members, we must get the chair an members
                if (type == MemberTypes.AllCommittee)
                {
                    MemberTypeID = (int)MemberTypes.CommitteeChair;
                    MemberTypeSecondaryID = (int)MemberTypes.CommitteeMember;
                }
                else
                {
                    MemberTypeID = (int)type;
                    MemberTypeSecondaryID = (int)type;
                }
                
                IQuery query = NHibernateSessionManager.Instance.GetSession().CreateQuery(NHQueries.GetAllByMemberType)
                            .SetInt32("PositionID", associatedPosition.ID)
                            .SetInt32("MemberTypeID", MemberTypeID)
                            .SetInt32("MemberTypeSecondaryID", MemberTypeSecondaryID);

                return query.List<CommitteeMember>() as List<CommitteeMember>;
            }
        }

        public class UserDao : AbstractNHibernateDao<User, int>, IUserDao {
            public User GetUserByLogin(string LoginID)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Login))
                    .Add(Expression.Eq("id", LoginID));

                Login login = criteria.UniqueResult<Login>();

                if (login == null)
                    return null;

                return login.User;
            }
        }

        public class FileTypeDao : AbstractNHibernateDao<FileType, int>, IFileTypeDao { }

        public class MemberTypeDao : AbstractNHibernateDao<MemberType, int>, IMemberTypeDao { }

        public class ChangeTrackingDao : AbstractNHibernateDao<ChangeTracking, int>, IChangeTrackingDao { }

        public class ChangeTypeDao : AbstractNHibernateDao<ChangeType, int>, IChangeTypeDao { }

        public class ReferSourceDao : AbstractNHibernateDao<ReferSource, int>, IReferSourceDao { }

        public class ThemeDao : AbstractNHibernateDao<Theme, int>, IThemeDao { }

        public class UnitDao : AbstractNHibernateDao<Unit, string>, IUnitDao { }
        #endregion
          
    }
}
