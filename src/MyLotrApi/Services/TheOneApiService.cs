using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using MyLotrApi.Configurations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyLotrApi.Services
{
    public class TheOneApiService : ITheOneApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TheOneApiConfiguration _configuration;

        public TheOneApiService(HttpClient httpClient, IOptions<TheOneApiConfiguration> configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration.Value;
        }

        public async Task<IList<Movie>> GetMovies(IDictionary<string, string?>? queryParams = null)
        {
            var movieResponse = await Send<MovieResponse>(HttpMethod.Get, "movie", queryParams);
            return movieResponse.Docs;
        }

        public async Task<IList<Character>> GetCharacters(IDictionary<string, string?>? queryParams = null)
        {
            var characterResponse = await Send<CharacterResponse>(HttpMethod.Get, "character", queryParams);
            return characterResponse.Docs;
        }

        private async Task<T> Send<T>(
            HttpMethod httpMethod,
            string route, 
            IDictionary<string, string?>? queryParams = null, 
            object? requestContent = null)
        {
            var url = queryParams != null ? QueryHelpers.AddQueryString(route, queryParams) : route;

            using var request = new HttpRequestMessage(httpMethod, $"{_configuration.BaseUrl}{url}");
            if (requestContent is not null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, MediaTypeNames.Application.Json);
            }

            var response = await _httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}
