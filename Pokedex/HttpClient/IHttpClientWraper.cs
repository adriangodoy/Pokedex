using OneOf;
using Pokedex.Models;

namespace Pokedex.HttpClient
{
    public interface IHttpClientWraper
    {
        Task<OneOf<string, PokedexError>> GetFromURL(string url);
    }
}