using System.Collections.Generic;
using System.Threading.Tasks;
using MyLotrApi.Models.QueryParams;
using Refit;

namespace MyLotrApi.Services
{
    public interface ITheOneApiService
    {
        [Get("/bestratedmovies")]
        Task<IAsyncEnumerable<Movie>> GetBestRatedMovies();

        [Get("/character")]
        Task<CharacterResponse> GetCharacters(CharacterQueryParam? characterQueryParam = null);

        [Get("/movie")]
        Task<MovieResponse> GetMovies();

        [Post("/ratings")]
        Task UpdateUserRating(MovieRating movieRating);
    }
}