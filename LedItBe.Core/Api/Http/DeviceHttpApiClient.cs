using LedItBe.Core.Dto;
using System.Net;
using System.Threading.Tasks;

namespace LedItBe.Core.Api.Http
{
    internal class DeviceHttpApiClient : HttpApiClient
    {
        public DeviceHttpApiClient(IPAddress ip)
            : base($"http://{ip.ToString()}/xled/v1")
        {}

        public Task<HttpApiResponse<DeviceInfoDto>> GetDeviceInfo()
            => Get<DeviceInfoDto> ("gestalt");
    }
}
