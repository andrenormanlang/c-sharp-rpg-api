using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReactSharpRPG.Models
{
    public class Enemy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; } // e.g., Goblin, Dragon, etc.
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int ExperienceReward { get; set; } // Experience awarded when defeated
    }
}
