
using System.Text;
using Microsoft.AspNetCore.Mvc;
using repo;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }
    }
}