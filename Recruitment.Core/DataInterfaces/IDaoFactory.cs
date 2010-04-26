using CAESDO.Recruitment.Core.Domain;

namespace CAESDO.Recruitment.Core.DataInterfaces
{
    /// <summary>
    /// Provides an interface for retrieving DAO objects
    /// </summary>
    public interface IDaoFactory 
    {
        IApplicationDao GetApplicationDao();
        IPositionDao GetPositionDao();
    }

    // There's no need to declare each of the DAO interfaces in its own file, so just add them inline here.
    // But you're certainly welcome to put each declaration into its own file.
    #region Inline interface declarations

    public interface IPositionDao : IDao<Position, int> { }

    public interface IApplicationDao : IDao<Application, int> { }

    #endregion
}
