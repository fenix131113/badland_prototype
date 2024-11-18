using Core;
using DG.Tweening;
using Player.Data;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private float rotateSpeed;

        public bool IsGrounded { get; private set; }

        private bool _rotated;
        private Tween _rotateTween;
        private Vector3 _startPosition;
        private PlayerInput _playerInput;
        private PlayerSettingsSO _playerSettings;
        private PlayerKiller _playerKiller;
        private GameStats _gameStats;

        [Inject]
        private void Construct(PlayerInput playerInput, PlayerSettingsSO playerSettings, PlayerKiller playerKiller,
            GameStats gameStats)
        {
            _playerInput = playerInput;
            _playerSettings = playerSettings;
            _playerKiller = playerKiller;
            _gameStats = gameStats;
        }

        private void Awake()
        {
            _startPosition = transform.position;

            Bind();
        }

        private void OnDestroy() => Expose();

        private void Update() => CheckGround();

        private void CheckGround()
        {
            if (_gameStats.IsGamePaused)
                return;

            var hit = Physics2D.Raycast(transform.position, Vector2.up, groundCheckDistance, groundLayer);

            IsGrounded = hit;
            
            CheckRotation();
        }

        private void CheckRotation()
        {
            rb.freezeRotation = !IsGrounded;
            
            if (IsGrounded)
            {
                _rotateTween?.Kill();
                _rotated = false;
                return;
            }

            if (_rotated || IsGrounded) return;
            
            _rotated = true;
            _rotateTween = transform.DORotate(Vector3.zero, .75f).SetEase(Ease.OutBack);
        }

        private void Jump()
        {
            if (_playerKiller.IsDead || _gameStats.IsGamePaused)
                return;

            rb.AddForce(Vector2.up * _playerSettings.PlayerJumpForce, ForceMode2D.Impulse);

            rb.velocity = new Vector2(rb.velocity.x,
                Mathf.Clamp(rb.velocity.y, -_playerSettings.PlayerMaxJumpForce, _playerSettings.PlayerMaxJumpForce));
        }

        private void Move(Vector2 movement)
        {
            if (_playerKiller.IsDead || _gameStats.IsGamePaused)
                return;

            var moveVector =
                new Vector2(
                    movement.x * _playerSettings.PlayerSpeed /
                    (_playerSettings.PlayerSpeed * _playerSettings.PlayerXStoppingResistance), rb.velocity.y);

            if (moveVector.magnitude > 0)
                rb.velocity += new Vector2(moveVector.x, 0);

            rb.velocity =
                new Vector2(Mathf.Clamp(rb.velocity.x, -_playerSettings.PlayerMaxSpeed, _playerSettings.PlayerMaxSpeed),
                    rb.velocity.y);
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
        }

        private void OnPauseStateChanged(bool state)
        {
            rb.simulated = !state;
        }

        private void Bind()
        {
            _playerInput.OnJumpInput += Jump;
            _playerInput.OnMoveInput += Move;
            _gameStats.OnGamePauseStateChanged += OnPauseStateChanged;
        }

        private void Expose()
        {
            _playerInput.OnJumpInput -= Jump;
            _playerInput.OnMoveInput -= Move;
            _gameStats.OnGamePauseStateChanged -= OnPauseStateChanged;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * groundCheckDistance);
        }
    }
}