using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Core.DataInterfaces
{
    /// <summary>
    /// Provides an interface for retrieving DAO objects
    /// </summary>
    public interface IDaoFactory 
    {
        IApplicationsDao GetApplicationsDao();
        IPositionsDao GetPositionsDao();

    }

    // There's no need to declare each of the DAO interfaces in its own file, so just add them inline here.
    // But you're certainly welcome to put each declaration into its own file.
    #region Inline interface declarations

    public interface IPositionsDao : IDao<Positions, int> { }

    public interface IApplicationsDao : IDao<Applications, int> { }

    #endregion
}
