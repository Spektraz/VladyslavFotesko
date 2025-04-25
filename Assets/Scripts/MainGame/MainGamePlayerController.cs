using Assets.Scripts.Application;
using SkinMenu;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
namespace MainGame
{
    public class MainGamePlayerController : Controller<MainGamePlayerModel>
    {
        private readonly SignalBus m_signalBus;

        private GameObject m_currentSkinInstance;

        public MainGamePlayerController(MainGamePlayerModel viewModel, SignalBus signalBus) 
            : base(viewModel)
        {
            m_signalBus = signalBus;
        }

        protected override async void OnInitialize()
        {
            await LoadAndApplySavedSkin();
        }

        private async Task LoadAndApplySavedSkin()
        {
            int savedSkinId = SaveManager.LoadInt(GlobalConst.SkinNowIndex);
            savedSkinId  += 1;
            var skinData = m_viewModel.SkinLibrary.skins.Find(s => s.Id == savedSkinId.ToString());

            if (skinData == null)
            {
                Debug.LogWarning($"Skin with ID {savedSkinId} not found");
                return;
            }

            await LoadAndApplySkin(skinData);
        }

        private async Task LoadAndApplySkin(SkinData skinData)
        {
            if (m_currentSkinInstance != null)
                GameObject.Destroy(m_currentSkinInstance);        
            AsyncOperationHandle<GameObject> handle = skinData.ModelPrefab.LoadAssetAsync<GameObject>();
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (m_viewModel.DefaultPlayer != null)
                    GameObject.Destroy(m_viewModel.DefaultPlayer);
                GameObject prefab = handle.Result;
                m_currentSkinInstance = GameObject.Instantiate(prefab, m_viewModel.Player.transform);
                m_currentSkinInstance.transform.localPosition = Vector3.zero;
                m_currentSkinInstance.transform.localScale = Vector3.one;

                AsyncOperationHandle<Material> materialHandle = skinData.Material.LoadAssetAsync<Material>();
                await materialHandle.Task;

                if (materialHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Material loadedMaterial = materialHandle.Result;
                    var renderers = m_currentSkinInstance.GetComponentsInChildren<Renderer>(true);
                    foreach (var renderer in renderers)
                    {
                        if (renderer.gameObject.name == GlobalConst.NameDraw)
                        {
                            renderer.material = loadedMaterial;
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to load skin prefab");
            }
        }
    }
}