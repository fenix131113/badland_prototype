using Core;
using UnityEngine;
using Zenject;

namespace Player
{
    public class GameStatsLink : MonoBehaviour
    {
        private GameStats _gameStats;

        [Inject]
        private void Construct(GameStats gameStats)
        {
            _gameStats = gameStats;
        }

        public void AddScore(int score)
        {
            _gameStats.AddScore(score);
        }
    }
}