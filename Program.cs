using BibliotecaApi.Configurations;
using BibliotecaApi.Data;
using BibliotecaApi.Repositories;
using BibliotecaApi.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(
builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<LivroRepository>();
builder.Services.AddScoped<LivroService>();

builder.Services.AddScoped<EmprestimoRepository>();
builder.Services.AddScoped<EmprestimoService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Lumina Biblioteca API",
        Version = "v1",
        Description = "API REST para gerenciamento de livros e empréstimos usando ASP.NET Core e MongoDB."
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();