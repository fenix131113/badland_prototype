using Player;
using UExtra.Layers;
using UnityEngine;

namespace LevelInteractable
{
    public class SizeBonus : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private bool setBigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactableLayer))
                return;

            if (setBigger)
                other.GetComponent<PlayerResizer>().SetBigger();
            else
                other.GetComponent<PlayerResizer>().SetSmaller();
            
            Destroy(gameObject);
        }
    }
}