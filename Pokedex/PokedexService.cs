using Pokedex.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    internal class PokedexService
    {
        private readonly IPokedexRepository _pokedexRepository;

        public PokedexService(IPokedexRepository pokedexRepository)
        {
            _pokedexRepository = pokedexRepository;
        }

        public Task<string> GetPokemonsTypeStrenghtsAndWeaknesses(string id) { 
        
        
        }
    }
}
