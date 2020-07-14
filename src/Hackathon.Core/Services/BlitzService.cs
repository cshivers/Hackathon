using Hackathon.Core.Blitz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hackathon.Core.Services
{
    public class BlitzService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BlitzService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<BlitzResponse> GetPlayer(string id)
        {
            // ignore case since the json has it lower and we have it pascal
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            var client = httpClientFactory.CreateClient();

            // in a real world application we would want the url in configuration
            var response = await client.GetAsync($"https://valorant.iesdev.com/player/{id}");

            // get the response
            using var responseStream = await response.Content.ReadAsStreamAsync();

            // deserialize into our POCO and return
            return await JsonSerializer.DeserializeAsync<BlitzResponse>(responseStream, options);
        }
    }
}
