using CSharpRPG.Models;

namespace CSharpRPG.Dtos
{
    public class CharacterWithClassDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int Mana { get; set; }
        public int Stamina { get; set; }
        public int Speed { get; set; }
        public string UserId { get; set; }

        // Constructor to map fields
        public CharacterWithClassDto(Character character, Class classEntity)
        {
            Id = character.Id;
            Name = character.Name;
            ClassId = character.ClassId;
            ClassName = classEntity?.Name ?? "Unknown"; // Populate ClassName from the Class entity
            Health = character.Health;
            Attack = character.Attack;
            Defense = character.Defense;
            Experience = character.Experience;
            Level = character.Level;
            Mana = character.Mana;
            Stamina = character.Stamina;
            Speed = character.Speed;
            UserId = character.UserId;
        }
    }
}
