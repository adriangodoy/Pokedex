namespace Pokedex
{
    public interface IPokedexService
    {
        Task<string> GetPokemonsTypeStrenghtsAndWeaknesses(string id);
    }
}