using Asp.Versioning;
using AutoMapper;
using Azure.Core;
using BloGoes.Domain.Admin;
using BloGoes.Domain.Util;
using BloGoes.Services.JwtServices;
using BloGoes.Services.Mappings;
using BloGoes.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BloGoes.API.Controllers.V1
{
    [AllowAnonymous]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LoginController : ControllerBase
    {
        private readonly IBlogUserServices _blogUserServices;
        private IMapper mapper;
        private readonly IJwtUtils _jwt;

        public LoginController(IBlogUserServices blogUserServices, IJwtUtils jwtUtils)
        {
            _blogUserServices = blogUserServices;
            _jwt = jwtUtils;
            mapper = new MapperConfiguration(c => { c.AddProfile<BlogUserProfileMap>(); }).CreateMapper();
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var blog = await _blogUserServices.GetByEmail(email);

                if (blog != null) {
                    var encryptpws = Security.Encrypt(password, true);
                    if (blog.Password != encryptpws) 
                    { 
                        throw new Exception("Invalid Password");
                    }
                }

                dynamic result = new
                {
                    token = _jwt.GenerateJwtToken(new Account { Id = blog.Id })
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
