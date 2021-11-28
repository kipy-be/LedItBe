using System.Net;

namespace LedItBe.Core.Devices
{
    public class Device
    {
        public DeviceInfo Infos { get; set; }
        public IPAddress Ip { get; set; }
        public string Name { get; set; }

        public Device (string name, IPAddress ip, DeviceInfo infos)
        {
            Name = name;
            Ip = ip;
            Infos = infos;
        }
    }
}
