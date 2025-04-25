using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fortune
{
    public class FortuneMainModel : MonoBehaviour
    {

        [Header("List")]
        [SerializeField] private List<CardPreset> m_cardPresets = new List<CardPreset>();
        [SerializeField] private List<CardBonusDataSO> m_cardBonusDataSO= new List<CardBonusDataSO>();
        [Header("Button")]
        [SerializeField] private Button m_backButton;
        [SerializeField] private Button m_takeCardsButton;

        [Header("Particle")]
        [SerializeField] private ParticleSystem m_particleOpen;

        [Header("Animation")]
        [SerializeField] private Animator m_showCard;

   

        public List<CardPreset> CardPresets => m_cardPresets;
        public List<CardBonusDataSO> CardBonusDataSO => m_cardBonusDataSO;
        public Button BackButton => m_backButton;
        public Button TakeCardsButton => m_takeCardsButton;
        public Animator ShowCard => m_showCard;
        public ParticleSystem ParticleOpen => m_particleOpen;
    }
}