using UnityEngine;
using UnityEngine.UI;

namespace DebugMenu
{
    public class DebugMenuModel : MonoBehaviour
    {
        [Header("Toogle")]
        [SerializeField] private Toggle m_toggleMainGame;
        [SerializeField] private Toggle m_toggleSkins;
        [SerializeField] private Toggle m_toggleDaily;
        [SerializeField] private Toggle m_toggleCards;

        public Toggle ToggleMainGame => m_toggleMainGame;
        public Toggle ToggleSkins => m_toggleSkins;
        public Toggle ToggleDaily => m_toggleDaily;
        public Toggle ToggleCards => m_toggleCards;
    }
}
