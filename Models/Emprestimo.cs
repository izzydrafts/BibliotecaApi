using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BibliotecaApi.Models;

public class Emprestimo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string NomeUsuario { get; set; } = null!;

    public string LivroId { get; set; } = null!;

    public DateTime DataEmprestimo { get; set; }

    public DateTime DataDevolucao { get; set; }
}