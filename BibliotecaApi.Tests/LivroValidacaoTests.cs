using BibliotecaApi.Models;
using BibliotecaApi.Services;

namespace BibliotecaApi.Tests;

public class LivroValidacaoTests
{
    private readonly LivroValidacaoService _validacaoService = new LivroValidacaoService();

    [Fact]
    public void Livro_ComDadosValidos_DeveRetornarVerdadeiro()
    {
        var livro = new Livro
        {
            Titulo = "O Acordo",
            Autor = "Elle Kennedy",
            Genero = "Romance",
            Quantidade = 5
        };

        var resultado = _validacaoService.LivroEhValido(livro);

        Assert.True(resultado);
    }

    [Fact]
    public void Livro_ComQuantidadeZero_DeveRetornarVerdadeiro()
    {
        var livro = new Livro
        {
            Titulo = "Livro Reservado",
            Autor = "Autor Teste",
            Genero = "Teste",
            Quantidade = 0
        };

        var resultado = _validacaoService.LivroEhValido(livro);

        Assert.True(resultado);
    }

    [Fact]
    public void Livro_SemTitulo_DeveRetornarFalso()
    {
        var livro = new Livro
        {
            Titulo = "",
            Autor = "Autor Teste",
            Genero = "Teste",
            Quantidade = 3
        };

        var resultado = _validacaoService.LivroEhValido(livro);

        Assert.False(resultado);
    }

    [Fact]
    public void Livro_ComQuantidadeNegativa_DeveRetornarFalso()
    {
        var livro = new Livro
        {
            Titulo = "Livro Teste",
            Autor = "Autor Teste",
            Genero = "Teste",
            Quantidade = -1
        };

        var resultado = _validacaoService.LivroEhValido(livro);

        Assert.False(resultado);
    }
}