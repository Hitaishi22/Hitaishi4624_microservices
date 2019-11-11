namespace TransactionAPI.Services
{
    using TransactionAPI.Models;

    public interface IIdentityService
    {
        IdentityModel GetIdentity();
    }
}
