using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void FixedUpdate()
        {
            transform.position += transform.right * (speed * Time.fixedDeltaTime);
        }
    }
}