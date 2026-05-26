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
    }
}