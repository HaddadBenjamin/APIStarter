namespace WriteModel.Domain.AuthentificationContext
{
    public interface IAuthentificationContextUserProvider
    {
        AuthentificationContextUser Get(string email);
    }
}