using ReactSharpRPG.Models;

namespace ReactSharpRPG.Utilities
{
    public static class GameUtilities
    {
        // Calculate the damage dealt to an enemy considering attacker's attack and defender's defense
        public static int CalculateDamage(int attackerAttack, int defenderDefense)
        {
            int damage = attackerAttack - defenderDefense;
            return damage > 0 ? damage : 0; // Ensure damage is never negative
        }

        // Calculate the experience required to reach the next level
        public static int CalculateExperienceForNextLevel(int currentLevel)
        {
            return currentLevel * 100; // Simple formula: level * 100 experience points
        }

        // Handle the leveling up process
        public static void LevelUp(Character character)
        {
            character.Level += 1;
            character.Health += 20; // Increase health by 20 each level
            character.Attack += 5;  // Increase attack by 5 each level
            character.Defense += 3; // Increase defense by 3 each level
        }
    }
}
