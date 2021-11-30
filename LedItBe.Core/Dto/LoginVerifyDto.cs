using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class LoginVerifyDto
    {
        [JsonPropertyName("challenge-response")]
        public string ChallengeResponse { get; set; }
    }
}
