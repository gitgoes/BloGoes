using Asp.Versioning;
using BloGoes.Services.JwtServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloGoes.API.Controllers.V1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtUtils _jwt;
        public AuthorizationController(IJwtUtils jwtUtils)
        {
            _jwt = jwtUtils;
        }

        [HttpPost("ValidateJwtToken")]
        [AllowAnonymous]
        public IActionResult ValidateJwtToken(string? token)
        {

            try
            {
                return Ok(_jwt.ValidateJwtToken(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
