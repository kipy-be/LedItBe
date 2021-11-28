using LedItBe.Core.Api;
using LedItBe.Core.Api.Base;
using System.Net;
using System.Threading.Tasks;

namespace LedItBe.Core.Devices
{
    public class Device
    {
        private DeviceApiService _apiService;

        public DeviceInfo Infos { get; private set; }
        public IPAddress Ip { get; private set; }
        public string Name { get; private set; }

        public Device (string name, IPAddress ip, DeviceInfo infos)
        {
            Name = name;
            Ip = ip;
            Infos = infos;

            _apiService = new DeviceApiService (new ApiConfig($"http://{ip}/xled/v1"));
        }

        public async Task<bool> GetInfos()
        {
            var callResult = await _apiService.GetDeviceInfo();

            if (callResult.IsSuccess)
            {
                Infos = new DeviceInfo(callResult.Result);
                return true;
            }

            return false;
        }
    }
}
