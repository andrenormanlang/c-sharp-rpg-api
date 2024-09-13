using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactSharpRPG.Models
{
    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ClassId { get; set; }

        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        // New field to reference the User who created the character
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        // Other properties...
    }
}
