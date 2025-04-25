using SignalClass;
using UnityEngine;
using Zenject;

public class CollisionFinish : MonoBehaviour
{
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            if (m_isPlayer)
            {
                PlayCollisionEffect();
                m_signalBus.Fire(new OnFinishGameSignal
                {
                    isStateResult = false
                });
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            if (!m_isPlayer)
            {
                PlayCollisionEffect();
                m_signalBus.Fire(new OnFinishGameSignal
                {
                    isStateResult = true
                });
            }
        }
    }
}
