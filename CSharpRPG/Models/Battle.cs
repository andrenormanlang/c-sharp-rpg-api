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

        public string BattleName { get; set; }    // Name of the battle
        public string Description { get; set; }   // Brief description of the battle

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> EnemyIds { get; set; } // List of enemy IDs that are part of the battle

        public DateTime BattleDate { get; set; } = DateTime.UtcNow; // Time the battle occurred
    }
}
