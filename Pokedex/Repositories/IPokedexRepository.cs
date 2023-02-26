using Pokedex.Models;

namespace Pokedex.Repositories
{
    public interface IPokedexRepository
    {
        Task<OneOf<Pokemon, PokedexError>> GetPokemonByIdOrName(string id);
        Task<OneOf<TypeInfo, PokedexError>> GetTypeByIdOrName(string id);
    }
}