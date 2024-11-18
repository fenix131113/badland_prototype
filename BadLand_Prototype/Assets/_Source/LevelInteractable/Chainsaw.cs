using DG.Tweening;
using UnityEngine;

namespace LevelInteractable
{
    public class Chainsaw : MonoBehaviour
    {
        [SerializeField] private Ease ease;
        [SerializeField] private float xMove;
        [SerializeField] private float yMove;
        [SerializeField] private float animTime;

        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
            MoveNext();
        }

        private void MoveNext()
        {
            transform.DOMove(_startPos + new Vector3(xMove, yMove, 0), animTime).SetEase(ease)
                .OnComplete(MoveBack);
        }

        private void MoveBack()
        {
            transform.DOMove(_startPos, animTime).SetEase(ease)
                .OnComplete(MoveNext);
        }
    }
}