using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Daily
{
    public class DailyModel : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button m_backButton;
        [SerializeField] private Button m_getButton;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI m_coinsText;
        [Header("Particle")]
        [SerializeField] private ParticleSystem m_particleSystemMoney;
        [SerializeField] private ParticleSystem m_particleSystemMain;
        [Header("Transform")]
        [SerializeField] private Transform m_startFlyCoins;
        [SerializeField] private Transform m_finishFlyCoins;
        [SerializeField] private Transform m_coin;
        [Header("List")]
        [SerializeField] private List<DailyItem> m_dailyItem = new List<DailyItem>();
        [SerializeField] private List<DailyBonusSO> m_dailyBonusSO = new List<DailyBonusSO>();

        public List<DailyItem> DailyItems => m_dailyItem;
        public List<DailyBonusSO> DailyBonusSO => m_dailyBonusSO;
        public TextMeshProUGUI CoinsText => m_coinsText;
        public Button BackButton => m_backButton;
        public Button GetButton => m_getButton;
        public Transform StartFlyCoins => m_startFlyCoins;
        public Transform FinishFlyCoins => m_finishFlyCoins;
        public Transform Coin => m_coin;
        public ParticleSystem ParticleSystemMoney => m_particleSystemMoney;
        public ParticleSystem ParticleSystemMain => m_particleSystemMain;
    }
}
