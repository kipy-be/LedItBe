using LedItBe.Core.IO.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace LedItBe.Core.Devices
{
    public static class DeviceExplorer
    {
        private const ushort PORT = 5555;
        private static byte[] DISCOVER_PAYLOAD = new byte[9]
        {
            0x01, 0x64, 0x69, 0x73, 0x63, 0x6F, 0x76, 0x65, 0x72
        };

        private static bool _isScanning = false;
        private static bool _stopAtFirst = false;
        private static ConcurrentDictionary<string, Device> _detectedDevices = new ConcurrentDictionary<string, Device>();
        private static List<UdpClient> _udpClients = new List<UdpClient>();

        public static event EventHandler<DeviceDetectedEventArgs> OnDeviceDetected;

        public static void StartScanForDevices(bool stopAtFirst = false)
        {
            if (_isScanning == true)
            {
                return;
            }

            _isScanning = true;
            _stopAtFirst = stopAtFirst;

            _udpClients.Clear();
            _detectedDevices.Clear();

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

                var ipInfos = nic.GetIPProperties()
                    .UnicastAddresses
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
        }

        public static void StopScan()
        {
            if (_isScanning == false)
            {
                return;
            }

            foreach (var client in _udpClients)
            {
                client.Close();
                client.Dispose();
            }

            _isScanning = false;
        }

        private static void DiscoverNetwork(IPAddress nicBroadcast)
        {
            var udpClient = new UdpClient();
            var inEp = new IPEndPoint(IPAddress.Any, PORT);
            var broadcastEp = new IPEndPoint(nicBroadcast, PORT);

            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(inEp);
            udpClient.BeginReceive(DataReceived, udpClient);

            udpClient.Send(DISCOVER_PAYLOAD, DISCOVER_PAYLOAD.Length, broadcastEp);

            _udpClients.Add(udpClient);
        }

        private async static void DataReceived(IAsyncResult ar)
        {
            if (_isScanning == false)
            {
                return;
            }

            var udpClient = (UdpClient)ar.AsyncState;
            var ep = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = udpClient.EndReceive(ar, ref ep);

            Device device = CheckResponse(ref ep, ref data);
            bool isDevice = device != null;

            if (isDevice)
            {
                if (!_detectedDevices.ContainsKey(device.Ip.ToString()))
                {
                    _detectedDevices.TryAdd(device.Ip.ToString(), device);
                    
                    if (await device.GetInfos())
                    {
                        RaiseOnDeviceDetectedEvent(device);
                    }
                }

                if (_stopAtFirst)
                {
                    StopScan();
                }
            }

            if (!isDevice || !_stopAtFirst)
            {
                udpClient.BeginReceive(DataReceived, udpClient);
            }
        }

        private static Device CheckResponse(ref IPEndPoint ep, ref byte[] data)
        {
            if (data.Length < 8) // Minimal size of device response (ipv4 + "OK" + name with minimal size of 1 + 0x00)
            {
                return null;
            }

            try
            {
                if(!(  data[4] == 'O'
                    && data[5] == 'K'
                    && data[data.Length - 1] == 0x00))
                {
                    return null;
                }

                var ipBytes = data.Create(0, 4)
                    .Reverse()
                    .ToArray();

                if (!ipBytes.IsEqual(ep.Address.GetAddressBytes()))
                {
                    return null;
                }

                string name = data.ReadAsciiString(6);
                var device = new Device(name, ep.Address);

                return device;
            }
            catch
            {
                return null;
            }
        }

        private static void RaiseOnDeviceDetectedEvent(Device device)
        {
            OnDeviceDetected?.Invoke(null, new DeviceDetectedEventArgs(device));
        }
    }
}
