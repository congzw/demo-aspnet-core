using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Domain.Devices
{
    public class DeviceManager : IDeviceManager
    {
        public DeviceManager()
        {
            Devices  = new ConcurrentDictionary<string, Device>(StringComparer.OrdinalIgnoreCase);
            Connections = new ConcurrentDictionary<string, Device>(StringComparer.OrdinalIgnoreCase);
        }

        public IDictionary<string, Device> Devices { get; set; }
        public IDictionary<string, Device> Connections { get; set; }

        public IList<Device> GetDevices()
        {
            return Devices.Values.ToList();
        }

        public void SetOnline(string connectionId, Device device)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            device.DeviceStateCode = DeviceStateCodes.Online;
            Connections[connectionId] = device;
            Devices[device.Id] = device;
        }

        public void SetOffline(string connectionId, Device device)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            var theDevice = !Devices.ContainsKey(device.Id) ? null : Devices[device.Id];
            if (theDevice != null)
            {
                theDevice.DeviceStateCode = DeviceStateCodes.Offline;
            }

            Connections[connectionId] = theDevice;
        }

        public void RemoveConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var theConnection = !Connections.ContainsKey(connectionId) ? null : Connections[connectionId];
            if (theConnection == null)
            {
                return;
            }

            theConnection.DeviceStateCode = DeviceStateCodes.Offline;
            Connections.Remove(connectionId);
        }
    }
}