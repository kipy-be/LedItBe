using LedItBe.Core.Dto;
using System.Net;
using System.Threading.Tasks;

namespace LedItBe.Core.Api.Http
{
    internal class DeviceHttpApiClient : HttpApiClient
    {
        public const int OK_RESPONSE_CODE = 1000;

        public DeviceHttpApiClient(IPAddress ip)
            : base($"http://{ip}/xled/v1")
        {}

        public Task<HttpApiResponse<DeviceInfoDto>> GetDeviceInfo()
            => Get<DeviceInfoDto> ("gestalt");

        public Task<HttpApiResponse<LoginResponseDto>> Login(LoginRequestDto dto)
            => PostJson<LoginResponseDto>("login", null, null, dto);

        public Task<HttpApiResponse<ReponseBaseDto>> Verify(LoginVerifyDto dto)
            => PostJson<ReponseBaseDto>("verify", null, null, dto);

        public Task<HttpApiResponse<ReponseBaseDto>> GetStatus()
            => Get<ReponseBaseDto>("status");

        public Task<HttpApiResponse<LedOperationModeDto>> GetLedOperationMode()
            => Get<LedOperationModeDto>("led/mode");

        public Task<HttpApiResponse<ReponseBaseDto>> SetLedOperationMode(SetLedOperationModeDto dto)
            => PostJson<ReponseBaseDto>("led/mode", null, null, dto);

        public Task<HttpApiResponse<LedColorDto>> GetLedColor()
            => Get<LedColorDto>("led/color");

        public Task<HttpApiResponse<ReponseBaseDto>> SetLedColorHsv(SetLedColorHsvDto dto)
            => PostJson<ReponseBaseDto>("led/color", null, null, dto);

        public Task<HttpApiResponse<ReponseBaseDto>> SetLedColorRgb(SetLedColorRgbwDto dto)
            => PostJson<ReponseBaseDto>("led/color", null, null, dto);

        public Task<HttpApiResponse<LedMovieConfigDto>> GetLedMovieConfig()
            => Get<LedMovieConfigDto>("led/movie/config");

        public Task<HttpApiResponse<ReponseBaseDto>> SetLedMovieConfig(SetLedMovieConfigDto dto)
            => PostJson<ReponseBaseDto>("led/movie/config", null, null, dto);
    }
}
