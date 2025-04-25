using UnityEngine;
using UnityEngine.UI;

namespace SkinMenu
{
    public class SkinPresetModel :  MonoBehaviour
    {
        [Header("SkinBrush")]
        [SerializeField] private SkinBrush m_skinBrush;
        [Header("Button")]
        [SerializeField] private Button m_chooseButton;
   
        public SkinBrush SkinBrush => m_skinBrush;
        public Button ChooseButton => m_chooseButton;
    }
}