using Player;
using UExtra.Layers;
using UnityEngine;

namespace DieSystem
{
    public class KillZone : MonoBehaviour
    {
        [SerializeField] private LayerMask interactiveLayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(LayerService.CheckLayersEquality(other.gameObject.layer, interactiveLayer))
                other.gameObject.GetComponent<PlayerKiller>().Kill();
        }
    }
}