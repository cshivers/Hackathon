using Hackathon.Core.Blitz.Models;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hackathon.Core.Services
{
    public class BlitzService
        : IBlitzService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlitzService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BlitzResponse> GetPlayer(string id)
        {
            // ignore case since the json has it lower and we have it pascal
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            HttpClient client = _httpClientFactory.CreateClient();

            // in a real world application we would want the url in configuration
            HttpResponseMessage response = await client.GetAsync($"https://valorant.iesdev.com/player/{id}");

            // get the response
            using Stream responseStream = await response.Content.ReadAsStreamAsync();

            // deserialize into our POCO and return
            return await JsonSerializer.DeserializeAsync<BlitzResponse>(responseStream, options);
        }
    }
}
