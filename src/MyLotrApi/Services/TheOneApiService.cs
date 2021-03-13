using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyLotrApi.Services
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

            var movieResponse = await _httpClient.GetFromJsonAsync<MovieResponse>(url);
            return movieResponse?.Docs ?? new List<Movie>();
        }

        public async Task<IList<Character>> GetCharacters(IDictionary<string, string?>? queryParams = null)
        {
            var route = "character";
            var url = queryParams != null ? QueryHelpers.AddQueryString(route, queryParams) : route;

            var characterResponse = await _httpClient.GetFromJsonAsync<CharacterResponse>(url);
            return characterResponse?.Docs ?? new List<Character>();
        }
    }
}
