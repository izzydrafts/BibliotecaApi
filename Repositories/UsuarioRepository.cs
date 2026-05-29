using BibliotecaApi.Configurations;
using BibliotecaApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BibliotecaApi.Repositories;

public class UsuarioRepository
{
    private readonly IMongoCollection<Usuario> _usuariosCollection;

    public UsuarioRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(
            mongoDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoDbSettings.Value.DatabaseName);

        _usuariosCollection = mongoDatabase.GetCollection<Usuario>("Usuarios");
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _usuariosCollection
            .Find(usuario => usuario.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Usuario usuario)
    {
        await _usuariosCollection.InsertOneAsync(usuario);
    }
}