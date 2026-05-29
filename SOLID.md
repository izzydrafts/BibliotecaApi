
# Aplicação dos Princípios SOLID

Este projeto aplica princípios SOLID na organização do backend da API Lumina Biblioteca.

-------

## 1. SRP — Single Responsibility Principle

O princípio da responsabilidade única foi aplicado separando o sistema em camadas.

### Onde aparece

- `Controllers/LivrosController.cs`
- `Controllers/EmprestimosController.cs`
- `Services/LivroService.cs`
- `Services/EmprestimoService.cs`
- `Repositories/LivroRepository.cs`
- `Repositories/EmprestimoRepository.cs`
- `Models/Livro.cs`
- `Models/Emprestimo.cs`

### Justificativa

Cada classe possui uma responsabilidade principal:

- Controllers recebem as requisições HTTP e retornam respostas.
- Services concentram regras de negócio.
- Repositories fazem a comunicação com o MongoDB.
- Models representam as entidades do sistema.

Essa separação evita que uma única classe concentre muitas funções diferentes.

------

## 2. DIP — Dependency Inversion Principle

O princípio da inversão de dependência foi aplicado com o uso de injeção de dependência no `Program.cs`.

### Onde aparece

- `Program.cs`
- `LivrosController.cs`
- `EmprestimosController.cs`
- `LivroService.cs`
- `EmprestimoService.cs`

### Justificativa

As classes recebem suas dependências pelo construtor, em vez de criarem objetos diretamente.

Exemplo:

```csharp
public LivrosController(LivroService livroService)
{
    _livroService = livroService;
}

```

E no `Program.cs` os serviços são registrados:

```csharp
builder.Services.AddScoped<LivroRepository>();
builder.Services.AddScoped<LivroService>();

builder.Services.AddScoped<EmprestimoRepository>();
builder.Services.AddScoped<EmprestimoService>();
```
Isso reduz o acoplamento entre as classes e facilita manutenção e testes.

-------

## 3. OCP — Open/Closed Principle

O princípio aberto/fechado aparece na organização da API em camadas.

### Onde aparece

* `Models`
* `Repositories`
* `Services`
* `Controllers`

### Justificativa

A estrutura permite adicionar novas entidades sem alterar drasticamente o código já existente.

Por exemplo, após criar a entidade `Livro`, foi possível adicionar a entidade `Emprestimo` criando novos arquivos próprios:

* `Emprestimo.cs`
* `EmprestimoRepository.cs`
* `EmprestimoService.cs`
* `EmprestimosController.cs`

A entidade nova foi adicionada estendendo o sistema, sem precisar modificar diretamente a estrutura principal da entidade `Livro`.

---

## Conclusão

O projeto aplica pelo menos três princípios SOLID:

* SRP: separação clara de responsabilidades.
* DIP: uso de injeção de dependência.
* OCP: possibilidade de expansão com novas entidades.

Essas decisões tornam o backend mais organizado, fácil de manter e mais simples de explicar durante a apresentação.

```

