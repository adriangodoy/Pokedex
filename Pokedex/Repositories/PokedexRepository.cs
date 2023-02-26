using Pokedex.HttpClient;
using Pokedex.Models;

namespace Pokedex.Repositories
{
    public class PokedexRepository : IPokedexRepository
    {
        private readonly IAPICaller _apiCaller;
        private const string UrlStringPokemon = "https://pokeapi.co/api/v2/pokemon/{0}";
        private const string UrlStringType = "https://pokeapi.co/api/v2/type/{0}";


        public PokedexRepository(IAPICaller apiCaller)
        {
            _apiCaller = apiCaller;
        }

        public async Task<OneOf<Pokemon, PokedexError>> GetPokemonByIdOrName(string id)
        {
            var url = string.Format(UrlStringPokemon, id);

            return await _apiCaller.GetFromURL<Pokemon>(url);
        }

        public async Task<OneOf<TypeInfo, PokedexError>> GetTypeByIdOrName(string id)
        {
            var url = string.Format(UrlStringType, id);

            return await _apiCaller.GetFromURL<TypeInfo>(url);
        }

    }
}
