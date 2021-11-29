using LedItBe.Core.Devices;
using LedItBe.Core.IO.Json;
using System;
using System.Threading;

namespace LedItBe.CLI
{
    public class Program
    {
        static AutoResetEvent _waiter = new AutoResetEvent(false);
        static Device _device;

        public static void Main(string[] args)
        {
            Console.CancelKeyPress += OnCancelKeyPress;

            DeviceExplorer.OnDeviceDetected += DeviceExplorer_OnDeviceDetected;
            DeviceExplorer.StartScanForDevices(true);

            _waiter.WaitOne();

            Console.WriteLine("Bye !");
        }

        private static void DeviceExplorer_OnDeviceDetected(object sender, DeviceDetectedEventArgs e)
        {
            _device = e.Device;

            Console.WriteLine("Detected device '{0}' ({1})", _device.Name, e.Device.Ip);
            Console.WriteLine("Infos :");
            Console.WriteLine(JsonUtils.ToReadableJson(_device.Infos));

            InitDevice();
        }

        private static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _waiter.Set();
        }

        private async static void InitDevice()
        {
            if(!await _device.Connect())
            {
                Console.WriteLine("could not connect to device");
                return;
            }

            Console.WriteLine("Device connected");
        }
    }
}