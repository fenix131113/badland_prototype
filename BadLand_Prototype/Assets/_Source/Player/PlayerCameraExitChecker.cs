using Zenject;
using UnityEngine;

namespace Player
{
    public class PlayerCameraExitChecker : IInitializable, IFixedTickable
    {
        private Camera _camera;
        private readonly PlayerMovement _playerMovement;

        public PlayerCameraExitChecker(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
        
        public void Initialize()
        {
            _camera = Camera.main;
        }
        
        public void FixedTick()
        {
            var maxPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            var minPoint = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));

            if (_playerMovement.transform.position.x < minPoint.x || _playerMovement.transform.position.x > maxPoint.x)
                _playerMovement.GetComponent<PlayerKiller>().Kill();
        }
    }
}