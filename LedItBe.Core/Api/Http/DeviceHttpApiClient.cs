using LedItBe.Core.Dto;
using System.Net;
using System.Threading.Tasks;

namespace LedItBe.Core.Api.Http
{
    internal class DeviceHttpApiClient : HttpApiClient
    {
        public DeviceHttpApiClient(IPAddress ip)
            : base($"http://{ip}/xled/v1")
        {}

        public Task<HttpApiResponse<DeviceInfoDto>> GetDeviceInfo()
            => Get<DeviceInfoDto> ("gestalt");

        public Task<HttpApiResponse<LoginResponseDto>> Login(LoginRequestDto dto)
            => PostJson<LoginResponseDto>("login", null, null, dto);
    }
}
