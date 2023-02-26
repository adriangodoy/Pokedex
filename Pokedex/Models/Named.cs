using System.Text.Json.Serialization;

namespace Pokedex.Models
{
    public abstract class Named
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}