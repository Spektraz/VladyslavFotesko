using Assets.Scripts.Application;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fortune
{
    public class CardPreset : MonoBehaviour
    {
        [SerializeField] private Button m_buttonCard;
        [SerializeField] private Image m_imageCoins;
        [SerializeField] private Image m_imageSkin;
        [SerializeField] private TextMeshProUGUI m_textMeshProUGUI;
        [SerializeField] private ParticleSystem m_particleSystem;
        [SerializeField] private Animator m_animator;
        private CardBonusDataSO m_data;
        private void Start()
        {
            m_imageCoins.enabled = false;
            m_imageSkin.enabled = false;
            m_textMeshProUGUI.enabled = false;
        }
        public void AddListenerButton(CardBonusDataSO data)
        {
            m_data = data;
            m_buttonCard.onClick.AddListener(TryOpenCard);
        }
        private void TryOpenCard()
        {
            if (!CanOpenCard())
            {
                return;
            }

            Setup(m_data);
            SaveLastCardOpenTime();
        }
        private void Setup(CardBonusDataSO data)
        {
            m_animator.SetTrigger(GlobalConst.AnimatorOpenCard);
            m_particleSystem.Play();
            if (data.bonusType == BonusType.Coins)
            {
                m_imageCoins.enabled = true;
                m_imageCoins.sprite = data.icon;
                m_textMeshProUGUI.text = data.coinCount.ToString();
                m_textMeshProUGUI.enabled = true;
                m_imageSkin.enabled = false;
                int coinsNow = SaveManager.LoadInt(GlobalConst.CoinsNow);
                coinsNow += data.coinCount;
                SaveManager.Save(GlobalConst.CoinsNow, coinsNow);
            }
            else
            {
                m_imageSkin.enabled = true;
                m_imageCoins.enabled = false;
                m_imageSkin.sprite = data.icon;
                m_textMeshProUGUI.enabled = false;
                SaveManager.Save(data.typeSkinItems);
            }
        }
        private bool CanOpenCard()
        {
            string timeStr = PlayerPrefs.GetString(GlobalConst.CardDaily, DateTime.MinValue.ToString());
            DateTime lastTime = DateTime.Parse(timeStr);
            return (DateTime.Now - lastTime).TotalHours >= 24;
        }

        private void SaveLastCardOpenTime()
        {
            SaveManager.Save(GlobalConst.CardDaily, DateTime.Now.ToString());
        }
        private void OnDestroy()
        {
            m_buttonCard.onClick.RemoveAllListeners();
        }
    }
}