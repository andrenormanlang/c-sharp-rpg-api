using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactSharpRPG.Models
{
    public class Class
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }    // The name of the class, e.g., "Warrior", "Mage", "Archer"
        public int BaseHealth { get; set; } // Base health for this class
        public int BaseAttack { get; set; } // Base attack for this class
        public string Description { get; set; } // A description of the class' abilities
    }
}
