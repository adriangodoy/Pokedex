using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    internal class Startup
    {
        private readonly IPokedexService _pokedexService;

        public Startup(IPokedexService pokedexService)
        {
            _pokedexService = pokedexService;
        }

        public async Task Start()
        {
            Console.WriteLine("Please introduce the pokemon name or id");
            var id=Console.ReadLine();
            Console.WriteLine(await _pokedexService.GetPokemonsTypeStrenghtsAndWeaknesses(id));
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }

    }
}
