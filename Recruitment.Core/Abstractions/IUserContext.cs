namespace CAESDO.Recruitment.Core.Abstractions
{
    public interface IUserContext
    {
        bool IsUserInRole(string role);
        bool IsAuthenticated();
        string Name();
    }
}