using Player;
using UExtra.Layers;
using UnityEngine;

namespace LevelInteractable
{
    public class FallObjectTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask triggerLayer;
        [SerializeField] private Rigidbody2D fallObject;

        private PlayerMovement _playerMovement;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!LayerService.CheckLayersEquality(collision.gameObject.layer, triggerLayer)) return;
            
            fallObject.isKinematic = false;
        }
    }
}