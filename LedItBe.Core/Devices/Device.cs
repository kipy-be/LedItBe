using LedItBe.Core.Api.Http;
using LedItBe.Core.Api.Udp;
using LedItBe.Core.Dto;
using LedItBe.Core.Utils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LedItBe.Core.Devices
{
    public class Device
    {
        private DeviceHttpApiClient _httpApiClient;
        private DeviceUdpApiClient _udpClient;
        private DateTime _sessionExpirationDate;

        public DeviceInfo Infos { get; private set; }
        public IPAddress Ip { get; private set; }
        public string Name { get; private set; }
        public LedOperationMode LedOperationMode { get; private set; } = LedOperationMode.Unknown;

        public Device (string name, IPAddress ip, DeviceInfo infos)
        {
            Name = name;
            Ip = ip;
            Infos = infos;

            _httpApiClient = new DeviceHttpApiClient(ip);
            _udpClient = new DeviceUdpApiClient(ip);
        }

        private bool IsResultSuccess<T>(HttpApiResponse<T> response) where T : ReponseBaseDto
            => response.IsSuccess && response.Result.Code == DeviceHttpApiClient.OK_RESPONSE_CODE;

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

        public async Task<bool> Connect()
        {
            var loginDto = new LoginRequestDto { Challenge = StringUtils.Base64Encode(StringUtils.GenerateAsciiString(32)) };
            var loginResult = await _httpApiClient.Login(loginDto);

            if (!IsResultSuccess(loginResult))
            {
                return false;
            }

            SetSession(loginResult.Result.Token, loginResult.Result.Expiration);

            var verifyDto = new LoginVerifyDto { ChallengeResponse = loginResult.Result.ChallengeResponse };
            var verifyResult = await _httpApiClient.Verify(verifyDto);

            if (!IsResultSuccess(verifyResult))
            {
                ResetSession();
                return false;
            }

            await GetMode();

            return true;
        }

        public async Task<bool> TurnOff() => await SetMode(LedOperationMode.Off);
        public async Task<bool> ToStaticColorMode() => await SetMode(LedOperationMode.Color);

        private async Task<bool> GetMode()
        {
            var result = await _httpApiClient.GetLedOperationMode();

            if (!IsResultSuccess(result))
            {
                return false;
            }

            LedOperationMode = LedOperationModes.GetModeFromString(result.Result.Mode);

            return true;
        }

        private async Task<bool> SetMode(LedOperationMode mode)
        {
            var dto = new SetLedOperationModeDto { Mode = LedOperationModes.GetStringFromMode(mode) };
            var result = await _httpApiClient.SetLedOperationMode(dto);

            if (!IsResultSuccess(result))
            {
                return false;
            }

            return true;
        }

        private void SetSession(string token, int expiration)
        {
            _sessionExpirationDate = DateTime.UtcNow.AddSeconds(expiration);
            _httpApiClient.SetSessionInfo(token);
            _udpClient.SetSessionInfo(token);
        }

        private void ResetSession()
        {
            _sessionExpirationDate = DateTime.MinValue;
            _httpApiClient.SetSessionInfo(null);
            _udpClient.SetSessionInfo(null);
        }
    }
}
