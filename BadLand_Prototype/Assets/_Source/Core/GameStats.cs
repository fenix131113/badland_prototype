using System;
using Player;

namespace Core
{
    public class GameStats
    {
        public int CurrentLevel { get; private set; } = 1;
        public int Score { get; private set; }
        public bool IsGamePaused { get; private set; }
        
        public event Action<bool> OnGamePauseStateChanged;
        public event Action OnNextLevel;
        public event Action OnScoreChanged;
        
        private readonly PlayerInput _playerInput;

        public GameStats(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            Bind();
        }

        ~GameStats() => Expose();

        public void IncreaseLevel()
        {
            CurrentLevel++;
            AddScore(100);
            OnNextLevel?.Invoke();
        }

        public void AddScore(int score)
        {
            Score += score;
            OnScoreChanged?.Invoke();
        }

        public void PauseGame()
        {
            IsGamePaused = true;
            OnGamePauseStateChanged?.Invoke(IsGamePaused);
        }

        public void ResumeGame()
        {
            IsGamePaused = false;
            OnGamePauseStateChanged?.Invoke(IsGamePaused);
        }
        
        private void SwitchPause()
        {
            if(IsGamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        private void Bind()
        {
            _playerInput.OnPauseInput += SwitchPause;
        }

        private void Expose()
        {
            _playerInput.OnPauseInput -= SwitchPause;
        }
    }
}