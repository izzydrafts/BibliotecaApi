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
            public async Task CreateAsync(Livro novoLivro) =>
            await _livrosCollection.InsertOneAsync(novoLivro);

            public async Task<Livro?> GetByIdAsync(string id) =>
            await _livrosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
            public async Task UpdateAsync(string id, Livro livroAtualizado) =>
            await _livrosCollection.ReplaceOneAsync(x => x.Id == id, livroAtualizado);
            
            public async Task RemoveAsync(string id) =>
        await _livrosCollection.DeleteOneAsync(x => x.Id == id);
    }
    
    
}