using LedItBe.Core.Devices;
using System;
using System.Threading;

namespace LedItBe.CLI
{
    public class Program
    {
        static AutoResetEvent _waiter = new AutoResetEvent(false);  

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
            Console.WriteLine("Detected device '{0}' ({1})", e.Device.Name, e.Device.Ip);
        }

        private static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _waiter.Set();
        }
    }
}