using Pokedex.Models;
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

        public async Task<string> GetPokemonsTypeStrenghtsAndWeaknesses(string id)
        { 
           var pokemon= await _pokedexRepository.GetPokemonByIdOrName(id);

           await pokemon.Match(  ProcessPokemonStrenghtsAndWeaknesses,  ProcessError);
        
        }


        public async Task<string> ProcessPokemonStrenghtsAndWeaknesses(Pokemon pokemon)
        {
           var typesResult=  pokemon.types.AsParallel().Select( async type => await _pokedexRepository.GetTypeByIdOrName(type.type.Name)).ToList();
            if (typesResult.Any( result =>  result.Result.IsT1)) {

               return await ProcessError(typesResult.First(result => result.Result.IsT1).Result.AsT1);
            }
            var damageRelations=typesResult.Select(oneOf => oneOf.Result.AsT0).Select(type => type.DamageRelations);

            var DamageMultipliyerDiccionary = new Dictionary<string, (int,int)>();
            foreach (var damageRelation in damageRelations)
            {
                foreach( var type in damageRelation.HalfDamageFrom)
                {
                    
                }
            }
            

        }

        public async Task<string> ProcessError(PokedexError error)
        {

        }
    }
}
