using OneOf;
using Pokedex.Models;

namespace Pokedex.HttpClient
{
    public interface IAPICaller
    {
        Task<OneOf<T, PokedexError>> GetFromURL<T>(string url);
    }
}