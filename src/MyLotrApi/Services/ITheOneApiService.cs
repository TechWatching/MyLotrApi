using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLotrApi.Services
{
    public interface ITheOneApiService
    {
        Task<IList<Movie>> GetMovies(IDictionary<string, string?>? queryParams = null);
        Task<IList<Character>> GetCharacters(IDictionary<string, string?>? queryParams = null);
    }
}
