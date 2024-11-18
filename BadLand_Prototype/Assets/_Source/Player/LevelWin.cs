using Core;
using UnityEngine;
using Zenject;

namespace Player
{
    public class LevelWin : MonoBehaviour
    {
        private GameControl _gameControl;

        [Inject]
        private void Construct(GameControl gameControl)
        {
            _gameControl = gameControl;
        }

        public void NextLevel()
        {
            _gameControl.NextLevel();
        }
    }
}