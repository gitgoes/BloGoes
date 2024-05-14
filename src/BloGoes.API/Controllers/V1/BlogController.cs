using Asp.Versioning;
using AutoMapper;
using BloGoes.Domain.DTO;
using BloGoes.Domain.Entity;
using BloGoes.Services;
using BloGoes.Services.Mappings;
using BloGoes.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloGoes.API.Controllers.V1
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogServices _blogServices;
        private IMapper mapper;
        private readonly IWebSocketServer _websocketServer;
        public BlogController(IBlogServices blogServices, IWebSocketServer websocketServer)
        {
            _blogServices = blogServices;
            mapper = new MapperConfiguration(c => { c.AddProfile<BlogProfileMap>(); }).CreateMapper();
            _websocketServer = websocketServer;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BlogDTO request) {
           
            try 
            {
                var entity = mapper.Map<BlogEntity>(request);

                await _blogServices.Add(entity);

                await _websocketServer.SendNotificationAsync("Nova postagem: " + request.Text);

                return Ok(true);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BlogUpdateDTO request)
        {
            try
            {
                return Ok(await _blogServices.Update(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int entityId)
        {
            try
            {
                return Ok(await _blogServices.Delete(entityId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listBlog = await _blogServices.List();
                var listBlogDTO = mapper.Map<IEnumerable<BlogDTO>>(listBlog);

                return Ok(listBlogDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByid(int entityId)
        {
            try
            {
                var blog = await _blogServices.GetById(entityId);
                var blogDTO = mapper.Map<BlogDTO>(blog);
                return Ok(blogDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
