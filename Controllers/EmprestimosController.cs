using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmprestimosController : ControllerBase
{
    private readonly EmprestimoService _emprestimoService;

    public EmprestimosController(EmprestimoService emprestimoService)
    {
        _emprestimoService = emprestimoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Emprestimo>>> Get()
    {
        var emprestimos = await _emprestimoService.GetAsync();

        return Ok(emprestimos);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Emprestimo>> Get(string id)
    {
        var emprestimo = await _emprestimoService.GetByIdAsync(id);

        if (emprestimo is null)
        {
            return NotFound();
        }

        return Ok(emprestimo);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Emprestimo emprestimo)
    {
        await _emprestimoService.CreateAsync(emprestimo);

        return CreatedAtAction(nameof(Get), new { id = emprestimo.Id }, emprestimo);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Emprestimo emprestimo)
    {
        var emprestimoExistente = await _emprestimoService.GetByIdAsync(id);

        if (emprestimoExistente is null)
        {
            return NotFound();
        }

        emprestimo.Id = emprestimoExistente.Id;

        await _emprestimoService.UpdateAsync(id, emprestimo);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var emprestimo = await _emprestimoService.GetByIdAsync(id);

        if (emprestimo is null)
        {
            return NotFound();
        }

        await _emprestimoService.RemoveAsync(id);

        return NoContent();
    }
}