using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MyWebApp.Domain.Devices;

namespace MyWebApp.Hubs
{
    public class DeviceHub : Hub
    {
        private readonly IDeviceManager _deviceManager;

        public DeviceHub(IDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        public async Task Online(Device device)
        {
            _deviceManager.SetOnline(Context.ConnectionId, device);
            var devices = _deviceManager.GetDevices();
            await Clients.All.SendAsync("UpdateDeviceStates", devices, device);
        }

        public async Task Offline(Device device)
        {
            _deviceManager.SetOffline(Context.ConnectionId, device);
            var devices = _deviceManager.GetDevices();
            await Clients.All.SendAsync("UpdateDeviceStates", devices, device);
        }
        
        public override async Task OnConnectedAsync()
        {
            var devices = _deviceManager.GetDevices();
            await Clients.All.SendAsync("UpdateDeviceStates", devices, null);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _deviceManager.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
