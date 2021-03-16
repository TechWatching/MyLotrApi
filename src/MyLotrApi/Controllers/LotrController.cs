using Microsoft.AspNetCore.Mvc;
using MyLotrApi.Models.QueryParams;
using MyLotrApi.Services;
using System.Linq;
using System.Threading.Tasks;

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
        [Route("popularmovies")]
        public async Task<IActionResult> GetPopularMovies()
        {
            var movieResponse = await _theOneApiService.GetMovies();
            var popularMovies = movieResponse.Docs.Where(m => m.RottenTomatesScore > 80);
            return Ok(popularMovies);
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
    }
}
