using BloGoes.Services.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BloGoes.Services
{
    public class WebSocketServer : IWebSocketServer
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> _clients = new ConcurrentDictionary<Guid, WebSocket>();
        private HttpListener _listener;

        public async Task StartAsync(WebSocket webSocket)
        {


                try
                {
                        
                    var clientId = Guid.NewGuid();
                    _clients.TryAdd(clientId, webSocket);
                    Console.WriteLine($"Client {clientId} connected.");

                    _ = Task.Run(() => HandleClientAsync(clientId, webSocket));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            
        }

        private async Task HandleClientAsync(Guid clientId, WebSocket clientSocket)
        {
            var buffer = new byte[1024 * 4];

            try
            {
                while (clientSocket.State == WebSocketState.Open)
                {
                    var result = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await clientSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        _clients.TryRemove(clientId, out _);
                        Console.WriteLine($"Client {clientId} disconnected.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        public async Task SendNotificationAsync(string message)
        {
            foreach (var clientSocket in _clients.Values)
            {
                if (clientSocket.State == WebSocketState.Open)
                {
                    var buffer = Encoding.UTF8.GetBytes(message);
                    await clientSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
