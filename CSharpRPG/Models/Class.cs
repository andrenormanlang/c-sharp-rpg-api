using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CSharpRPG.Models
{
    public class Class
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }    // The name of the class, e.g., "Warrior", "Mage", "Archer"
        public int BaseHealth { get; set; } // Base health for this class
        public int BaseAttack { get; set; } // Base attack for this class
        public int BaseDefense { get; set; } // Base defense for this class
        public int BaseExperience { get; set; }  // Starting experience points
        public int BaseLevel { get; set; }  // Starting level

        // New Attributes
        public int BaseMana { get; set; }  // Base mana pool for magic-based characters
        public int BaseStamina { get; set; }  // Base stamina for physical actions
        public int BaseSpeed { get; set; }  // Base speed to determine turn order

        public string Description { get; set; } // A description of the class' abilities
    }
}
