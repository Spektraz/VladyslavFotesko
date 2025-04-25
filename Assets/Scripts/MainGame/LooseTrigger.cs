using SignalClass;
using UnityEngine;
using Zenject;

namespace MainGame
{
    public class LooseTrigger : MonoBehaviour
    {
        private SignalBus m_signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            m_signalBus = signalBus;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                m_signalBus.Fire(new OnFinishGameSignal { 
                    isStateResult = false
                });
            }
        }

    }
}
