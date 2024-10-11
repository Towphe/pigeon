
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        public AuthController()
        {
        }
    }
}