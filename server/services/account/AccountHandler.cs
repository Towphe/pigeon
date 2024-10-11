using Microsoft.EntityFrameworkCore;
using repo;

namespace services.account
{
    public interface IAccountHandler
    {
        public Task Signup(string username, string auth0Id);
        public Task<bool> IsUsernameTaken(string username);
        public Task<Account?> GetAccount(string username);
        public Task<List<Account>> GetAccounts(int p = 0, int c = 0, string? filter = null);
    }

    public class AccountHandler : IAccountHandler
    {
        private readonly PigeonContext _dbContext;

        public AccountHandler(PigeonContext dbCtx)
        {
            _dbContext = dbCtx;
        }

        public async Task Signup(string username, string auth0Id)
        {
            // create new `Account` object
            var newAccount = new Account();
            newAccount.Username = username;
            newAccount.UserType = "USER";
            newAccount.Auth0Id = auth0Id;

            // save new account
            await _dbContext.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();

            return;
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            var user = await _dbContext.Accounts.FirstAsync(a => a.UserType == "USER" && a.Username == username);

            if (user == null)
            {
                // no account with same username found
                return false;
            }
            return true;
        }

        public async Task<Account?> GetAccount(string username)
        {
            var user = await _dbContext.Accounts.FirstAsync(a => a.UserType == "USER" && a.Username == username);

            if (user == null)
            {
                // no account with same username found
                return null;
            }
            return user;
        }

        public async Task<List<Account>> GetAccounts(int p = 0, int c = 0, string? filter = null)
        {
            List<Account>? accounts = null;

            if (filter != null)
            {
                accounts = await _dbContext.Accounts.Where(a => a.Username == filter).Skip(c*p).Take(c).ToListAsync();
            }
            else
            {
                accounts = await _dbContext.Accounts.Skip(c*p).Take(c).ToListAsync();
            }
            

            return accounts;
        }


    }
}