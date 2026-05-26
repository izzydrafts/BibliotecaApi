using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BibliotecaApi.Models
{
    public class Livro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public string Genero { get; set; } = null!;

        public int Quantidade { get; set; }
    }
}