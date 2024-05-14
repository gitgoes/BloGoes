using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloGoes.Services.Services.Interfaces
{
    public interface IWebSocketServer
    {
        public Task StartAsync(string prefix);
        public Task SendNotificationAsync(string message);
    }
}
