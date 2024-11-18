using System;
using Core;
using Player;
using UnityEngine;
using Zenject;

namespace CameraSystem
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Vector3 _startPosition;
        private GameStats _gameStats;
        private PlayerKiller _playerKiller;

        [Inject]
        private void Construct(GameStats gameStats, PlayerKiller playerKiller)
        {
            _gameStats = gameStats;
            _playerKiller = playerKiller;
        }

        private void Awake()
        {
            _startPosition = transform.position;
            
            Bind();
        }

        private void OnDestroy() => Expose();

        private void FixedUpdate()
        {
            if (!_gameStats.IsGamePaused && !_playerKiller.IsDead)
                transform.position += transform.right * (speed * Time.fixedDeltaTime);
        }

        private void ResetCameraPosition() => transform.position = _startPosition;

        private void Bind() => _gameStats.OnNextLevel += ResetCameraPosition;

        private void Expose() => _gameStats.OnNextLevel -= ResetCameraPosition;
    }
}