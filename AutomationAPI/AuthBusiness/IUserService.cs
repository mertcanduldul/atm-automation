namespace AutomationAPI.AuthBusiness
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);
    }
}
