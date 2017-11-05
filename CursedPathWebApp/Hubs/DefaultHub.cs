using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace CursedPathWebApp.Hubs
{
    public class DefaultHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
    }
}