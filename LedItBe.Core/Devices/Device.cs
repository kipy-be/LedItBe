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
        private string _sessionToken;
        private DateTime _sessionExpirationDate;

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

        public async Task<bool> Connect()
        {
            var loginDto = new LoginRequestDto { Challenge = StringUtils.Base64Encode(StringUtils.GenerateAsciiString(32)) };
            var loginResult = await _httpApiClient.Login(loginDto);

            if (!loginResult.IsSuccess || loginResult.Result.Code != DeviceHttpApiClient.OK_RESPONSE_CODE)
            {
                return false;
            }

            SetSession(loginResult.Result.Token, loginResult.Result.Expiration);

            var verifyDto = new LoginVerifyDto { ChallengeResponse = loginResult.Result.ChallengeResponse };
            var verifyResult = await _httpApiClient.Verify(verifyDto);

            if (!verifyResult.IsSuccess || verifyResult.Result.Code != DeviceHttpApiClient.OK_RESPONSE_CODE)
            {
                ResetSession();
                return false;
            }

            return true;
        }

        private void SetSession(string token, int expiration)
        {
            _sessionToken = token;
            _sessionExpirationDate = DateTime.UtcNow.AddSeconds(expiration);
            _httpApiClient.SetSessionInfo(token);
            _udpClient.SetSessionInfo(token);
        }

        private void ResetSession()
        {
            _sessionToken = null;
            _sessionExpirationDate = DateTime.MinValue;
            _httpApiClient.SetSessionInfo(null);
            _udpClient.SetSessionInfo(null);
        }
    }
}
