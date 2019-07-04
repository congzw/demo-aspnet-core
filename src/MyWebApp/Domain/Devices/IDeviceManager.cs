using System.Collections.Generic;

namespace MyWebApp.Domain.Devices
{
    public interface IDeviceManager
    {
        IList<Device> GetDevices();
        void SetOnline(string connectionId, Device device);
        void SetOffline(string connectionId, Device device);
        void RemoveConnection(string connectionId);
    }
}
