using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpRPG.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } // e.g., Sword, Shield, Health Potion
        public string Type { get; set; } // Weapon, Armor, Consumable
        public int Damage { get; set; } // If it's a weapon
        public int Healing { get; set; } // If it's a consumable
        public int Defense { get; set; } // If it's armor
    }
}
