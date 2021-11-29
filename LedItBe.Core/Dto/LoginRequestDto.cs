using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class LoginRequestDto
    {
        [JsonPropertyName("challenge")]
        public string Challenge { get; set; }
    }
}
