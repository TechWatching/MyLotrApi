using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyLotrApi.Models.QueryParams;
using MyLotrApi.Services;

namespace MyLotrApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotrController : ControllerBase
    {
        private readonly ITheOneApiService _theOneApiService;

        public LotrController(ITheOneApiService theOneApiService)
        {
            _theOneApiService = theOneApiService;
        }

        [HttpGet]
        [Route("bestratedmovies")]
        public async Task<IActionResult> GetBestRatedMovies()
        {
            IAsyncEnumerable<Movie> bestRatedMovies = await _theOneApiService.GetBestRatedMovies();
            return Ok(bestRatedMovies);
        }

        [HttpGet]
        [Route("famousorcs")]
        public async Task<IActionResult> GetFamousOrcs()
        {
            var queryParams = new CharacterQueryParam
            {
                Race = "Orc"
            };
            var characterResponse = await _theOneApiService.GetCharacters(queryParams);
            return Ok(characterResponse.Docs);
        }

        [HttpGet]
        [Route("popularmovies")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var movieResponse = await _theOneApiService.GetMovies();
            var popularMovies = movieResponse.Docs.Where(m => m.RottenTomatesScore > 80);
            return Ok(popularMovies);
        }

        [HttpPost]
        [Route("ratings")]
        public async Task<IActionResult> RateMovie([FromBody] MovieRating movieRating)
        {
            if (movieRating.Rating < 1)
            {
                return BadRequest("Invalid rating. Please rate from 1 to 5.");
            }
            if(string.IsNullOrWhiteSpace(movieRating.Name))
            {
                return BadRequest("Please name a movie to be rated.");
            }
            var movieResponse = await _theOneApiService.GetMovies();
            var movieBeingRated = movieResponse.Docs.FirstOrDefault(x => x.Name == movieRating.Name);
            if (movieBeingRated is null)
            {
                return BadRequest("Unknown movie.");
            }
            await _theOneApiService.UpdateUserRating(movieRating);
            return Ok();
        }
    }
}