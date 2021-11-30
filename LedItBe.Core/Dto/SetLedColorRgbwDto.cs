using LedItBe.Core.Devices;
using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class SetLedColorRgbwDto
    {
        [JsonPropertyName("red")]
        public int R { get; set; }

        [JsonPropertyName("green")]
        public int G { get; set; }

        [JsonPropertyName("blue")]
        public int B { get; set; }
        [JsonPropertyName("white")]
        public int W { get; set; }

        public SetLedColorRgbwDto()
        {}

        public SetLedColorRgbwDto(LedColor color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            W = color.W;
        }
    }
}
