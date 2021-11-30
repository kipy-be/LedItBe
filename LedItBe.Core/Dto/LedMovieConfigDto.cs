using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class LedMovieConfigDto : ReponseBaseDto
    {
        [JsonPropertyName("frame_delay")]
        public int FrameDelay { get; set; }

        [JsonPropertyName("leds_number")]
        public int LedCount { get; set; }

        [JsonPropertyName("loop_type")]
        public int LoopType { get; set; }

        [JsonPropertyName("frames_number")]
        public int FramesNumber { get; set; }
    }
}
