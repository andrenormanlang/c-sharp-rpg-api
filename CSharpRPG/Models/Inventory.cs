using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ReactSharpRPG.Models
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string CharacterId { get; set; } // Reference to the character
        public List<ItemEntry> Items { get; set; } = new List<ItemEntry>(); // A list of item entries (an item with a quantity)
        public int Gold { get; set; } = 0; // Optionally track gold or currency
    }

    // A helper class to track item and its quantity
    public class ItemEntry
    {
        public Item Item { get; set; } // Reference to the actual item object
        public int Quantity { get; set; } // Quantity of the item
    }
}
