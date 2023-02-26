using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using OneOf;
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using Pokedex.Models;

namespace Pokedex.HttpClient
{
    /// <summary>
    /// Class created to aislate HTTClient And Factory from the rest of the domain.
    /// </summary>
    public class HttpClientWraper : IHttpClientWraper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientWraper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OneOf<string, PokedexError>> GetFromURL(string url)
        {

            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync("https://pokeapi.co/api/v2/type/poison/");
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStringAsync();               
            }
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new NotFoundError();
            }

            return new HTTPError()
            {

            };




        }



    }
}
