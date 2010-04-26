using CAESDO.Recruitment.Core.Domain;
using System.Collections.Generic;

namespace CAESDO.Recruitment.Core.DataInterfaces
{
    /// <summary>
    /// Provides an interface for retrieving DAO objects
    /// </summary>
    public interface IDaoFactory 
    {
        IApplicationDao GetApplicationDao();
        IPositionDao GetPositionDao();
        IApplicantDao GetApplicantDao();
        IProfileDao GetProfileDao();
        IDepartmentDao GetDepartmentDao();
        ISurveyDao GetSurveyDao();
        IRecruitmentSrcDao GetRecruitmentSrcDao();
        IEthnicityDao GetEthnicityDao();
        IGenderDao GetGenderDao();
        IFileDao GetFileDao();
        IEducationDao GetEducationDao();
        ICommitteeMemberDao GetCommitteeMemberDao();
        IUserDao GetUserDao();
    }

    // There's no need to declare each of the DAO interfaces in its own file, so just add them inline here.
    // But you're certainly welcome to put each declaration into its own file.
    #region Inline interface declarations

    public interface IPositionDao : IDao<Position, int> {
        List<Position> GetAllPositionsByStatus(bool Closed, bool AdminAccepted);
        List<Position> GetAllPositionsByStatus(bool Closed);
    }

    public interface IApplicationDao : IDao<Application, int> { }

    public interface IApplicantDao : IDao<Applicant, int> { }

    public interface IProfileDao : IDao<Profile, int> { }

    public interface IDepartmentDao : IDao<Department, int> { }

    public interface ISurveyDao : IDao<Survey, int> { }
    
    public interface IRecruitmentSrcDao : IDao<RecruitmentSrc, int> { }
          
    public interface IEthnicityDao : IDao<Ethnicity, int> { }

    public interface IGenderDao : IDao<Gender, int> { }

    public interface IFileDao : IDao<File, int> { }
          
    public interface IEducationDao : IDao<Education, int> { }
    
    public interface ICommitteeMemberDao : IDao<CommitteeMember, int> {
        List<CommitteeMember> GetAllByMemberType(Position associatedPosition, MemberTypes type);
    }
     
    public interface IUserDao : IDao<User, int> { }
    
    #endregion
}
