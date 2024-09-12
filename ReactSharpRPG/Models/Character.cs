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

        // Reference to the character's class by ClassId
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClassId { get; set; }

        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        // Reference to the character's inventory
        public Inventory Inventory { get; set; }

        // This could be expanded to include abilities, skills, etc.
    }
}
