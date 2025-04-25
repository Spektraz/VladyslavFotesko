using Assets.Scripts.Application;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Fortune
{
    public class FortuneMainController : Controller<FortuneMainModel>
    {
        private readonly SignalBus m_signalBus;
        public FortuneMainController(FortuneMainModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
        }

        protected override void OnInitialize()
        {
            InitializeButtons();
        }
        private CardBonusDataSO GetRandomBonus()
        {
            var weightedList = new List<(CardBonusDataSO data, float weight)>();

            foreach (var bonus in m_viewModel.CardBonusDataSO)
            {
                weightedList.Add((bonus, bonus.dropChance));
            }
            float totalWeight = 0f;
            foreach (var item in weightedList)
                totalWeight += item.weight;

            float randomValue = Random.Range(0, totalWeight);
            float current = 0f;

            foreach (var item in weightedList)
            {
                current += item.weight;
                if (randomValue <= current)
                    return item.data;
            }

            return weightedList[0].data; 
        }
        private void InitializeButtons()
        {
            m_viewModel.BackButton.onClick.AddListener(BackLevel);
            m_viewModel.TakeCardsButton.onClick.AddListener(TakeCards);
            foreach (var cardPreset in m_viewModel.CardPresets) 
            {
                CardBonusDataSO bonus = GetRandomBonus();
                cardPreset.AddListenerButton(bonus);
            }
        }
        protected override void OnDispose()
        {
            m_viewModel.BackButton.onClick.RemoveListener(BackLevel);
            m_viewModel.TakeCardsButton.onClick.RemoveListener(TakeCards);
        }
        private void BackLevel()
        {
            SceneManager.LoadSceneAsync(GlobalConst.MenuGameLvl);
        }

        private void TakeCards()
        {
            m_viewModel.ShowCard.SetTrigger(GlobalConst.AnimatorFlyCards);
        }


        public void CloseAnimator()
        {
            m_viewModel.ShowCard.enabled = false;
        }
    }
}
