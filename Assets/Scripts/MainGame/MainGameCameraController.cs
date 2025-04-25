using DG.Tweening;
using SignalClass;
using UnityEngine;
using Zenject;

namespace MainGame
{
    public class MainGameCameraController : Controller<MainGameCameraModel>
    {
        private readonly SignalBus m_signalBus;
        private float shakeDuration = 0.2f;
        private float shakeStrength = 0.3f;
        private int shakeVibrato = 10;

        private float bounceScale = 1.05f;
        private float bounceTime = 0.1f;
        private Vector3 originalPos;
        private Vector3 originalScale;
        public MainGameCameraController(MainGameCameraModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
            originalPos = m_viewModel.MainGameCamera.transform.localPosition;
            originalScale = m_viewModel.MainGameCamera.transform.localScale;
        }
        protected override void OnInitialize()
        {
        }          
        private void Hit()
        {
           m_viewModel.MainGameCamera.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato)
                     .OnComplete(() => m_viewModel.MainGameCamera.transform.localPosition = originalPos);
            m_viewModel.MainGameCamera.transform.DOScale(originalScale * bounceScale, bounceTime)
                     .SetLoops(2, LoopType.Yoyo)
                     .OnComplete(() => m_viewModel.MainGameCamera.transform.localScale = originalScale);
        }

        protected override void BindEvents()
        {
            m_signalBus.Subscribe<OnHitPlayerSignal>(Hit);
        }

        protected override void UnbindEvents()
        {
            m_signalBus.TryUnsubscribe<OnHitPlayerSignal>(Hit);

        }

    }
}
