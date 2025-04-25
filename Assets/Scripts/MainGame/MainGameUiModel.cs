using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainGame
{
    public class MainGameUiModel : MonoBehaviour
    {
        [Header("Transform")]
        [SerializeField] private Transform m_startPos;
        [SerializeField] private Transform m_endPos;
        [Header("Particle")]
        [SerializeField] private ParticleSystem m_particleCoins;
        [Header("Image")]
        [SerializeField] private Image m_coinsImage;
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI m_textCoins;

        public Image CoinsImage => m_coinsImage;
        public TextMeshProUGUI TextCoins => m_textCoins;
        public ParticleSystem ParticleCoins => m_particleCoins;
        public Transform StartPos => m_startPos;
        public Transform EndPos => m_endPos;
    }
}
