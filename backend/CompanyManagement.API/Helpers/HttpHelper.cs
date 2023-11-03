using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CompanyManagement.API.Helpers
{
    public static class HttpHelper
    {
        public static HttpRequestMessage GetRequestMessageApiKey(this IConfiguration config, HttpMethod method, string url, Guid? countryId = null, Guid? tenantId = null)
        {
            var apiKey = config.GetValue<string>("apiKey");
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "x-api-key", apiKey }
                }
            };

            httpRequestMessage.AddDefaultHeader(null, countryId, tenantId);

            return httpRequestMessage;
        }

        public static HttpRequestMessage GetRequestMessageApiKeyWithBody(this IConfiguration config, HttpMethod method, dynamic model, string url, Guid? countryId = null, Guid? tenantId = null)
        {
            var httpRequestMessage = GetRequestMessageApiKey(config, method, url);
            httpRequestMessage.Content = GetByteContent(model);

            httpRequestMessage.AddDefaultHeader(null, countryId, tenantId);

            return httpRequestMessage;
        }

        public static HttpRequestMessage GetRequestMessage(HttpMethod method, string url, Guid? countryId = null, Guid? tenantId = null)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };

            httpRequestMessage.AddDefaultHeader(null, countryId, tenantId);

            return httpRequestMessage;
        }

        public static void AddDefaultHeader(this HttpRequestMessage request, IHttpContextAccessor accessor, Guid? countryId = null, Guid? tenantId = null)
        {
            var profileId = accessor?.HttpContext.GetUserId();
            if (!GuidHelper.IsNullOrEmpty(profileId))
            {
                request.Headers.Add("profileId", profileId.ToString());
            }
        }

        public static async Task<HttpResponseMessage> GetResponseMessage(this HttpClient client, HttpRequestMessage request, CancellationToken ct = default(CancellationToken))
        {
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct)
                        .ContinueWith((t) =>
                        {
                            return new HttpResponseMessage(t.Result.StatusCode)
                            {
                                Content = t.Result.Content
                            };
                        });
            return response;
        }

        public static ByteArrayContent GetByteContent(dynamic model)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        public static async Task<T> GetResponse<T>(this HttpClient client, HttpRequestMessage request, CancellationToken ct = default(CancellationToken)) where T : class
        {
            var response = await GetResponseMessage(client, request, ct);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<T>().Result;
            }
            return null;
        }

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var result = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
