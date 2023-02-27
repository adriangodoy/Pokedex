using Pokedex.Models;
using Pokedex.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex
{
    public class PokedexService : IPokedexService
    {
        private readonly IPokedexRepository _pokedexRepository;

        public PokedexService(IPokedexRepository pokedexRepository)
        {
            _pokedexRepository = pokedexRepository;
        }

        public async Task<string> GetPokemonsTypeStrenghtsAndWeaknesses(string id)
        {
            var pokemon = await _pokedexRepository.GetPokemonByIdOrName(id);            
            return await pokemon.Match(ProcessPokemonStrenghtsAndWeaknesses, ProcessError); ;
        }

        public async Task<string> ProcessPokemonStrenghtsAndWeaknesses(Pokemon pokemon)
        {
            var typesResult = pokemon.types.AsParallel().Select(async type =>
            await _pokedexRepository.GetTypeByIdOrName(type.type.Name)
            ).ToList();
            if (typesResult.Any(result => result.Result.IsT1))
            {

                return await ProcessError(typesResult.First(result => result.Result.IsT1).Result.AsT1);
            }
            var damageRelations = typesResult.Select(oneOf => oneOf.Result.AsT0).Select(type => type.DamageRelations);

            (var DamageMultipliyerDiccionary, var DamageReceivedDiccionary) = MakeDamageDictionaries(damageRelations);



            return MakeMessageFromDictionaries(DamageMultipliyerDiccionary, DamageReceivedDiccionary);
        }

        private const string _template =
            """
            Your pokemon atacks are strong against:
                {0}
            Your pokemon atacks are weak against:
                {1}
            Your pokemon defense are strong against:
                {2}
            Your pokemon defense are weak against:
                {3}
            """;

        private string MakeMessageFromDictionaries(Dictionary<string, (int, int)> damageMultipliyerDiccionary, Dictionary<string, (int, int)> damageReceivedDiccionary)
        {
            List<string> strongAtack = new();
            List<string> weakAtack = new();
            List<string> strongDefense = new();
            List<string> weakDefense = new();

            foreach (var keyValue in damageMultipliyerDiccionary.ToList())
            {
                if (keyValue.Value.Item1 > keyValue.Value.Item2) { strongAtack.Add(keyValue.Key); }
                else if (keyValue.Value.Item1 < keyValue.Value.Item2) { weakAtack.Add(keyValue.Key); }
            }
            foreach (var keyValue in damageReceivedDiccionary.ToList())
            {
                if (keyValue.Value.Item1 > keyValue.Value.Item2) { strongDefense.Add(keyValue.Key); }
                else if (keyValue.Value.Item1 < keyValue.Value.Item2) { weakDefense.Add(keyValue.Key); }
            }
            return string.Format(_template,
                string.Join($"{Environment.NewLine}    ", strongAtack),
                string.Join($"{Environment.NewLine}    ", weakAtack),
                string.Join($"{Environment.NewLine}    ", strongDefense),
                string.Join($"{Environment.NewLine}    ", weakDefense));


        }

        public (Dictionary<string, (int, int)>, Dictionary<string, (int, int)>) MakeDamageDictionaries(IEnumerable<TypeRelations> damageRelations)
        {
            var DamageMultipliyerDiccionary = new Dictionary<string, (int, int)>();
            var DamageReceivedDiccionary = new Dictionary<string, (int, int)>();
            foreach (var damageRelation in damageRelations)
            {
                foreach (var type in damageRelation.HalfDamageFrom)
                {
                    var previousValue = DamageReceivedDiccionary.GetValueOrDefault(type.Name, (1, 1));
                    DamageReceivedDiccionary[type.Name] = (previousValue.Item1, previousValue.Item2 * 2);
                }

                foreach (var type in damageRelation.DoubleDamageFrom)
                {
                    var previousValue = DamageReceivedDiccionary.GetValueOrDefault(type.Name, (1, 1));
                    DamageReceivedDiccionary[type.Name] = (previousValue.Item1 * 2, previousValue.Item2);
                }
                foreach (var type in damageRelation.NoDamageFrom)
                {
                    var previousValue = DamageReceivedDiccionary.GetValueOrDefault(type.Name, (1, 1));
                    DamageReceivedDiccionary[type.Name] = (previousValue.Item1 * 0, previousValue.Item2);
                }

                foreach (var type in damageRelation.DoubleDamageTo)
                {
                    var previousValue = DamageMultipliyerDiccionary.GetValueOrDefault(type.Name, (1, 1));
                    DamageMultipliyerDiccionary[type.Name] = (previousValue.Item1 * 2, previousValue.Item2);
                }
                foreach (var type in damageRelation.HalfDamageTo)
                {
                    var previousValue = DamageMultipliyerDiccionary.GetValueOrDefault(type.Name, (1, 1));
                    DamageMultipliyerDiccionary[type.Name] = (previousValue.Item1, previousValue.Item2 * 2);
                }
                foreach (var type in damageRelation.NoDamageTo)
                {
                    var previousValue = DamageMultipliyerDiccionary.GetValueOrDefault(type.Name, (1, 1));
                    DamageMultipliyerDiccionary[type.Name] = (previousValue.Item1 * 0, previousValue.Item2);
                }

            }
            return (DamageMultipliyerDiccionary, DamageReceivedDiccionary);
        }

        public async Task<string> ProcessError(PokedexError error) => error switch
        {
            HTTPError httpError => $"Error contacting https://pokeapi.co/ error code {httpError.StatusCode} with content {httpError.Content}",
            SerialisingError serialisingError => $"Error serialising the message from pokeapi with the Exception message: {serialisingError.Exception.Message}",
            NotFoundError _ => "Pokemon not found try another pokemon name or id",
            _ => "Unidentified error"
        };

    }
}
