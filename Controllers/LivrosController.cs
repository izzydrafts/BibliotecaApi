using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivrosController(LivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Livro>>> Get()
        {
            var livros = await _livroService.GetAsync();

            return Ok(livros);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Livro novoLivro)
        {
            await _livroService.CreateAsync(novoLivro);

            return CreatedAtAction(nameof(Get), new { id = novoLivro.Id }, novoLivro);
        }
        [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Livro>> Get(string id)
    {
    var livro = await _livroService.GetByIdAsync(id);

    if (livro is null)
    {
        return NotFound();
    }

    return Ok(livro);
    }
    
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Livro livroAtualizado)
    {
    var livro = await _livroService.GetByIdAsync(id);

    if (livro is null)
    {
        return NotFound();
    }

    livroAtualizado.Id = livro.Id;

    await _livroService.UpdateAsync(id, livroAtualizado);

    return NoContent();
    }
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
    var livro = await _livroService.GetByIdAsync(id);

    if (livro is null)
    {
        return NotFound();
    }

    await _livroService.RemoveAsync(id);

    return NoContent();
    }
    
    }
}