using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LedItBe.Core.Devices
{
    public static class DeviceExplorer
    {
        static byte[] DISCOVER_PAYLOAD = new byte[9]
        {
            0x01, 0x64, 0x69, 0x73, 0x63, 0x6F, 0x76, 0x65, 0x72
        };

        public static event EventHandler<DeviceDetectedEventArgs> OnDeviceDetected;

        public static List<DeviceInfo> FindDevices()
        {
            var res = new List<DeviceInfo>();

            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Loopback
                    ||
                    !(
                        nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                        || nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                        || nic.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                    )
                {
                    continue;
                }

                var props = nic.GetIPProperties();
                var ipInfos = props.UnicastAddresses
                    .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                    .FirstOrDefault();

                if (ipInfos == null)
                {
                    continue;
                }

                var ip = ipInfos.Address;
                var mask = ipInfos.IPv4Mask;

                byte[] ipBytes = ip.GetAddressBytes();
                byte[] maskBytes = mask.GetAddressBytes();
                byte[] broadcastBytes = new byte[ipBytes.Length];

                for (int i = 0; i < ipBytes.Length; i++)
                {
                    broadcastBytes[i] = (byte)(ipBytes[i] | ~maskBytes[i]);
                }

                var broadcast = new IPAddress(broadcastBytes);

                DiscoverNetwork(broadcast);
            }

            return res;
        }

        private static void DiscoverNetwork(IPAddress nicBroadcast)
        {
            var udpClient = new UdpClient();
            var inEp = new IPEndPoint(IPAddress.Any, 5555);
            var broadcastEp = new IPEndPoint(nicBroadcast, 5555);

            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(inEp);
            udpClient.BeginReceive(DataReceived, udpClient);

            udpClient.Send(DISCOVER_PAYLOAD, DISCOVER_PAYLOAD.Length, broadcastEp);
        }

        private static void DataReceived(IAsyncResult ar)
        {
            if (ar.AsyncState == null)
            {
                return;
            }

            var udpClient = (UdpClient)ar.AsyncState;
            var ep = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = udpClient.EndReceive(ar, ref ep);

            udpClient.BeginReceive(DataReceived, ar.AsyncState);
        }

        private static void RaiseOnDeviceDetectedEvent(DeviceInfo device)
        {
            OnDeviceDetected?.Invoke(null, new DeviceDetectedEventArgs(device));
        }
    }
}
