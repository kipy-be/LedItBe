using LedItBe.Core.IO.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LedItBe.Core.Api.Http
{
    public abstract class HttpApiClient
    {
        private static HttpClient _httpClient;
        private static string _sessionToken;
        private static string _uri;

        static HttpApiClient()
        {
            _httpClient = new HttpClient(new HttpClientHandler()
            {
                AllowAutoRedirect = false
            });
        }

        public HttpApiClient(string uri)
        {
            _uri = uri;
        }

        public static void SetSessionInfo(string token)
        {
            _sessionToken = token;
        }

        private HttpApiResponse<T> MakeJsonRequest<T>(string url, HttpMethod method, object data = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                SetHeaders(request);

                if (data != null)
                {
                    string payload = IO.Json.JsonUtils.ToJson(data);
                    request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
                }

                return MakeRequest<T>(request);
            }
        }

        private HttpApiResponse<T> MakeRequest<T>(string url, HttpMethod method, Dictionary<string, object> data = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                SetHeaders(request);

                if (data != null && data.Count > 0)
                {
                    string payload = string.Join("&", data.Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value.ToString())}"));
                    request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
                }

                return MakeRequest<T>(request);
            }
        }

        private HttpApiResponse<T> MakeRequest<T>(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            try
            {
                response = _httpClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return HttpApiResponse<T>.CreateError(response?.StatusCode ?? HttpStatusCode.InternalServerError);
                }

                using (var reader = new StreamReader(response.Content.ReadAsStreamAsync().Result))
                {
                    string json = reader.ReadToEnd();
                    T token;

                    if (string.IsNullOrWhiteSpace(json) || !TryParse<T>(json, out token))
                    {
                        return HttpApiResponse<T>.CreateError(response?.StatusCode ?? HttpStatusCode.NoContent);
                    }

                    return HttpApiResponse<T>.Create(response?.StatusCode ?? HttpStatusCode.OK, token);
                }
            }
            catch (AggregateException aex)
            {
                var wex = GetWebException(aex);

                return (wex != null)
                    ? HttpApiResponse<T>.CreateError(HttpStatusCode.InternalServerError, wex.Status.ToString(), wex)
                    : HttpApiResponse<T>.CreateError(HttpStatusCode.InternalServerError, aex);
            }
            catch (Exception ex)
            {
                return (response != null)
                    ? HttpApiResponse<T>.CreateError(response.StatusCode, ex)
                    : HttpApiResponse<T>.CreateError(HttpStatusCode.InternalServerError, ex);
            }
        }

        private HttpApiResponse MakeJsonRequest(string url, HttpMethod method, object data = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                SetHeaders(request);

                if (data != null)
                {
                    string json = JsonUtils.ToJson(data);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                return MakeRequest(request);
            }
        }

        private HttpApiResponse MakeRequest(string url, HttpMethod method, Dictionary<string, object> data = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                SetHeaders(request);

                if (data != null && data.Count > 0)
                {
                    string payload = string.Join("&", data.Select(p => $"{HttpUtility.UrlEncode(p.Key)}={HttpUtility.UrlEncode(p.Value.ToString())}"));
                    request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
                }

                return MakeRequest(request);
            }
        }

        private HttpApiResponse MakeRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            try
            {
                response = _httpClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return HttpApiResponse.CreateError(response?.StatusCode ?? HttpStatusCode.InternalServerError);
                }

                var stream = response.Content.ReadAsStreamAsync().Result;

                return HttpApiResponse.Create(response?.StatusCode ?? HttpStatusCode.OK, stream);
            }
            catch (AggregateException aex)
            {
                var wex = GetWebException(aex);
                return (wex != null)
                    ? HttpApiResponse.CreateError(HttpStatusCode.InternalServerError, wex.Status.ToString(), wex)
                    : HttpApiResponse.CreateError(HttpStatusCode.InternalServerError, aex);
            }
            catch (Exception ex)
            {
                return (response != null)
                    ? HttpApiResponse.CreateError(response.StatusCode, ex)
                    : HttpApiResponse.CreateError(HttpStatusCode.InternalServerError, ex);
            }
        }

        private void SetHeaders(HttpRequestMessage request)
        {
            if (_sessionToken != null)
            {
                request.Headers.Add("X-Auth-Token", _sessionToken);
            }
        }

        private string FormatUrl(string service, object[] queryPath = null, Dictionary<string, object> queryParameters = null)
        {
            string path = null;
            string parameters = null;

            if (queryParameters?.Count > 0)
            {
                parameters = string.Join("&", queryParameters.Select
                (
                    p => (p.Value != null)
                        ? $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value.ToString())}"
                        : $"{Uri.EscapeDataString(p.Key)}")
                );
            }

            if (queryPath?.Length > 0)
            {
                path = string.Join("/", queryPath.Select(x => x.ToString()));
            }

            return path != null
                ? (parameters != null)
                    ? $"{_uri}/{service}/{path}?{parameters}"
                    : $"{_uri}/{service}/{path}"
                : (parameters != null)
                    ? $"{_uri}/{service}?{parameters}"
                    : $"{_uri}/{service}";
        }

        protected Task<HttpApiResponse<T>> Get<T>(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest<T>(url, HttpMethod.Get));
        }

        protected Task<HttpApiResponse> Get(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest(url, HttpMethod.Get));
        }

        protected Task<HttpApiResponse<T>> Post<T>(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null, Dictionary<string, object> data = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeRequest<T>(url, HttpMethod.Post, data));
        }

        protected Task<HttpApiResponse<T>> PostJson<T>(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null, object data = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest<T>(url, HttpMethod.Post, data));
        }

        protected Task<HttpApiResponse> Post(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null, Dictionary<string, object> data = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeRequest(url, HttpMethod.Post, data));
        }

        protected Task<HttpApiResponse> PostJson(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null, object data = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest(url, HttpMethod.Post, data));
        }

        protected Task<HttpApiResponse<T>> PutJson<T>(string service = null, object[] queryValues = null, Dictionary<string, object> queryParameters = null, object data = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest<T>(url, HttpMethod.Put, data));
        }

        protected Task<HttpApiResponse> PutJson(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null, object data = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest(url, HttpMethod.Put, data));
        }

        protected Task<HttpApiResponse<T>> Delete<T>(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest<T>(url, HttpMethod.Delete));
        }

        protected Task<HttpApiResponse> Delete(string service, object[] queryValues = null, Dictionary<string, object> queryParameters = null)
        {
            var url = FormatUrl(service, queryValues, queryParameters);
            return Task.FromResult(MakeJsonRequest(url, HttpMethod.Delete));
        }

        private bool TryParse<T>(string jsonData, out T token)
        {
            try
            {
                token = JsonUtils.FromJson<T>(jsonData);
                return true;
            }
            catch
            {

                token = default;
                return false;
            }
        }

        private WebException GetWebException(AggregateException aex)
        {
            var wex = aex.InnerExceptions.FirstOrDefault(e => e is WebException || e.InnerException is WebException);

            if (wex == null)
            {
                return null;
            }

            return (wex is WebException)
                ? (WebException)wex
                : (WebException)wex.InnerException;
        }
    }
}
