using System.Text.Json.Serialization;

namespace LedItBe.Core.Dto
{
    internal class LoginResponseDto : ReponseBaseDto
    {
        [JsonPropertyName("authentication_token")]
        public string Token { get; set; }

        [JsonPropertyName("authentication_token_expires_in")]
        public int Expiration { get; set; }

        [JsonPropertyName("challenge-response")]
        public string ChallengeResponse { get; set; }
    }
}
