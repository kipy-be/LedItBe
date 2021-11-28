using LedItBe.Core.Dto;
using System;

namespace LedItBe.Core.Devices
{
    public class DeviceInfo
    {
        public string ProductName { get; set; }
        public string HardwareVersion { get; set; }
        public int BytesPerLed { get; set; }
        public string HardwareId { get; set; }
        public int FlashSize { get; set; }
        public int LedType { get; set; }
        public string ProductCode { get; set; }
        public string FirmwareFamily { get; set; }
        public string DeviceName { get; set; }
        public DateTime Uptime { get; set; }
        public string MacAddress { get; set; }
        public string Uuid { get; set; }
        public int LedMax { get; set; }
        public int LedCount { get; set; }
        public string LedProfile { get; set; }
        public int Fps { get; set; }
        public double MeasuredFps { get; set; }
        public int MovieCapacity { get; set; }
        public int WireType { get; set; }

        internal DeviceInfo()
        {}

        internal DeviceInfo(DeviceInfoDto dto)
        {
            ProductName = dto.ProductName;
            HardwareVersion = dto.HardwareVersion;
            BytesPerLed = dto.BytesPerLed;
            HardwareId = dto.HardwareId;
            FlashSize = dto.FlashSize;
            LedType = dto.LedType;
            ProductCode = dto.ProductCode;
            FirmwareFamily = dto.FirmwareFamily;
            DeviceName = dto.DeviceName;

            if (int.TryParse(dto.Uptime, out int uptime))
            {
                Uptime = DateTime.UtcNow.AddMilliseconds(-uptime);
            }
            else
            {
                Uptime = DateTime.MinValue;
            }

            MacAddress = dto.MacAddress;
            Uuid = dto.Uuid;
            LedMax = dto.LedMax;
            LedCount = dto.LedCount;
            LedProfile = dto.LedProfile;
            Fps = dto.Fps;
            MeasuredFps = dto.MeasuredFps;
            MovieCapacity = dto.MovieCapacity;
            WireType = dto.WireType;
        }
    }
}
