using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace SkinMenu
{
    public class SkinBrush : MonoBehaviour
    {
        [SerializeField] private RawImage m_rawImage;
        [SerializeField] private Camera m_camera;
        [SerializeField] private Transform m_modelRoot;

        private GameObject m_currentModel;
        private Tween m_rotationTween;
        public void Init(GameObject modelPrefab, Material material)
        {
            if (m_currentModel != null)
            {
                Destroy(m_currentModel);
                m_rotationTween?.Kill();
            }
            m_currentModel = Instantiate(modelPrefab, m_modelRoot);
            m_currentModel.transform.localPosition = Vector3.zero;
            m_currentModel.transform.localRotation = Quaternion.identity;

            foreach (var renderer in m_currentModel.GetComponentsInChildren<Renderer>())
            {
                renderer.material = material;
            }
            StartRotation();
        }
        private void StartRotation()
        {
            m_rotationTween = m_currentModel.transform
                .DOLocalRotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        public void SetupCamera(RenderTexture rt = null)
        {
            if (rt == null)
            {
                rt = new RenderTexture(256, 256, 16);
            }

            m_camera.targetTexture = rt;
            m_rawImage.texture = rt;
        }
    }
}