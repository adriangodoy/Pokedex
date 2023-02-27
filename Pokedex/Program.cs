using System.Data;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pokedex;
using Pokedex.HttpClient;
using Pokedex.Models;
using Pokedex.Repositories;
using static System.Net.Mime.MediaTypeNames;

var services = new ServiceCollection();
ConfigureServices(services);
await services
    .AddSingleton<Startup>()
    .BuildServiceProvider()
    .GetRequiredService<Startup>()
    .Start();

void ConfigureServices(IServiceCollection services)
{
    services
            .AddSingleton<IPokedexService, PokedexService>()
            .AddSingleton<IPokedexRepository, PokedexRepository>()
            .AddSingleton<IAPICaller, APICaller>()
            .AddSingleton<IHttpClientWraper, HttpClientWraper>()
            .AddHttpClient()
            ;
        
}
