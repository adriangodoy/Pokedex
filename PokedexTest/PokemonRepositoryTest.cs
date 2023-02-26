using Moq;
using Pokedex.HttpClient;
using Pokedex.Models;
using Pokedex.Repositories;

namespace PokedexTest
{
    public class PokemonRepositoryTest
    {
        [Test]
        public async Task  PokemonRepositoryCallsTheRightUrl()
        {
            var id = "1";
            var ApiCallerMock = new Mock<IAPICaller>();
            var target = new PokedexRepository(ApiCallerMock.Object);
            await target.GetPokemonByIdOrName(id);
            ApiCallerMock.Verify(a => a.GetFromURL<Pokemon>("https://pokeapi.co/api/v2/pokemon/1"), Times.Once);
            await target.GetTypeByIdOrName(id);
            ApiCallerMock.Verify(a => a.GetFromURL<TypeInfo>("https://pokeapi.co/api/v2/type/1"), Times.Once);
        }
    }
}
