using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class ReponseBaseDto
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
