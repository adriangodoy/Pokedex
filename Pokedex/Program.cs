using System.Data;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Models;

var client =new HttpClient();
var result=client.GetAsync("https://pokeapi.co/api/v2/type/poison/");
var stringResult =await result.Result.Content.ReadAsStringAsync();
var jsonSerializer = JsonSerializer.Deserialize<TypeInfo>(stringResult);


Console.WriteLine(stringResult);
Console.ReadKey();