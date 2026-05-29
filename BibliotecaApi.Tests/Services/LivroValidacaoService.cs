using BibliotecaApi.Models;

namespace BibliotecaApi.Services;

public class LivroValidacaoService
{
    public bool LivroEhValido(Livro livro)
    {
        return !string.IsNullOrWhiteSpace(livro.Titulo)
            && !string.IsNullOrWhiteSpace(livro.Autor)
            && !string.IsNullOrWhiteSpace(livro.Genero)
            && livro.Quantidade >= 0;
    }
}