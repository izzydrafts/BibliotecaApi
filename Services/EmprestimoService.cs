using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services;

public class EmprestimoService
{
    private readonly EmprestimoRepository _repository;

    public EmprestimoService(EmprestimoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Emprestimo>> GetAsync() =>
        await _repository.GetAsync();

    public async Task<Emprestimo?> GetByIdAsync(string id) =>
        await _repository.GetByIdAsync(id);

    public async Task CreateAsync(Emprestimo emprestimo) =>
        await _repository.CreateAsync(emprestimo);

    public async Task UpdateAsync(string id, Emprestimo emprestimo) =>
        await _repository.UpdateAsync(id, emprestimo);

    public async Task RemoveAsync(string id) =>
        await _repository.RemoveAsync(id);
}