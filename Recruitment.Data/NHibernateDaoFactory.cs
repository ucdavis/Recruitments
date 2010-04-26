using CAESDO.Recruitment.Core.DataInterfaces;
using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;
using System.ComponentModel;
using System.Web;
using System.Web.Security;

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

        public IDepartmentMemberDao GetDepartmentMemberDao()
        {
            return new DepartmentMemberDao();
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
            public List<Position> GetAllPositionsByStatus(bool Closed, bool AdminAccepted)
            {
                User currentUser = new UserDao().GetUserByLogin(HttpContext.Current.User.Identity.Name);
                                                
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Position))
                    .Add(Expression.Eq("Closed", Closed))
                    .Add(Expression.Eq("AdminAccepted", AdminAccepted));

                if (currentUser != null) //only filter logged in users
                {
                    if (!Roles.IsUserInRole("Admin"))
                    {
                        List<string> deptFIS = new List<string>();

                        foreach (Unit u in currentUser.Units)
                        {
                            deptFIS.Add(u.FISCode);
                        }

                        criteria.CreateCriteria("Departments")
                                .Add(Expression.In("DepartmentFIS", deptFIS.ToArray()));
                    }
                }
                return criteria.List<Position>() as List<Position>;
            }

            public List<Position> GetAllPositionsByStatusForCommittee(bool Closed, bool AdminAccepted)
            {
                User currentUser = new UserDao().GetUserByLogin(HttpContext.Current.User.Identity.Name);

                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Position))
                    .Add(Expression.Eq("Closed", Closed))
                    .Add(Expression.Eq("AdminAccepted", AdminAccepted))
                    .CreateCriteria("PositionCommittee")
                    .Add(Expression.Eq("LoginID", HttpContext.Current.User.Identity.Name));

                return criteria.List<Position>() as List<Position>;
            }

            public bool VerifyPositionAccess(Position position)
            {
                User currentUser = new UserDao().GetUserByLogin(HttpContext.Current.User.Identity.Name);

                if (currentUser == null)
                    return false;
                else
                {
                    List<string> deptFIS = new List<string>();

                    foreach (Unit u in currentUser.Units)
                    {
                        deptFIS.Add(u.FISCode);
                    }

                    ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(Position))
                        .Add(Expression.IdEq(position.ID))
                        .CreateCriteria("Departments")
                        .Add(Expression.In("DepartmentFIS", deptFIS.ToArray()));

                    if (criteria.List<Position>().Count == 0)
                        return false;
                    else
                        return true;
                }
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

        public class DepartmentMemberDao : AbstractNHibernateDao<DepartmentMember, int>, IDepartmentMemberDao {
            public List<DepartmentMember> GetMembersByDepartmentAndType(string[] DepartmentFIS, MemberTypes type)
            {
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

                MemberTypeDao mdao = new MemberTypeDao();

                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(DepartmentMember))
                    .Add(Expression.In("DepartmentFIS", DepartmentFIS))
                    .Add(Expression.Or(Expression.Eq("MemberType", mdao.GetById(MemberTypeID,false)), Expression.Eq("MemberType", mdao.GetById(MemberTypeSecondaryID, false))));

                return criteria.List<DepartmentMember>() as List<DepartmentMember>;
            }

            [DataObjectMethod(DataObjectMethodType.Select, true)]
            public List<DepartmentMember> GetMembersByDepartmentAndType(string DepartmentFIS, MemberTypes type)
            {
                List<string> depts = new List<string>();
                depts.Add(DepartmentFIS);

                return GetMembersByDepartmentAndType(depts.ToArray(), type);
            }

            public bool IsUserMember(MemberTypes type)
            {
                ICriteria criteria = NHibernateSessionManager.Instance.GetSession().CreateCriteria(typeof(DepartmentMember))
                    .Add(Expression.Eq("LoginID", HttpContext.Current.User.Identity.Name));

                List<DepartmentMember> members = criteria.List<DepartmentMember>() as List<DepartmentMember>;

                foreach (DepartmentMember member in members)
                {
                    if (member.MemberType.ID == (int)type)
                    {
                        return true;
                    }
                    else if (type == MemberTypes.AllCommittee)
                    {
                        if (member.MemberType.ID == (int)MemberTypes.CommitteeChair)
                            return true;
                        else if (member.MemberType.ID == (int)MemberTypes.CommitteeMember)
                            return true;
                    }
                }

                //No match found
                return false;
            }
        }
         
        #endregion
          
    }
}
