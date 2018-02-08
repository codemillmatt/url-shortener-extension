using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShortUrl.Core
{
    public class HttpService
    {
        static readonly JsonSerializer _serializer = new JsonSerializer();
        static readonly HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };

        public static async Task<HttpResponseMessage> Post<T>(string url, T objectToPost, string authToken = null)
        {
            var payload = JsonConvert.SerializeObject(objectToPost);

            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    if (!string.IsNullOrWhiteSpace(authToken))
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                    request.Content = content;

                    return await client.SendAsync(request).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"**** ERROR: {ex.Message}");
                return null;
            }
        }


    }
}
