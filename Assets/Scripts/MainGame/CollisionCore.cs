using DG.Tweening;
using SignalClass;
using UnityEngine;
using Zenject;
namespace MainGame
{
    public class CollisionCore : MonoBehaviour
    {
        [SerializeField] private float bounceDistance = 1.5f;
        [SerializeField] private float bounceDuration = 0.2f;
        [SerializeField] private ParticleSystem collisionEffect;
        [SerializeField] private bool m_isPlayer;
        private SignalBus m_signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            m_signalBus = signalBus;
        }

        private void PlayCollisionEffect()
        {
            if (collisionEffect != null && collisionEffect.gameObject != null)
                collisionEffect.Play();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            {
                Vector3 bounceDir = (transform.position - collision.transform.position).normalized;

                Vector3 targetPos = transform.position + bounceDir * bounceDistance;
                transform.DOKill();
                transform.DOMove(targetPos, bounceDuration)
                         .SetEase(Ease.OutQuad);             
                PlayCollisionEffect();
                if (m_isPlayer)
                    m_signalBus.Fire(new OnHitPlayerSignal());
            }
        }
    }
}