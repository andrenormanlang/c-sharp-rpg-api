using CSharpRPG.Models;
using CSharpRPG.Repositories;
using CSharpRPG.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpRPG.Services
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _battleRepository;
        private readonly ICharacterService _characterService;
        private readonly IEnemyService _enemyService;

        public BattleService(IBattleRepository battleRepository, ICharacterService characterService, IEnemyService enemyService)
        {
            _battleRepository = battleRepository;
            _characterService = characterService;
            _enemyService = enemyService;
        }

        // Simulates a battle between a character and an enemy based on the battleId
        public async Task<BattleResult> ExecuteBattle(string characterId, string battleId)
        {
            var character = await _characterService.GetCharacterByIdAsync(characterId);
            var battle = await _battleRepository.GetBattleByIdAsync(battleId);

            if (character == null || battle == null)
                throw new Exception("Character or Battle not found.");

            // Fetch enemies based on EnemyIds in the battle
            var enemies = new List<Enemy>();
            foreach (var enemyId in battle.EnemyIds)
            {
                var enemy = await _enemyService.GetEnemyByIdAsync(enemyId);
                if (enemy != null)
                {
                    enemies.Add(enemy);
                }
            }

            // Assuming you want to fight the first enemy
            var enemyToFight = enemies.FirstOrDefault();
            if (enemyToFight == null)
                throw new Exception("Enemy not found.");

            // Simulate dice rolls for both player and enemy
            var playerRoll = RandomNumberGenerator.GenerateRandomNumber(1, 6);
            var enemyRoll = RandomNumberGenerator.GenerateRandomNumber(1, 6);

            string result;

            // Determine battle outcome
            if (playerRoll > enemyRoll)
            {
                result = "win";
                character.Experience += enemyToFight.ExperienceReward;

                // Level up the character if experience crosses threshold
                if (character.Experience >= GameUtilities.CalculateExperienceForNextLevel(character.Level))
                {
                    GameUtilities.LevelUp(character);
                }
            }
            else if (playerRoll < enemyRoll)
            {
                result = "lose";
                character.Experience = Math.Max(0, character.Experience - 10); // Lose 10 experience on defeat
            }
            else
            {
                result = "tie";
            }

            await _characterService.UpdateCharacterAsync(character.Id, character);

            return new BattleResult
            {
                PlayerRoll = playerRoll,
                EnemyRoll = enemyRoll,
                Result = result,
                UpdatedCharacter = character
            };
        }

        public async Task<IEnumerable<Battle>> GetBattlesAsync()
        {
            return await _battleRepository.GetAllBattlesAsync();
        }

        public async Task<Battle> GetBattleByIdAsync(string id)
        {
            return await _battleRepository.GetBattleByIdAsync(id);
        }

        public async Task CreateBattleAsync(Battle battle)
        {
            await _battleRepository.CreateBattleAsync(battle);
        }

        public async Task<bool> UpdateBattleAsync(string id, Battle updatedBattle)
        {
            return await _battleRepository.UpdateBattleAsync(id, updatedBattle);
        }

        public async Task<bool> DeleteBattleAsync(string id)
        {
            return await _battleRepository.DeleteBattleAsync(id);
        }
    }

    // Define an interface for the repository
    public interface IBattleService
    {
        Task<IEnumerable<Battle>> GetBattlesAsync();
        Task<Battle> GetBattleByIdAsync(string id);
        Task<BattleResult> ExecuteBattle(string characterId, string battleId);
        Task CreateBattleAsync(Battle battle);
        Task<bool> UpdateBattleAsync(string id, Battle updatedBattle);
        Task<bool> DeleteBattleAsync(string id);
    }

    public class BattleResult
    {
        public int PlayerRoll { get; set; }
        public int EnemyRoll { get; set; }
        public string Result { get; set; }
        public Character UpdatedCharacter { get; set; }
    }
}
