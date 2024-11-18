using UnityEngine;

namespace Player
{
    public class PlayerKiller : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;
        [SerializeField] private Rigidbody2D player;

        public bool IsDead { get; private set; }

        public void Kill()
        {
            if(IsDead)
                return;

            IsDead = true;
            deathParticles.transform.position = player.transform.position;
            deathParticles.Play();
            player.gameObject.SetActive(false);
            player.isKinematic = true;
        }
    }
}