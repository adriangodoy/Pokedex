using System.Text.Json.Serialization;

namespace Pokedex.Models
{
    /// <summary>
    /// The information for how a type interacts with other types
    /// </summary>
    public class TypeRelations
    {
        /// <summary>
        /// A list of types this type has no effect on.
        /// </summary>
        [JsonPropertyName("no_damage_to")]
        public List<NamedAPIResource> NoDamageTo { get; set; }

        /// <summary>
        /// A list of types this type is not very effect against.
        /// </summary>
        [JsonPropertyName("half_damage_to")]
        public List<NamedAPIResource> HalfDamageTo { get; set; }

        /// <summary>
        /// A list of types this type is very effect against.
        /// </summary>
        [JsonPropertyName("double_damage_to")]
        public List<NamedAPIResource> DoubleDamageTo { get; set; }

        /// <summary>
        /// A list of types that have no effect on this type.
        /// </summary>
        [JsonPropertyName("no_damage_from")]
        public List<NamedAPIResource> NoDamageFrom { get; set; }

        /// <summary>
        /// A list of types that are not very effective against this type.
        /// </summary>
        [JsonPropertyName("half_damage_from")]
        public List<NamedAPIResource> HalfDamageFrom { get; set; }

        /// <summary>
        /// A list of types that are very effective against this type.
        /// </summary>
        [JsonPropertyName("double_damage_from")]
        public List<NamedAPIResource> DoubleDamageFrom { get; set; }
    }
}
