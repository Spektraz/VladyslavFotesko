using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenu
{
    public class MainMenuModel : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button m_startGameButton;
        [SerializeField] private Button m_cardGameButton;
        [SerializeField] private Button m_dailyGameButton;
        [SerializeField] private Button m_skinGameButton;

        [Header("Canvas")]
        [SerializeField] private Canvas m_dailyCanvas;
        [SerializeField] private Canvas m_debugCanvas;
        [Header("Input")]
        [SerializeField] private TMPro.TMP_InputField m_namePlayer;

        [Header("Slider")]
        [SerializeField] private Slider m_sliderExp;

        [SerializeField] private TextMeshProUGUI m_textExp;
        public Canvas DailyCanvas => m_dailyCanvas;
        public Canvas DebugCanvas => m_debugCanvas;
        public Button StartGameButton => m_startGameButton;
        public Button CardGameButton => m_cardGameButton;
        public Button DailyGameButton => m_dailyGameButton;
        public Button SkinGameButton => m_skinGameButton;
        public TMPro.TMP_InputField NamePlayer => m_namePlayer;
        public TextMeshProUGUI TextExp => m_textExp;
        public Slider SliderExp => m_sliderExp;
    }
}