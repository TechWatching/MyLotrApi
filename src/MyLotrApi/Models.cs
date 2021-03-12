using System.Collections.Generic;

namespace MyLotrApi
{
    public record Movie(string Name, int RuntimeInMinutes, int BudgetInMillions, float RottenTomatesScore);
    
    public record MovieResponse(IList<Movie> Docs, int Total);

    public record Character(string Name, string Realm);

    public record CharacterResponse(IList<Character> Docs, int Total);
}
