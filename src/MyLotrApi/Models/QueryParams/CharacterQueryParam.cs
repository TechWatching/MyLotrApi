using Refit;

namespace MyLotrApi.Models.QueryParams
{
    public record CharacterQueryParam
    {
        [AliasAs("race")]
        public string Race { get; init; } = default!;
    }
}
