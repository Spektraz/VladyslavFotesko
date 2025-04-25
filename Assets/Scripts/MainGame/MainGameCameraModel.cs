using UnityEngine;
namespace MainGame
{
    public class MainGameCameraModel : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera m_mainGameCamera;

        public Camera MainGameCamera => m_mainGameCamera;
    }
}