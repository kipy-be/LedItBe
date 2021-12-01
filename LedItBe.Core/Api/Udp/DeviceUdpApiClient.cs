using LedItBe.Core.Common;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace LedItBe.Core.Api.Udp
{
    internal class DeviceUdpApiClient
    {
        private const ushort PORT = 7777;
        private const byte PACKET_FORMAT_VERSION = 0x03;

        private IPEndPoint _deviceEp;
        private UdpClient _udpClient;

        private byte[] _sessionToken;

        public DeviceUdpApiClient(IPAddress ip)
        {
            _udpClient = new UdpClient();
            _deviceEp = new IPEndPoint(ip, PORT);
        }

        public void SetSessionInfo(string token)
        {
            _sessionToken = Convert.FromBase64String(token);
        }

        public bool SendFrame(Frame frame)
        {
            try
            {
                var fragments = frame.ToFragments();
                byte[] packet;
                byte packetNum = 0;

                foreach (var f in fragments)
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.WriteByte(PACKET_FORMAT_VERSION);
                        ms.Write(_sessionToken);
                        ms.WriteByte(0x00);
                        ms.WriteByte(0x00);
                        ms.WriteByte(packetNum);
                        ms.Write(f);
                        packet = ms.ToArray();

                        packetNum++;
                    }

                    _udpClient.Send(packet, packet.Length, _deviceEp);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
