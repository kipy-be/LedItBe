using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class LedColorDto : ReponseBaseDto
    {
        [JsonPropertyName("hue")]
        public int H { get; set; }

        [JsonPropertyName("saturation")]
        public int S { get; set; }

        [JsonPropertyName("value")]
        public int V { get; set; }

        [JsonPropertyName("red")]
        public int R { get; set; }

        [JsonPropertyName("green")]
        public int G { get; set; }

        [JsonPropertyName("blue")]
        public int B { get; set; }
        [JsonPropertyName("white")]
        public int W { get; set; }
    }
}
