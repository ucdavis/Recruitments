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
        ICurrentPositionDao GetCurrentPositionDao();
        ICommitteeMemberDao GetCommitteeMemberDao();
        IUserDao GetUserDao();
        IFileTypeDao GetFileTypeDao();
        IMemberTypeDao GetMemberTypeDao();
        IChangeTrackingDao GetChangeTrackingDao();
        IChangeTypeDao GetChangeTypeDao();
        IThemeDao GetThemeDao();
        IUnitDao GetUnitDao();
        IReferenceDao GetReferenceDao();
        ITemplateTypeDao GetTemplateTypeDao();
        ITemplateDao GetTemplateDao();
    }

    // There's no need to declare each of the DAO interfaces in its own file, so just add them inline here.
    // But you're certainly welcome to put each declaration into its own file.
    #region Inline interface declarations

    public interface IPositionDao : IDao<Position, int> {
        List<Position> GetAllPositionsByStatus(bool Closed, bool AdminAccepted);
        List<Position> GetAllPositionsByStatus(bool Closed);
    }

    public interface IApplicationDao : IDao<Application, int> {
        List<Application> GetApplicationsByApplicant(Profile applicantProfile);
        List<Application> GetApplicationsByPosition(Position position);
    }

    public interface IApplicantDao : IDao<Applicant, int> {
        Applicant GetApplicantByEmail(string email);
    }

    public interface IProfileDao : IDao<Profile, int> { }

    public interface IDepartmentDao : IDao<Department, int> { }

    public interface ISurveyDao : IDao<Survey, int> { }
    
    public interface IRecruitmentSrcDao : IDao<RecruitmentSrc, int> { }
          
    public interface IEthnicityDao : IDao<Ethnicity, int> { }

    public interface IGenderDao : IDao<Gender, int> { }

    public interface IFileDao : IDao<File, int> { }
          
    public interface IEducationDao : IDao<Education, int> { }

    public interface ICurrentPositionDao : IDao<CurrentPosition, int> { }
    
    public interface ICommitteeMemberDao : IDao<CommitteeMember, int> {
        List<CommitteeMember> GetAllByMemberType(Position associatedPosition, MemberTypes type);
    }
     
    public interface IUserDao : IDao<User, int> {
        User GetUserByLogin(string LoginID);
    }

    public interface IFileTypeDao : IDao<FileType, int> {
        FileType GetFileTypeByName(string fileTypeName);
    }
          
    public interface IMemberTypeDao : IDao<MemberType, int> { }
    
    public interface IChangeTrackingDao : IDao<ChangeTracking, int> { }
          
    public interface IChangeTypeDao : IDao<ChangeType, int> { }
          
    public interface IThemeDao : IDao<Theme, int> { }

    public interface IUnitDao : IDao<Unit, string> { }

    public interface IReferenceDao : IDao<Reference, int> { }

    public interface ITemplateTypeDao : IDao<TemplateType, int> { }

    public interface ITemplateDao : IDao<Template, int> {
        List<Template> GetTemplatesByType(TemplateType type);
    }

    #endregion
}
