using Moq;
using Pokedex.HttpClient;
using Pokedex.Models;

namespace PokedexTest
{
    public class APICallerTest
    {
        private Mock<IHttpClientWraper> HttpClientWraper = new();



        [Test]
        public async Task WhenWeGetAPokemonItSerializesCorrectly()
        {
            HttpClientWraper.Setup(c => c.GetFromURL(It.IsAny<string>())).ReturnsAsync(ExampleJsons.PokemonJsonSample);
            var target = new APICaller(HttpClientWraper.Object);

            var result = await target.GetFromURL<Pokemon>("dumb string");


            result.Switch(
                pokemon =>
                    Assert.Multiple(() =>
                    {
                        Assert.That(pokemon, Is.Not.Null);
                        Assert.That(pokemon.types, Is.Not.Null);
                        Assert.That(pokemon.id, Is.EqualTo(132));
                    }),
                error => Assert.Fail()
                );
        }
        [Test]
        public async Task WhenWeGetAPTypeItSerializesCorrectly()
        {
            HttpClientWraper.Setup(c => c.GetFromURL(It.IsAny<string>())).ReturnsAsync(ExampleJsons.TypeExample);
            var target = new APICaller(HttpClientWraper.Object);

            var result = await target.GetFromURL<TypeInfo>("dumb string");


            result.Switch(
                type =>
                    Assert.Multiple(() =>
                    {
                        Assert.That(type, Is.Not.Null);
                        Assert.That(type.DamageRelations, Is.Not.Null);
                        Assert.That(type.Name, Is.EqualTo("flying"));
                    })
                ,
                error => Assert.Fail()

                );
        }

        [Test]
        public async Task WhenTheStringIsNotJsonThenSerialitzationError()
        {
            HttpClientWraper.Setup(c => c.GetFromURL(It.IsAny<string>())).ReturnsAsync("Not a Json string");
            var target = new APICaller(HttpClientWraper.Object);
            var result = await target.GetFromURL<TypeInfo>("dumb string");
            result.Switch(
                type => Assert.Fail(),
                error => Assert.That(error is SerialisingError,Is.True));
        }
    }
}