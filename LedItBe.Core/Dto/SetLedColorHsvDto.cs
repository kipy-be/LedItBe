using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class SetLedColorHsvDto
    {
        [JsonPropertyName("hue")]
        public int H { get; set; }

        [JsonPropertyName("saturation")]
        public int S { get; set; }

        [JsonPropertyName("value")]
        public int V { get; set; }
    }
}
