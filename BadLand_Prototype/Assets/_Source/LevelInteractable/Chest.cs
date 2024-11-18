using Player;
using UExtra.Layers;
using UnityEngine;

namespace LevelInteractable
{
    public class Chest : MonoBehaviour
    {
        private static readonly int _open = Animator.StringToHash("Open");
        
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private Animator anim;
        [SerializeField] private int score;

        private GameStatsLink _gameStatsLink;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerService.CheckLayersEquality(other.gameObject.layer, interactableLayer))
                return;
            
            _gameStatsLink = other.GetComponent<GameStatsLink>();
            
            OpenChest();
        }

        private void OpenChest()
        {
            anim.SetTrigger(_open);
            
            _gameStatsLink.AddScore(score);
        }
    }
}