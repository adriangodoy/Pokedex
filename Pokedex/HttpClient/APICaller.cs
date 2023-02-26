using OneOf;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.HttpClient;

public class APICaller : IAPICaller
{

    public APICaller(IHttpClientWraper httpClientWraper)
    {
        _httpClientWraper = httpClientWraper;
    }

    private IHttpClientWraper _httpClientWraper;

    public async Task<OneOf<T, PokedexError>> GetFromURL<T>(string url)
    {

        var WraperResult = await _httpClientWraper.GetFromURL(url);
        return WraperResult.Match<OneOf<T, PokedexError>>(
            resultContent =>
        {
            try
            {
                var deserializedObject = JsonSerializer.Deserialize<T>(resultContent);
                if (deserializedObject != null) return deserializedObject;
                return new SerialisingError { Exception = new Exception("Serialization returned null") };
            }
            catch (Exception e)
            {

                return new SerialisingError() { Exception = e };
            }
        },
            error => OneOf<T, PokedexError>.FromT1(error)
        );
    }
}

