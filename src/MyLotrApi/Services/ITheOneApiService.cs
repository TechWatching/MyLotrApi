using MyLotrApi.Models.QueryParams;
using Refit;
using System.Threading.Tasks;

namespace MyLotrApi.Services
{
    public interface ITheOneApiService
    {
        [Get("/movie")]
        Task<MovieResponse> GetMovies();
        
        [Get("/character")]
        Task<CharacterResponse> GetCharacters(CharacterQueryParam? characterQueryParam = null);
    }
}
