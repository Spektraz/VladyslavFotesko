using Assets.Scripts.Application;
using DG.Tweening;
using SignalClass;
using System;
using UnityEngine;
using Zenject;

namespace Daily
{
    public class DailyController : Controller<DailyModel>
    {
        private readonly SignalBus m_signalBus;
        private int m_count;
        private DateTime m_lastRewardTime;

        public DailyController(SignalBus signalBus, DailyModel viewModel) : base(viewModel) 
        {
            m_signalBus = signalBus;
            SetCoins();
            if (HasMissedReward())
            {
                SaveManager.Save(GlobalConst.CountDaily, 0);
                m_lastRewardTime = DateTime.Now;
                PlayerPrefs.SetString(GlobalConst.PresentDaily, m_lastRewardTime.ToString());
                PlayerPrefs.Save();
            }       
        }
        private void SetReload()
        {
            m_viewModel.CoinsText.text = SaveManager.LoadInt(GlobalConst.CoinsNow).ToString();
            m_count = SaveManager.LoadInt(GlobalConst.CountDaily);
            if (m_count == 0)
            {
               for(int i = 0; i < m_viewModel.DailyItems.Count; i++)
                {
                    m_viewModel.DailyItems[i].SetInfo(false);
                }
            }
        }
        protected override void OnInitialize()
        {            
            m_viewModel.BackButton.onClick.AddListener(SetBackButton);
            m_viewModel.GetButton.onClick.AddListener(GetBonus);
            for (int i = 0; i < m_viewModel.DailyItems.Count; i++)
            {
                var isPassedDay = i < m_count;
                var isToday = i == m_count;
                m_viewModel.DailyItems[i].SetText(m_viewModel.DailyBonusSO[i].coinCount.ToString());


                if (isPassedDay)
                    m_viewModel.DailyItems[i].SetInfo(true); 
                else
                    m_viewModel.DailyItems[i].SetInfo(false);
            }
            ShowDaily();
        }
        private void ShowDaily()
        {
            if (CanClaimReward())
            {
                m_signalBus.Fire(new OnCloseDailySignal());
            }
        }
        protected override void OnDispose()
        {
            m_viewModel.BackButton.onClick.RemoveListener(SetBackButton);
            m_viewModel.GetButton.onClick.RemoveListener(GetBonus);
        }
        private void SetCoins()
        {
            m_viewModel.CoinsText.text = SaveManager.LoadInt(GlobalConst.CoinsNow).ToString();
            m_count = SaveManager.LoadInt(GlobalConst.CountDaily);
            LoadLastRewardTime();
        }
        private void SetBackButton()
        {
            m_signalBus.Fire(new OnCloseDailySignal());            
        }
        private void LoadLastRewardTime()
        {
            string lastRewardTimeStr = PlayerPrefs.GetString(GlobalConst.PresentDaily, DateTime.MinValue.ToString());
            m_lastRewardTime = DateTime.Parse(lastRewardTimeStr);
        }

        bool CanClaimReward()
        {
            return (DateTime.Now - m_lastRewardTime).TotalHours >= 24;
        }
        bool HasMissedReward()
        {
            if (!PlayerPrefs.HasKey(GlobalConst.PresentDaily)) return false;
            string lastRewardTimeStr = PlayerPrefs.GetString(GlobalConst.PresentDaily, DateTime.MinValue.ToString());
            var lastRewardTime = DateTime.Parse(lastRewardTimeStr);
            return (DateTime.Now - lastRewardTime).TotalHours >= 48;
        }
        private void AnimateCoinArc(float duration)
        {
            var start = m_viewModel.StartFlyCoins.position;
            var target = m_viewModel.FinishFlyCoins.position;
            var coin = m_viewModel.Coin.gameObject;
            coin.SetActive(true);
            m_viewModel.ParticleSystemMain.Play();
            coin.transform.position = start;
            Vector3 controlPoint = (start + target) / 2f;
            controlPoint.y += 3f;
            DOTween.To(() => 0f, t =>
            {
                Vector3 m1 = Vector3.Lerp(start, controlPoint, t);
                Vector3 m2 = Vector3.Lerp(controlPoint, target, t);
                coin.transform.position = Vector3.Lerp(m1, m2, t);
            }, 1f, duration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                m_viewModel.ParticleSystemMoney.Play();
                            var reward = m_viewModel.DailyBonusSO[m_count];
                            var coins = SaveManager.LoadInt(GlobalConst.CoinsNow);
                            coins += reward.coinCount;
                            SaveManager.Save(GlobalConst.CoinsNow, coins);
                            SetCoins();
                            coin.SetActive(false);
            });
               
        }
        public void ClaimReward()
        {
            if (CanClaimReward())
            {
                if (m_count >= m_viewModel.DailyBonusSO.Count)
                    m_count = 0;
                AnimateCoinArc(1);
                m_viewModel.DailyItems[m_count].SetInfo(true);
 

                m_viewModel.DailyItems[m_count].SetInfo(true);

                m_count++;
                SaveManager.Save(GlobalConst.CountDaily, m_count);

                m_lastRewardTime = DateTime.Now;
                PlayerPrefs.SetString(GlobalConst.PresentDaily, m_lastRewardTime.ToString());
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Reward already claimed for today. Come back later.");
            }
        }
        private void GetBonus()
        {
            LoadLastRewardTime();
          
            if (m_count == GlobalConst.MaxDaysPresent)
            {
                for (int i = 0; i < m_count; i++)
                    m_viewModel.DailyItems[i].SetInfo(false);

                m_count = 0;
                SaveManager.Save(GlobalConst.CountDaily, m_count);
            }
            ClaimReward();

        }
        private void DebugShow(OnDebugButtonSignal onDebugButtonSignal)
        {
            if (onDebugButtonSignal.TypeButtonMainMenu == TypeButtonMainMenu.Daily)
            {
                LoadLastRewardTime();
                ShowDaily();
            }
        }
        protected override void BindEvents()
        {          
            m_signalBus.Subscribe<OnResetDailySignal>(SetReload);
            m_signalBus.Subscribe<OnDebugButtonSignal>(DebugShow);
        }

        protected override void UnbindEvents()
        {
            m_signalBus.TryUnsubscribe<OnResetDailySignal>(SetReload);
            m_signalBus.TryUnsubscribe<OnDebugButtonSignal>(DebugShow);
        }
    }
}