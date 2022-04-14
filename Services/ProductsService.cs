using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApi.Models;

namespace WebApi.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductsService(
            IOptions<ProductDatabaseSettings> productDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    productDatabaseSettings.Value.ConnectionString);
                
                var mongoDatabase = mongoClient.GetDatabase(
                    productDatabaseSettings.Value.DatabaseName);
                
                _productsCollection = mongoDatabase.GetCollection<Product>(
                    productDatabaseSettings.Value.ProductsCollectionName);
            }

            public async Task<List<Product>> GetAsync() =>
                await _productsCollection.Find(x => true).ToListAsync();

            public async Task<Product?> GetAsync(string id) =>
                await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            public async Task CreateAsync(Product newProduct) =>
                await _productsCollection.InsertOneAsync(newProduct);

            public async Task UpdateAsync(string id, Product upadateProduct) =>
                await _productsCollection.ReplaceOneAsync(x => x.Id == id, upadateProduct);

            public async Task RemoveAsync(string id) =>
                await _productsCollection.DeleteOneAsync(x => x.Id == id);
                
    }
}