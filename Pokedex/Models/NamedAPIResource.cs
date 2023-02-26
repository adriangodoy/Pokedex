using System.Text.Json.Serialization;

namespace Pokedex.Models
{
    public class NamedAPIResource : Named
    {
        [JsonPropertyName("url")]
        public string url { get; set; }

    }
}