using Player;
using UExtra.Layers;
using UnityEngine;

namespace LevelInteractable
{
    public class FallObjectKill : MonoBehaviour
    {
        [SerializeField] private LayerMask triggerLayer;
        [SerializeField] private LayerMask groundLayer;

        private PlayerMovement _playerMovement;
        private bool _isPlayerInZone;
        private bool _isFalling = true;
        private bool _onGround;

        private void Update()
        {
            if (!_isPlayerInZone || !_playerMovement || !_playerMovement.IsGrounded) return;
            
            _playerMovement.GetComponent<PlayerKiller>().Kill();
            _playerMovement = null;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (LayerService.CheckLayersEquality(other.gameObject.layer, groundLayer))
            {
                _onGround = true;
                
                if (!_isPlayerInZone)
                    _isFalling = false;
            }

            if (!LayerService.CheckLayersEquality(other.gameObject.layer, triggerLayer) || !_isFalling) return;

            _isPlayerInZone = true;

            if (!_playerMovement)
                _playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!LayerService.CheckLayersEquality(other.gameObject.layer, triggerLayer)) return;
            
            _isPlayerInZone = false;
            if(_onGround)
                _isFalling = false;
        }
    }
}