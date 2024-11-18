using Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        
        private GameStats _gameStats;

        [Inject]
        private void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        private void Start() => Bind();

        private void OnDestroy() => Expose();

        private void SwitchPauseMenu(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
            
            pauseMenu.SetActive(state);
        }

        private void Bind()
        {
            _gameStats.OnGamePauseStateChanged += SwitchPauseMenu;
            resumeButton.onClick.AddListener(_gameStats.ResumeGame);
            restartButton.onClick.AddListener(GameControl.RestartGame);
        }

        private void Expose()
        {
            _gameStats.OnGamePauseStateChanged -= SwitchPauseMenu;
            resumeButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
        }
    }
}