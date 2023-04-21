using Task12.Models;

namespace Services.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserAccount> GetUser(Guid id);
        Task<UserAccount> GetUser(string username);
        Task RegistrateUser(UserAccount user);
        Task UpdateUser(Guid id, UserAccount user);

    }
}