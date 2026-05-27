using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services
{
    public class LivroService
    {
        private readonly LivroRepository _livroRepository;

        public LivroService(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<List<Livro>> GetAsync()
        {
            return await _livroRepository.GetAsync();
        }
        public async Task CreateAsync(Livro novoLivro)
        {
        await _livroRepository.CreateAsync(novoLivro);
        }
        public async Task<Livro?> GetByIdAsync(string id)
        {
        return await _livroRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(string id, Livro livroAtualizado)
        {
        await _livroRepository.UpdateAsync(id, livroAtualizado);
        }

        public async Task RemoveAsync(string id)
        {
        await _livroRepository.RemoveAsync(id);
        }

    }

}