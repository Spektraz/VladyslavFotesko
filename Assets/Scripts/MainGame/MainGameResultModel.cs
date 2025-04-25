using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainGame
{
    public class MainGameResultModel : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button m_backButton;
        [Header("Image")]
        [SerializeField] private Image m_winImage;
        [SerializeField] private Image m_looseImage;
        [Header("Canvas")]
        [SerializeField] private Canvas m_debugCanvas;
        [SerializeField] private Canvas m_mainCanvas;
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI m_textCoins;
        [SerializeField] private TextMeshProUGUI m_textResult;
        public Button BackButton => m_backButton;
        public Canvas DebugCanvas => m_debugCanvas;
        public Canvas MainCanvas => m_mainCanvas;
        public Image WinImage => m_winImage;
        public Image LooseImage => m_looseImage;
        public TextMeshProUGUI TextCoins => m_textCoins;
        public TextMeshProUGUI TextResult => m_textResult;
    }
}