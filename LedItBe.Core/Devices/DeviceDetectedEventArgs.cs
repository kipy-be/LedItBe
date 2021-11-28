using System;

namespace LedItBe.Core.Devices
{
    public class DeviceDetectedEventArgs : EventArgs
    {
        public Device Device { get; private set; }

        public DeviceDetectedEventArgs(Device device)
        {
            Device = device;
        }
    }
}
