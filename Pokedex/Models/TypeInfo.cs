using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public class TypeInfo : Named
    {
        [JsonPropertyName("damage_relations")]
        public TypeRelations DamageRelations { get; set; }
    }
}
