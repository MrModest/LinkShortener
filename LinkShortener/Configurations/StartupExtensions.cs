using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using LinkShortener.Data;
using LinkShortener.Entities;
using LinkShortener.Repositories;
using LinkShortener.RepositoryImpls;
using LinkShortener.ServiceImpls;
using LinkShortener.Services;

namespace LinkShortener.Configurations
{
    public static class StartupExtensions
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddTransient<ILinkService, LinkService>();
            services.AddTransient<IShortenerService, ShortenerService>();
        }
        
        public static void SetupMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LinksDatabaseSettings>(
                configuration.GetSection(nameof(LinksDatabaseSettings)));

            services.AddSingleton<ILinksDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<LinksDatabaseSettings>>().Value);

            //AddIndexes(configuration);
            
            BsonClassMap.RegisterClassMap<Link>(cm => 
            {
                cm.AutoMap();
                cm.MapProperty(l => l.UserId).SetElementName("UserId");
                cm.MapProperty(l => l.ShortAlias).SetElementName("ShortAlias");
                cm.MapProperty(l => l.FullLink).SetElementName("FullLink");
                cm.MapProperty(l => l.VisitedCount).SetElementName("VisitedCount");
                cm.MapCreator(l => new Link(l.Id, l.UserId, l.ShortAlias, l.FullLink, l.VisitedCount));
            });
        }

        private static void AddIndexes(IConfiguration configuration)
        {
            var databaseSettings = configuration.GetValue<ILinksDatabaseSettings>("LinksDatabaseSettings");
            
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            var linksCollection = database.GetCollection<Link>(databaseSettings.LinksCollectionName);
            
            
            var key = Builders<Link>.IndexKeys.Ascending(l => l.ShortAlias);
            var options = new CreateIndexOptions { Unique = true };
            var model = new CreateIndexModel<Link>(key, options);

            linksCollection.Indexes.CreateOne(model);
        }
    }
}