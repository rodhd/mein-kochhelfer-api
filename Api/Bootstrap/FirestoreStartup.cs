using Domain.Recipes;
using FirestoreRepository;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Bootstrap
{
    public static class FirestoreStartup
    {
        public static IServiceCollection AddFirestoreDb(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(x => FirestoreDb.Create(config["FirestoreDb:Project"]));
            services.AddSingleton<IRecipeRepository, FirestoreDbRecipeRepository>();

            return services;
        }
    }
}