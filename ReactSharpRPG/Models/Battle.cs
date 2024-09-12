using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ReactSharpRPG.Models
{
    public class Battle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string CharacterId { get; set; } // Reference to the player's character
        public string EnemyId { get; set; } // Reference to the enemy
        public string BattleName { get; set; }    // Name of the battle
        public List<string> Characters { get; set; }    // List of character names participating in the battle
        public List<string> BattleLog { get; set; } = new List<string>(); // Logs for each battle round
        public bool IsVictory { get; set; } // True if the player won the battle
        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Time the battle occurred
        public List<Enemy> Enemies { get; set; }    // List of enemies in the battle
    }
}
