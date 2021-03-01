using Microsoft.AspNetCore.WebUtilities;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyLotrApi
{
    public class TheOneApiService : ITheOneApiService
    {
        private readonly HttpClient _httpClient;

        public TheOneApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Movie>> GetMovies(IDictionary<string, string?>? queryParams = null)
        {
            var route = "movie";
            var url = queryParams != null ? QueryHelpers.AddQueryString(route, queryParams) : route;
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new TheOneApiException(response.StatusCode, response.ReasonPhrase);
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var movieResponse = JsonConvert.DeserializeObject<MovieResponse>(responseContent);
            return movieResponse.Docs;
        }

        public async Task<IList<Character>> GetCharacters(IDictionary<string, string?>? queryParams = null)
        {
            var route = "character";
            var url = queryParams != null? QueryHelpers.AddQueryString(route, queryParams) : route;
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new TheOneApiException(response.StatusCode, response.ReasonPhrase);
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var characterResponse = JsonConvert.DeserializeObject<CharacterResponse>(responseContent);
            return characterResponse.Docs;
        }
    }
}
