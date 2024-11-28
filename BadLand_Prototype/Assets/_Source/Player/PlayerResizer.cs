using DG.Tweening;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerResizer : MonoBehaviour
    {
        [SerializeField] private float bigSize;
        [SerializeField] private float bigSizeMass;
        [SerializeField] private float smallSize;
        [SerializeField] private float smallSizeMass;

        private Rigidbody2D _rb;
        private float _defaultSize;
        private float _defaultSizeMass;
        private int _currentSizeIndex;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _defaultSize = transform.localScale.x;
            _defaultSizeMass = _rb.mass;
        }

        public void SetBigger()
        {
            if(_currentSizeIndex == 1)
                return;

            _currentSizeIndex += 1;
            SetSizeByIndex();
        }

        public void SetSmaller()
        {
            if(_currentSizeIndex == -1)
                return;

            _currentSizeIndex -= 1;
            SetSizeByIndex();
        }

        private void SetSizeByIndex()
        {
            var newSize = _currentSizeIndex switch
            {
                -1 => (smallSize, smallSizeMass),
                0 => (_defaultSize, _defaultSizeMass),
                _ => (bigSize, bigSizeMass)
            };
            
            transform.DOScale(newSize.Item1, 0.5f).SetEase(Ease.OutBounce);
            DOTween.To(() => _rb.mass, x => _rb.mass = x, newSize.Item2, 0.5f);
        }
    }
}