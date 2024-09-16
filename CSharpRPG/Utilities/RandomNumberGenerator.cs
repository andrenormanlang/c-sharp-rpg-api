using System;

namespace CSharpRPG.Utilities
{
    public static class RandomNumberGenerator
    {
        private static readonly Random _random = new Random();

        // Generate a random number between min and max (inclusive)
        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max + 1); // +1 to make the max inclusive
        }

        // 50% chance to return true (used for hit/miss calculations)
        public static bool FiftyFiftyChance()
        {
            return _random.Next(0, 2) == 0;
        }

        // Generate a random critical hit multiplier (e.g., 1x to 3x)
        public static double GenerateCriticalHitMultiplier()
        {
            return _random.NextDouble() * (3.0 - 1.0) + 1.0; // Random value between 1.0 and 3.0
        }
    }
}
