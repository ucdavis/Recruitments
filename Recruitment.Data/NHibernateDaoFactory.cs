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

        #endregion
          
    }
}
