using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class SetLedOperationModeDto
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("effect_id")]
        public int EffectId { get; set; } = 0;
    }
}
