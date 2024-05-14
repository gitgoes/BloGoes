using Asp.Versioning;
using AutoMapper;
using BloGoes.Domain.DTO;
using BloGoes.Domain.Entity;
using BloGoes.Domain.Util;
using BloGoes.Services;
using BloGoes.Services.Mappings;
using BloGoes.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BloGoes.API.Controllers.V1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserBlogController : ControllerBase
    {
        private readonly IBlogUserServices _blogUserServices;
        private IMapper mapper;
        public UserBlogController(IBlogUserServices blogUserServices)
        {
            _blogUserServices = blogUserServices;
            mapper = new MapperConfiguration(c => { c.AddProfile<BlogUserProfileMap>(); }).CreateMapper();
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BlogUserRequestDTO request)
        {

            try
            {

                var entity = mapper.Map<BlogUserEntity>(request);
                entity.Password = Security.Encrypt(request.Password, true);

                await _blogUserServices.Add(entity);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
