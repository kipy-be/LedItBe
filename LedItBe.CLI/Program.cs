using LedItBe.Core.Common;
using LedItBe.Core.Devices;
using LedItBe.Core.IO.Json;
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

            Console.WriteLine("Detected device '{0}' ({1})", _device.Name, e.Device.Ip);
            Console.WriteLine("Infos :");
            Console.WriteLine(JsonUtils.ToReadableJson(_device.Infos));

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
                Console.WriteLine("could not connect to device");
                return;
            }

            Console.WriteLine("Device connected");
            Console.WriteLine("Current led operation mode : {0}", _device.LedOperationMode);

            LedColor.SetWhiteColorTemperature(WhiteColorTemperature.K4000);
            await _device.ToStaticColorMode(LedColor.Red);
            _waiter.WaitOne(5000);

            var frame = Frame.Create(_device.Infos.LedCount, _device.Infos.LedProfile);
            await _device.ToDirectMode();

            for(int i = 0; i < frame.Data.Length; i++)
            {
                frame.Data[i] = LedColor.White;
                _device.SendFrame(frame);
                _waiter.WaitOne(40);
            }

            await _device.TurnOff();
        }
    }
}