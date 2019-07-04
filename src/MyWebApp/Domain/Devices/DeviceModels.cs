namespace MyWebApp.Domain.Devices
{
    public class Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        //public string DeviceTypeCode { get; set; } //todo
        public string DeviceStateCode { get; set; }
    }

    public class DeviceStateCodes
    {
        public static string Online = "Online";
        public static string Offline = "Offline";
    }

    public class DeviceTypeCodes
    {
        public static string Client = "Client";
        public static string Pod = "Pod";
        public static string Server = "Server";

        //public static string Client_Android = "Client_Android";
        //public static string Client_IOS = "Client_IOS";
        //public static string Client_PC = "Client_PC";
    }
    //public class DeviceState
    //{
    //    public string Id { get; set; }
    //    public string DeviceStateCode { get; set; }
    //}

    public class DeviceUseConnection
    {
        public string DeviceId { get; set; }
        public string ConnectionId { get; set; }
    }
}
