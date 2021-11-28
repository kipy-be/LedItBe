namespace LedItBe.Core.Api.Base
{
    public class ApiConfig
    {
        public string Uri { get; set; }

        public ApiConfig() {}
        public ApiConfig(string uri)
        {
            Uri = uri;
        }
    }
}
