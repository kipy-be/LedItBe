using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class LedOperationModeDto : ReponseBaseDto
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("shop_mode")]
        public int ShopMode { get; set; }
    }
}
