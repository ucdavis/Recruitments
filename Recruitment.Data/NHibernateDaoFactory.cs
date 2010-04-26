using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using System.ComponentModel;

namespace CAESDO.Recruitment.Data
{
    /// <summary>
    /// Exposes access to NHibernate DAO classes.  Motivation for this DAO
    /// framework can be found at http://www.hibernate.org/328.html.
    /// </summary>
    public class NHibernateDaoFactory : IDaoFactory
    {
        #region Dao Retrieval Operations

        public IGenericDao<T, IdT> GetGenericDao<T, IdT>()
        {
            return new GenericDao<T, IdT>();
        }

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

        public ICurrentPositionDao GetCurrentPositionDao()
        {
            return new CurrentPositionDao();
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

        public IThemeDao GetThemeDao()
        {
            return new ThemeDao();
        }

        public IUnitDao GetUnitDao()
        {
            return new UnitDao();
        }

        public IReferenceDao GetReferenceDao()
        {
            return new ReferenceDao();
        }

        public ITemplateTypeDao GetTemplateTypeDao()
        {
            return new TemplateTypeDao();
        }

        public ITemplateDao GetTemplateDao()
        {
            return new TemplateDao();
        }

        #endregion

        #region Inline DAO implementations

        public class GenericDao<T, IdT> : AbstractNHibernateDao<T, IdT>, IGenericDao<T, IdT> { }

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

        [DataObject]
        public class ApplicationDao : AbstractNHibernateDao<Application, int>, IApplicationDao {
            public List<Application> GetApplicationsByApplicant(Profile applicantProfile)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Application))
                    .Add(Expression.Eq("AssociatedProfile", applicantProfile));
                    
                return criteria.List<Application>() as List<Application>;
            }

            public List<Application> GetApplicationsByApplicant(Profile applicantProfile, bool submitted)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Application))
                    .Add(Expression.Eq("Submitted", submitted))
                    .Add(Expression.Eq("AssociatedProfile", applicantProfile));

                return criteria.List<Application>() as List<Application>;
            }

            [DataObjectMethod(DataObjectMethodType.Select)]
            public List<Application> GetApplicationsByPosition(Position position)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Application))
                    .Add(Expression.Eq("AppliedPosition", position))
                    .CreateCriteria("AssociatedProfile")
                    .AddOrder(Order.Desc("LastName"));

                return criteria.List<Application>() as List<Application>;
            }

            [DataObjectMethod(DataObjectMethodType.Select)]
            public List<Application> GetApplicationsByPosition(int positionID)
            {
                Position position = new PositionDao().GetById(positionID, false);

                return GetApplicationsByPosition(position);
            }
        }

        public class ApplicantDao : AbstractNHibernateDao<Applicant, int>, IApplicantDao {
            public Applicant GetApplicantByEmail(string email)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Applicant))
                    .Add(Expression.Eq("Email", email));

                return criteria.UniqueResult<Applicant>();
            }
        }

        public class ProfileDao : AbstractNHibernateDao<Profile, int>, IProfileDao { }

        public class DepartmentDao : AbstractNHibernateDao<Department, int>, IDepartmentDao { }

        public class SurveyDao : AbstractNHibernateDao<Survey, int>, ISurveyDao { }

        public class RecruitmentSrcDao : AbstractNHibernateDao<RecruitmentSrc, int>, IRecruitmentSrcDao { }

        public class EthnicityDao : AbstractNHibernateDao<Ethnicity, int>, IEthnicityDao { }

        public class GenderDao : AbstractNHibernateDao<Gender, int>, IGenderDao { }

        public class FileDao : AbstractNHibernateDao<File, int>, IFileDao { }

        public class EducationDao : AbstractNHibernateDao<Education, int>, IEducationDao { }

        public class CurrentPositionDao : AbstractNHibernateDao<CurrentPosition, int>, ICurrentPositionDao { }

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

            public User GetUserBySID(string SID)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(User))
                    .Add(Expression.Eq("SID", SID));

                return criteria.UniqueResult<User>();
            }
        }

        public class FileTypeDao : AbstractNHibernateDao<FileType, int>, IFileTypeDao {
            public FileType GetFileTypeByName(string fileTypeName)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(FileType))
                    .Add(Expression.Eq("FileTypeName", fileTypeName));

                return criteria.UniqueResult<FileType>();
            }

            public List<FileType> GetAllByApplicationFileType(bool applicationFileType, string propertyName, bool ascending)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(FileType))
                    .Add(Expression.Eq("ApplicationFile", applicationFileType))
                    .AddOrder(new Order(propertyName, ascending));

                return criteria.List<FileType>() as List<FileType>;
            }
        }

        public class MemberTypeDao : AbstractNHibernateDao<MemberType, int>, IMemberTypeDao { }

        public class ChangeTrackingDao : AbstractNHibernateDao<ChangeTracking, int>, IChangeTrackingDao { }

        public class ChangeTypeDao : AbstractNHibernateDao<ChangeType, int>, IChangeTypeDao { }

        public class ThemeDao : AbstractNHibernateDao<Theme, string>, IThemeDao { }

        public class UnitDao : AbstractNHibernateDao<Unit, string>, IUnitDao { }

        public class ReferenceDao : AbstractNHibernateDao<Reference, int>, IReferenceDao {
            public Reference GetReferenceByUploadID(string UploadID)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Reference))
                    .Add(Expression.Eq("UploadID", UploadID));

                return criteria.UniqueResult<Reference>();
            }
        }

        public class TemplateTypeDao : AbstractNHibernateDao<TemplateType, int>, ITemplateTypeDao { }

        public class TemplateDao : AbstractNHibernateDao<Template, int>, ITemplateDao {
            public List<Template> GetTemplatesByType(TemplateType type)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Template))
                    .Add(Expression.Eq("TemplateType", type));

                return criteria.List<Template>() as List<Template>;
            }
        }
         
        #endregion
          
    }
}
