using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class DeviceInfoDto
    {
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("hardware_version")]
        public string HardwareVersion { get; set; }

        [JsonPropertyName("bytes_per_led")]
        public int BytesPerLed { get; set; }

        [JsonPropertyName("hw_id")]
        public string HardwareId { get; set; }

        [JsonPropertyName("flash_size")]
        public int FlashSize { get; set; }

        [JsonPropertyName("led_type")]
        public int LedType { get; set; }

        [JsonPropertyName("product_code")]
        public string ProductCode { get; set; }

        [JsonPropertyName("fw_family")]
        public string FirmwareFamily { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("uptime")]
        public string Uptime { get; set; }

        [JsonPropertyName("mac")]
        public string MacAddress { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("max_supported_led")]
        public int LedMax { get; set; }

        [JsonPropertyName("number_of_led")]
        public int LedCount { get; set; }

        [JsonPropertyName("led_profile")]
        public string LedProfile { get; set; }

        [JsonPropertyName("frame_rate")]
        public int Fps { get; set; }

        [JsonPropertyName("measured_frame_rate")]
        public double MeasuredFps { get; set; }

        [JsonPropertyName("movie_capacity")]
        public int MovieCapacity { get; set; }

        [JsonPropertyName("wire_type")]
        public int WireType { get; set; }

        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
