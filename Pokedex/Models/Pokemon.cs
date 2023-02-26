using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class Pokemon
    {
        public int id { get; set; }
        public List<PokemonType> types { get; set; }
        public class PokemonType
        {
            public int slot { get; set; }
            public NamedAPIResource type { get; set; }
        }
    }
}
