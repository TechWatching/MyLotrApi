using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLotrApi
{
    public interface ITheOneApiService
    {
        Task<IList<Movie>> GetMovies(IDictionary<string, string?>? queryParams = null);
        Task<IList<Character>> GetCharacters(IDictionary<string, string?>? queryParams = null);
    }
}
