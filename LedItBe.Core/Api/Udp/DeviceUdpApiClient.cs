using System.Net;

namespace LedItBe.Core.Api.Udp
{
    internal class DeviceUdpApiClient
    {
        private const ushort PORT = 7777;

        private IPEndPoint _deviceEp;
        private string _sessionToken;

        public DeviceUdpApiClient(IPAddress ip)
        {
            _deviceEp = new IPEndPoint(ip, PORT);
        }

        public void SetSessionInfo(string token)
        {
            _sessionToken = token;
        }
    }
}
