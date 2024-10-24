
using Microsoft.AspNetCore.Mvc;
using data.account;
using repo;
using services.account;
using api.Filters;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountHandler _accountHandler;
        public AccountController(IConfiguration config, IAccountHandler acctHdlr)
        {
            _config = config;
            _accountHandler = acctHdlr;
        }

        [HttpPost("auth0-signup")]
        [Auth0IPFilter] 
        public async Task<IActionResult> Auth0Signup(Auth0SignupDTO newUser)
        {
            // create user
            await _accountHandler.Signup(newUser.Username, newUser.Auth0Id);
            return Ok("Successfully created account.");
        }

        [HttpGet("users")]
        [Authorize(Auth0Permission.ReadAllUsers)]
        public async Task<IActionResult> RetrieveAccounts(int p = 1, int c = 10, string? filter = null)
        {
            // retrieve accounts
            var accounts = await _accountHandler.GetAccounts(p, c, filter);

            return Ok(accounts);
        }
    }
}