using LevelGenerationSystem;
using Player;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core
{
    public class GameControl
    {
        private readonly GameStats _gameStats;
        private readonly LevelGeneration _levelGeneration;
        private readonly PlayerMovement _playerMovement;

        [Inject]
        public GameControl(GameStats gameStats, LevelGeneration levelGeneration, PlayerMovement playerMovement)
        {
            _gameStats = gameStats;
            _levelGeneration = levelGeneration;
            _playerMovement = playerMovement;
        }

        public static void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel()
        {
            _playerMovement.ResetPosition();
            _gameStats.IncreaseLevel();
            _levelGeneration.Rebuild();
        }
    }
}