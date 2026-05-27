using BibliotecaApi.Models;
using BibliotecaApi.Data;
using MongoDB.Driver;

namespace BibliotecaApi.Repositories;

public class EmprestimoRepository
{
    private readonly IMongoCollection<Emprestimo> _emprestimos;

    public EmprestimoRepository(MongoDbContext context)
    {
        _emprestimos = context.GetCollection<Emprestimo>("Emprestimos");
    }

    public async Task<List<Emprestimo>> GetAsync() =>
        await _emprestimos.Find(_ => true).ToListAsync();

    public async Task<Emprestimo?> GetByIdAsync(string id) =>
        await _emprestimos.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Emprestimo emprestimo) =>
        await _emprestimos.InsertOneAsync(emprestimo);

    public async Task UpdateAsync(string id, Emprestimo emprestimo) =>
        await _emprestimos.ReplaceOneAsync(x => x.Id == id, emprestimo);

    public async Task RemoveAsync(string id) =>
        await _emprestimos.DeleteOneAsync(x => x.Id == id);
}