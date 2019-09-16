using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyWebApp.Domain.Whiteboards;
// ReSharper disable CheckNamespace

namespace MyWebApp.Hubs
{
    public class WhiteboardHub : Hub
    {
        ////suggest use buffer version: DrawLines!
        //public Task DrawLine(DrawLineDto dto)
        //{
        //    return Clients.Others.SendAsync("DrawLine", dto);
        //}

        //public Task DrawLines(DrawLinesDto dto)
        //{
        //    return Clients.Others.SendAsync("DrawLines", dto);
        //}

        public Task DrawLines(DrawLineDto[] dtos)
        {
            return Clients.Others.SendAsync("DrawLines", dtos);
        }
    }
}
