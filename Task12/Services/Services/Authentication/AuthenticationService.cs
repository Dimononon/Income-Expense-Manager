using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task12;
using Task12.Models;

namespace Services.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDBContext _dbContext;
        public AuthenticationService(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }
        public async Task RegistrateUser(UserAccount user)
        {
            _dbContext.UserAccounts.Add(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<UserAccount> GetUser(Guid id)
        {
            var user = await _dbContext.UserAccounts.FindAsync(id);
            return user;
        }
        public async Task<UserAccount> GetUser(string username)
        {
            var user = _dbContext.UserAccounts.Where(p => p.UserName==username);
            return user.First();
        }
        public async Task UpdateUser(Guid id, UserAccount user)
        {
            var existedInstanse = await _dbContext.UserAccounts.FindAsync(id);
            if (existedInstanse != null)
            {
                _dbContext.Entry(existedInstanse).CurrentValues.SetValues(user);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                await RegistrateUser(user);
            }
        }
    }
}
