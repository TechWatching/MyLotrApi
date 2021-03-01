using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
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
            try
            {
                var movies = await _theOneApiService.GetMovies();
                var popularMovies = movies.Where(m => m.RottenTomatesScore > 80);
                return Ok(popularMovies);
            }
            catch (TheOneApiException ex)
            {
                return StatusCode((int) ex.StatusCode, ex.ReasonPhrase);
            }
        }

        [HttpGet]
        [Route("famousorcs")]
        public async Task<IActionResult> GetFamousORcs()
        {
            try
            {
                Dictionary<string, string?> queryParams = new() { ["race"] = "Orc" };
                var movies = await _theOneApiService.GetCharacters(queryParams);
                return Ok(movies);
            }
            catch (TheOneApiException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.ReasonPhrase);
            }
        }
    }
}
