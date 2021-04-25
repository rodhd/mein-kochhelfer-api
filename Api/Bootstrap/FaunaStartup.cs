using Domain.Ingredients;
using Domain.Recipes;
using FaunaDB.Client;
using FaunaRepository.Recipes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Bootstrap
{
    public static class FaunaStartup
    {
        public static IServiceCollection AddFaunaDb(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(x => new FaunaClient(config["FaunaDb:Secret"], config["FaunaDb:Endpoint"]));
            services.AddSingleton<IRecipeRepository, FaunaDbRecipeRepository>();
            return services;
        }
    }
}