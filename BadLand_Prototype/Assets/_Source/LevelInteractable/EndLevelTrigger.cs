using Player;
using UExtra.Layers;
using UnityEngine;

namespace LevelInteractable
{
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!LayerService.CheckLayersEquality(other.gameObject.layer, interactableLayer))
                return;
            
            other.GetComponent<LevelWin>().NextLevel();
        }
    }
}