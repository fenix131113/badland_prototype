using UnityEngine;

namespace DieSystem
{
    public class KillZone : MonoBehaviour
    {
        [SerializeField] private LayerMask interactiveLayer;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            //TODO: Kill player
        }
    }
}