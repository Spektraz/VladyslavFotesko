using Assets.Scripts.Application;
using DG.Tweening;
using SignalClass;
using UnityEngine;
using Zenject;

namespace MainGame
{
    public class MainGameUiController : Controller<MainGameUiModel>
    {
        private readonly SignalBus m_signalBus;

        public MainGameUiController(MainGameUiModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
            UpdateCoins(SaveManager.LoadInt(GlobalConst.CoinsNow));
        }
        private void HitPlayer()
        {
            AnimateCoinArc(1);
        }
        private void AnimateCoinArc(float duration)
        {
            var start = m_viewModel.StartPos.position;
            var target = m_viewModel.EndPos.position;
            var coin = m_viewModel.CoinsImage.gameObject;
            coin.SetActive(true);
            m_viewModel.ParticleCoins.Play();
            coin.transform.position = start;
            Vector3 controlPoint = (start + target) / 2f;
            controlPoint.y += 0.1f;
            DOTween.To(() => 0f, t =>
            {
                Vector3 m1 = Vector3.Lerp(start, controlPoint, t);
                Vector3 m2 = Vector3.Lerp(controlPoint, target, t);
                coin.transform.position = Vector3.Lerp(m1, m2, t);
            }, 1f, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {              
                var coins = SaveManager.LoadInt(GlobalConst.CoinsNow);
                coins += GlobalConst.HitCoins;
                UpdateCoins(coins);
                SaveManager.Save(GlobalConst.CoinsNow, coins);
                coin.SetActive(false);
            });

        }
        private void UpdateCoins(int count)
        {
            m_viewModel.TextCoins.text = count.ToString();
        }
        protected override void BindEvents()
        {
            m_signalBus.Subscribe<OnHitPlayerSignal>(HitPlayer);

        }
        protected override void UnbindEvents()
        {
            m_signalBus.TryUnsubscribe<OnHitPlayerSignal>(HitPlayer);
  
        }

    }
}