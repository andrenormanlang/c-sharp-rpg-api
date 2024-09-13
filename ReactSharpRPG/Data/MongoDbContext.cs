using MongoDB.Driver;
using ReactSharpRPG.Models;

namespace ReactSharpRPG.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        // Constructor to initialize MongoDB connection and select the database
        public MongoDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        // Collection for Classes
        public IMongoCollection<Class> Classes => _database.GetCollection<Class>("Classes");

        // Collection for Characters
        public IMongoCollection<Character> Characters => _database.GetCollection<Character>("Characters");

        // Collection for Items
        public IMongoCollection<Item> Items => _database.GetCollection<Item>("Items");

        // Collection for Battles
        public IMongoCollection<Battle> Battles => _database.GetCollection<Battle>("Battles");

        // Collection for Enemies
        public IMongoCollection<Enemy> Enemies => _database.GetCollection<Enemy>("Enemies");

        // Collection for Inventory
        public IMongoCollection<Inventory> Inventories => _database.GetCollection<Inventory>("Inventories");

        // Add any other collections based on your models
    }
}

