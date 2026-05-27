using BibliotecaApi.Configurations;
using BibliotecaApi.Data;
using BibliotecaApi.Repositories;
using BibliotecaApi.Services;

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
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();