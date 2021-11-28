using LedItBe.Core.Api.Http;
using LedItBe.Core.Api.Udp;
using System.Net;
using System.Threading.Tasks;

namespace LedItBe.Core.Devices
{
    public class Device
    {
        private DeviceHttpApiClient _httpApiClient;
        private DeviceUdpApiClient _udpClient;

        public DeviceInfo Infos { get; private set; }
        public IPAddress Ip { get; private set; }
        public string Name { get; private set; }

        public Device (string name, IPAddress ip, DeviceInfo infos)
        {
            Name = name;
            Ip = ip;
            Infos = infos;

            _httpApiClient = new DeviceHttpApiClient(ip);
            _udpClient = new DeviceUdpApiClient(ip);
        }

        public async Task<bool> GetInfos()
        {
            var callResult = await _httpApiClient.GetDeviceInfo();

            if (callResult.IsSuccess)
            {
                Infos = new DeviceInfo(callResult.Result);
                return true;
            }

            return false;
        }
    }
}
