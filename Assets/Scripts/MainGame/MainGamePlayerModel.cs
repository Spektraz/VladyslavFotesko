using UnityEngine;

namespace MainGame
{
    public class MainGamePlayerModel : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private GameObject m_player;
        [SerializeField] private GameObject m_defaultPlayer;
        [Header("Library")]
        [SerializeField] private SkinLibrary m_skinLibrary;

        public GameObject DefaultPlayer => m_defaultPlayer;
        public GameObject Player => m_player;
        public SkinLibrary SkinLibrary => m_skinLibrary;
    }
}