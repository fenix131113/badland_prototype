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
        
        private bool _isGrounded;
        
        private PlayerInput _playerInput;
        private PlayerSettingsSO _playerSettings;

        [Inject]
        private void Construct(PlayerInput playerInput, PlayerSettingsSO playerSettings)
        {
            _playerInput = playerInput;
            _playerSettings = playerSettings;
        }

        private void Awake() => Bind();

        private void OnDestroy() => Expose();

        private void FixedUpdate() => CheckGround();

        private void CheckGround()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.up, groundCheckDistance, groundLayer);

            _isGrounded = hit;
            
            CheckRotation();
        }

        private void CheckRotation()
        {
            if(_isGrounded)
                return;
            
            rb.freezeRotation = _isGrounded;
            
            switch (transform.rotation.z)
            {
                case > -.15f and < .15f:
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case < 0:
                    transform.eulerAngles += new Vector3(0f, 0f, rotateSpeed);
                    break;
                default:
                    transform.eulerAngles -= new Vector3(0f, 0f, rotateSpeed);
                    break;
            }
        }

        private void Jump()
        {
            rb.AddForce(Vector2.up * _playerSettings.PlayerJumpForce, ForceMode2D.Impulse);

            rb.velocity = new Vector2(rb.velocity.x,
                Mathf.Clamp(rb.velocity.y, -_playerSettings.PlayerMaxJumpForce, _playerSettings.PlayerMaxJumpForce));
        }

        private void Move(Vector2 movement)
        {
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

        private void Bind()
        {
            _playerInput.OnJumpInput += Jump;
            _playerInput.OnMoveInput += Move;
        }

        private void Expose()
        {
            _playerInput.OnJumpInput -= Jump;
            _playerInput.OnMoveInput -= Move;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * groundCheckDistance);
        }
    }
}