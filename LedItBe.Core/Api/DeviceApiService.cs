using LedItBe.Core.Api.Base;
using LedItBe.Core.Dto;
using System.Threading.Tasks;

namespace LedItBe.Core.Api
{
    internal class DeviceApiService : ApiService
    {
        public DeviceApiService(ApiConfig apiConfig)
            : base(apiConfig)
        {}

        public Task<ApiResponse<DeviceInfoDto>> GetDeviceInfo()
            => Get<DeviceInfoDto> ("gestalt");
    }
}
