using Catalog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configurations)
        {
            var connectionString = configurations.GetValue<string>("DatabaseSettings:ConnectionString");
            var databaseName = configurations.GetValue<string>("DatabaseSettings:DatabaseName");
            var productsCollectionName = configurations.GetValue<string>("DatabaseSettings:CollectionName");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(productsCollectionName);

            CatalogContextSeed.SeedData(Products);
        }
    }
}
