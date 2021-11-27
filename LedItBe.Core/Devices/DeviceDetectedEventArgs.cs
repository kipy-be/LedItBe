using System;

namespace LedItBe.Core.Devices
{
    public class DeviceDetectedEventArgs : EventArgs
    {
        public DeviceInfo Device { get; private set; }

        public DeviceDetectedEventArgs(DeviceInfo device)
        {
            Device = device;
        }
    }
}
