using LedItBe.Core.Common;
using LedItBe.Core.Devices;
using System;
using System.Threading;

namespace LedItBe.CLI
{
    public class Program
    {
        static AutoResetEvent _exitWaiter = new AutoResetEvent(false);
        static AutoResetEvent _waiter = new AutoResetEvent(false);
        static Device _device;

        public static void Main(string[] args)
        {
            Console.CancelKeyPress += OnCancelKeyPress;

            DeviceExplorer.OnDeviceDetected += DeviceExplorer_OnDeviceDetected;
            DeviceExplorer.StartScanForDevices(true);

            _exitWaiter.WaitOne();

            Console.WriteLine("Bye !");
        }

        private static void DeviceExplorer_OnDeviceDetected(object sender, DeviceDetectedEventArgs e)
        {
            _device = e.Device;

            Console.WriteLine("Detected device '{0}'", _device.Name);
            Console.WriteLine("\tDevice alias : {0}", _device.Infos.DeviceName);
            Console.WriteLine("\tIp : {0}", e.Device.Ip);
            Console.WriteLine("\tLeds : {0} {1}", _device.Infos.LedCount, _device.Infos.LedProfile);
            Console.WriteLine("\tUptime : {0:dd/MM/yyyy HH:mm:ss}", _device.Infos.Uptime);
            Console.WriteLine();

            InitDevice();
        }

        private async static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;

            if (_device != null)
            {
                await _device.ToInitialMode();
            }

            _exitWaiter.Set();
        }

        private async static void InitDevice()
        {
            if(!await _device.Connect())
            {
                Console.WriteLine("Could not connect to device");
                return;
            }

            Console.WriteLine("Device connected");
            Console.WriteLine("Current led operation mode : {0}", _device.LedOperationMode);

            LedColor.SetWhiteColorTemperature(WhiteColorTemperature.K4000);
            Console.WriteLine("Set white temperature to 4000K");

            Console.WriteLine("Testing random color for 5 seconds");
            await _device.ToStaticColorMode(LedColor.Random);
            _waiter.WaitOne(5000);

            Console.WriteLine("Starting audio animation");
            _device.StartAudioAnimation();
            Console.WriteLine("Listening...");
        }
    }
}