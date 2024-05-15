using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Services.Services.Interfaces
{
    public interface IWebSocketServer
    {
        public Task StartAsync(WebSocket webSocket);
        public Task SendNotificationAsync(string message);
    }
}
