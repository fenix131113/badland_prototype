using Services;
using UnityEngine;

namespace LevelInteractable
{
    [RequireComponent(typeof(Animator))]
    public class Spike : MonoBehaviour
    {
        private static readonly int _growAnimKey = Animator.StringToHash("Grow");

        [SerializeField] private LayerMask playerLayer;
        
        private Animator _anim;
        private bool _isGrow;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void GrowSpike()
        {
            if (_isGrow)
                return;
            
            _anim.SetTrigger(_growAnimKey);
            _isGrow = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(LayerService.CheckLayersEquality(other.gameObject.layer, playerLayer))
                GrowSpike();
        }
    }
}