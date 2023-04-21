using BlazorClient.Models;
using Task12.API.Authentication;
using Task12.Models;

namespace BlazorClient.Services
{
    public interface IAuthenticationService
    {
        Task<UserAccount?> GetUser(Guid id);
        Task<UserAccount?> GetUser(string username);
        Task<UserResponse> RegistrateUser(UserAccount user);
        Task UpdateUser(Guid id, UserAccount user);
        Task<bool> ValidateUser(UserModel user);

    }
}