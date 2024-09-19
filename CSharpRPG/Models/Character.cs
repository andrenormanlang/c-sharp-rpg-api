using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpRPG.Models
{
    [BsonIgnoreExtraElements]
    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string ClassId { get; set; }

        public string ClassName { get; set; }

        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        // New attributes
        public int Mana { get; set; }
        public int Stamina { get; set; }
        public int Speed { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
    }


}
