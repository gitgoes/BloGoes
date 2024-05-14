using Asp.Versioning;
using BloGoes.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloGoes.API.Controllers.V1
{

    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class NotificationController : ControllerBase
    {
        private readonly IWebSocketServer _webSocketServer;
        public NotificationController(IWebSocketServer webSocketServer)
        {
            _webSocketServer = webSocketServer;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] string message)
        {

            try
            {
                _webSocketServer.SendNotificationAsync(message);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
