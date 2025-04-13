using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetCrud.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}