using Domain.Ingredients;
using Domain.Recipes;
using FaunaDB.Client;
using FaunaRepository.Recipes;
using Microsoft.Extensions.DependencyInjection;


namespace Api.Bootstrap
{
    public static class FaunaStartup
    {
        static readonly string ENDPOINT = "https://db.fauna.com:443";
        static readonly string SECRET = "fnAEFAozOHACB8I0eyU-T82QJLTqCG5kaRFdp5Dq";

        public static IServiceCollection AddFaunaDb(this IServiceCollection services)
        {
            services.AddSingleton(x => new FaunaClient(SECRET, ENDPOINT));
            services.AddSingleton<IRecipeRepository, FaunaDbRecipeRepository>();
            return services;
        }
    }
}