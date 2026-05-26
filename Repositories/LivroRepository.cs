using BibliotecaApi.Models;
using BibliotecaApi.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BibliotecaApi.Repositories
{
    public class LivroRepository
    {
        private readonly IMongoCollection<Livro> _livrosCollection;

        public LivroRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(
                mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbSettings.Value.DatabaseName);

            _livrosCollection = mongoDatabase.GetCollection<Livro>("Livros");
        }

         public async Task<List<Livro>> GetAsync() =>
            await _livrosCollection.Find(_ => true).ToListAsync();
    }
}