using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var filter = FilterDefinition<Product>.Empty;
            var cursor = await _context.Products.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(product => product.Id, id);
            var cursor = await _context.Products.FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(product => product.Name, name);
            var cursor = await _context.Products.FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(product => product.Category, categoryName);
            var cursor = await _context.Products.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public Task CreateProduct(Product product)
            => _context.Products.InsertOneAsync(product);

        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var replaced = await _context.Products.ReplaceOneAsync(filter, product);

            return replaced.IsAcknowledged &&
                replaced.MatchedCount == replaced.ModifiedCount;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(product => product.Id, id);
            var deleted = await _context.Products.DeleteOneAsync(filter);

            return deleted.IsAcknowledged &&deleted.DeletedCount > 0;
        }
    }
}
