using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SkinMenu
{
    public class SkinMenuModel : MonoBehaviour
    {
        [Header("Skins")]
        [SerializeField] private List<SkinData> m_skinDataList = new List<SkinData>();

        [Header("Presets")]
        [SerializeField] private List<SkinPresetModel> m_skinItemPreset = new List<SkinPresetModel>();
        [Header("GameObject")]
        [SerializeField] private GameObject m_showObject;
        [Header("Transform")]
        [SerializeField] private Transform m_trasformBrush;
        [Header("Button")]
        [SerializeField] private Button m_backGameButton;
        [Header("Particle")]
        [SerializeField] private ParticleSystem m_takeParticle;

        public List<SkinData> SkinDataList => m_skinDataList;
        public List<SkinPresetModel> SkinItemPresets => m_skinItemPreset;
        public Transform TransformBrush => m_trasformBrush;
        public GameObject ShowObject => m_showObject;
        public Button BackGameButton => m_backGameButton;
        public ParticleSystem TakeParticle => m_takeParticle;
    }
}