using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class SetLedMovieConfigDto
    {
        [JsonPropertyName("frame_delay")]
        public int FrameDelay { get; set; }

        [JsonPropertyName("leds_number")]
        public int LedCount { get; set; }

        [JsonPropertyName("frames_number")]
        public int FramesNumber { get; set; }
    }
}
