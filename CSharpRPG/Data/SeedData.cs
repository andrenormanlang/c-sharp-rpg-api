using MongoDB.Driver;
using ReactSharpRPG.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactSharpRPG.Data
{
    public class SeedData
    {
        private readonly MongoDbContext _context;

        public SeedData(MongoDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Seed Classes
            if (await _context.Classes.CountDocumentsAsync(_ => true) == 0)
            {
                await SeedClassesAsync();
            }

            // Seed Characters
            if (await _context.Characters.CountDocumentsAsync(_ => true) == 0)
            {
                await SeedCharactersAsync();
            }

            // Seed Items
            if (await _context.Items.CountDocumentsAsync(_ => true) == 0)
            {
                await SeedItemsAsync();
            }

            // Seed Enemies
            if (await _context.Enemies.CountDocumentsAsync(_ => true) == 0)
            {
                await SeedEnemiesAsync();
            }

            // Seed Battles
            if (await _context.Battles.CountDocumentsAsync(_ => true) == 0)
            {
                await SeedBattlesAsync();
            }

            // Seed Inventory
            if (await _context.Inventories.CountDocumentsAsync(_ => true) == 0)
            {
                await SeedInventoryAsync();
            }
        }

        private async Task SeedClassesAsync()
        {
            var classes = new List<Class>
            {
                new Class { Name = "Warrior", BaseHealth = 100, BaseAttack = 15, Description = "A strong and resilient fighter." },
                new Class { Name = "Mage", BaseHealth = 80, BaseAttack = 25, Description = "A master of magic with strong attack power." },
                // Add more classes...
            };

            await _context.Classes.InsertManyAsync(classes);
        }

        private async Task SeedCharactersAsync()
        {
            // Fetch class IDs to assign to characters
            var warriorClass = await _context.Classes.Find(c => c.Name == "Warrior").FirstOrDefaultAsync();
            var mageClass = await _context.Classes.Find(c => c.Name == "Mage").FirstOrDefaultAsync();

            var characters = new List<Character>
            {
                new Character { Name = "Thorin", ClassId = warriorClass.Id, Health = warriorClass.BaseHealth, Attack = warriorClass.BaseAttack, Defense = 10, Level = 1 },
                new Character { Name = "Gandalf", ClassId = mageClass.Id, Health = mageClass.BaseHealth, Attack = mageClass.BaseAttack, Defense = 5, Level = 1 },
                // Add more characters...
            };

            await _context.Characters.InsertManyAsync(characters);
        }

        private async Task SeedItemsAsync()
        {
            var items = new List<Item>
            {
                new Item { Name = "Sword", Type = "Weapon", Damage = 15 },
                new Item { Name = "Health Potion", Type = "Consumable", Healing = 25 },
                // Add more items...
            };

            await _context.Items.InsertManyAsync(items);
        }

        private async Task SeedEnemiesAsync()
        {
            var enemies = new List<Enemy>
            {
                new Enemy { Name = "Goblin", Health = 30, Attack = 10 },
                new Enemy { Name = "Orc", Health = 50, Attack = 20 },
                // Add more enemies...
            };

            await _context.Enemies.InsertManyAsync(enemies);
        }

        private async Task SeedBattlesAsync()
        {
            // Fetch enemy IDs
            var goblin = await _context.Enemies.Find(e => e.Name == "Goblin").FirstOrDefaultAsync();
            var orc = await _context.Enemies.Find(e => e.Name == "Orc").FirstOrDefaultAsync();

            var battles = new List<Battle>
    {
        new Battle
        {
            BattleName = "First Encounter",
            Description = "First encounter with a goblin",
            EnemyIds = new List<string> { goblin.Id },
            BattleDate = DateTime.UtcNow
        },
        new Battle
        {
            BattleName = "Orc Invasion",
            Description = "An orc invasion battle",
            EnemyIds = new List<string> { orc.Id },
            BattleDate = DateTime.UtcNow
        }
    };

            await _context.Battles.InsertManyAsync(battles);
        }


        private async Task SeedInventoryAsync()
        {
            // Fetch characters from the database
            var thorin = await _context.Characters.Find(c => c.Name == "Thorin").FirstOrDefaultAsync();
            var gandalf = await _context.Characters.Find(c => c.Name == "Gandalf").FirstOrDefaultAsync();

            // Fetch items from the database
            var sword = await _context.Items.Find(i => i.Name == "Sword").FirstOrDefaultAsync();
            var healthPotion = await _context.Items.Find(i => i.Name == "Health Potion").FirstOrDefaultAsync();

            var inventories = new List<Inventory>
            {
                new Inventory
                {
                    CharacterId = thorin.Id,  // Use the actual Character ID
                    Items = new List<ItemEntry>
                    {
                        new ItemEntry { Item = sword, Quantity = 1 },
                        new ItemEntry { Item = healthPotion, Quantity = 2 },
                        

                    }
                },
                new Inventory
                {
                    CharacterId = gandalf.Id,  // Use the actual Character ID
                    Items = new List<ItemEntry>
                    {
                        new ItemEntry { Item = healthPotion, Quantity = 1 }
                    }
                },
                // Add more inventory records if needed
            };

            await _context.Inventories.InsertManyAsync(inventories);
        }
    }
}
