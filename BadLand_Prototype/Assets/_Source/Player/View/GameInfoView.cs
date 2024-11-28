using Core;
using TMPro;
using UnityEngine;
using Zenject;

namespace Player.View
{
    public class GameInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreLabel;
        [SerializeField] private TMP_Text levelLabel;

        private GameStats _gameStats;
        
        [Inject]
        private void Construct(GameStats gameStats) => _gameStats = gameStats;

        private void Start()
        {
            Bind();
            Redraw();
        }

        private void OnDestroy() => Expose();

        private void Redraw()
        {
            scoreLabel.text = "Score: " + _gameStats.Score.ToString();
            levelLabel.text = "Level: " + _gameStats.CurrentLevel.ToString();
        }

        private void Bind()
        {
            _gameStats.OnNextLevel += Redraw;
            _gameStats.OnScoreChanged += Redraw;
        }

        private void Expose()
        {
            _gameStats.OnNextLevel -= Redraw;
            _gameStats.OnScoreChanged -= Redraw;
        }
    }
}