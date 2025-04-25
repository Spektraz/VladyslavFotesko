using Assets.Scripts.Application;
using DG.Tweening;
using SignalClass;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Zenject;

namespace SkinMenu
{
    public class SkinMenuController : Controller<SkinMenuModel>
    {
        private readonly SignalBus m_signalBus;
        private GameObject currentModel;
        private AsyncOperationHandle<GameObject> modelHandle;
        private AsyncOperationHandle<Material> materialHandle;
        private Tween m_rotationTween;
        public SkinMenuController(SkinMenuModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
        }

        protected override void OnInitialize()
        {
            InitializeButtons();
            InitializeSkinPresets();    
                   m_rotationTween = m_viewModel.TransformBrush
            .DOLocalRotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        }
        private void LoadModelWithMaterial(int index)
        {
            if (index < 0 || index >= m_viewModel.SkinDataList.Count) return;

            var skinData = m_viewModel.SkinDataList[index];
            if (currentModel != null)
            {
                GameObject.Destroy(currentModel);
                Addressables.ReleaseInstance(currentModel);
                currentModel = null;
            }
            else
            {
                GameObject.Destroy(m_viewModel.ShowObject);
            }
            if (modelHandle.IsValid()) Addressables.Release(modelHandle);
            if (materialHandle.IsValid()) Addressables.Release(materialHandle);

            var existingModelHandle = skinData.ModelPrefab.OperationHandle;
            if (!existingModelHandle.IsValid() || existingModelHandle.Status != AsyncOperationStatus.Succeeded)
            {
                modelHandle = skinData.ModelPrefab.LoadAssetAsync<GameObject>();
                modelHandle.Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        InstantiateAndApplyMaterial(handle.Result, skinData);
                    }                   
                };
            }
            else
            {
                var prefab = existingModelHandle.Result as GameObject;
                InstantiateAndApplyMaterial(prefab, skinData);
            }
        }
        private void InstantiateAndApplyMaterial(GameObject prefab, SkinData skinData)
        {
            currentModel = GameObject.Instantiate(prefab, m_viewModel.TransformBrush);
            currentModel.transform.localPosition = Vector3.zero;
            currentModel.transform.localRotation = Quaternion.identity;
            currentModel.transform.localScale = Vector3.one;
            var existingMaterialHandle = skinData.Material.OperationHandle;
            if (!existingMaterialHandle.IsValid() || existingMaterialHandle.Status != AsyncOperationStatus.Succeeded)
            {
                materialHandle = skinData.Material.LoadAssetAsync<Material>();
                materialHandle.Completed += matHandle =>
                {
                    if (matHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        ApplyMaterial(currentModel, matHandle.Result);
                    }
                };
            }
            else
            {
                var material = existingMaterialHandle.Result as Material;
                ApplyMaterial(currentModel, material);
            }
        }
        private void InitializeSkinPresets()
        {
            var skinDataList = m_viewModel.SkinDataList;
            var presetModels = m_viewModel.SkinItemPresets;

            for (int i = 0; i < presetModels.Count; i++)
            {
                var model = presetModels[i];
                if (i >= skinDataList.Count) continue;

                var skinData = skinDataList[i];
                int index = i;

                var controller = new SkinPresetController(model, m_signalBus);
                controller.Initialize();
                controller.SetIndex(index);

                skinData.ModelPrefab.LoadAssetAsync<GameObject>().Completed += modelHandle =>
                {
                    if (modelHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                    {
                        var modelPrefab = modelHandle.Result;

                        skinData.Material.LoadAssetAsync<Material>().Completed += matHandle =>
                        {
                            if (matHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                            {
                                var material = matHandle.Result;

                                controller.Set3DModel(modelPrefab, material);
                            }
                        };
                    }
                };
            }
        }
        private void ApplyMaterial(GameObject model, Material material)
        {
            var renderer = model.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
        private void InitializeButtons()
        {
            m_viewModel.BackGameButton.onClick.AddListener(BackGame);
        }
        private void BackGame()
        {
            SceneManager.LoadSceneAsync(GlobalConst.MenuGameLvl);
        }
        protected override void BindEvents()
        {
            m_signalBus.Subscribe<OnChooseSkinSignal>(ChooseSkin);
        }

        protected override void UnbindEvents()
        {
            m_signalBus.TryUnsubscribe<OnChooseSkinSignal>(ChooseSkin);
        }
        private void ChooseSkin(OnChooseSkinSignal signal)
        {
            LoadModelWithMaterial(signal.SkinIndex);
            m_viewModel.TakeParticle.Play();
            SaveManager.Save(GlobalConst.SkinNowIndex, signal.SkinIndex);
        }
        protected override void OnDispose()
        {
            m_viewModel.BackGameButton.onClick.RemoveListener(BackGame);

            if (modelHandle.IsValid()) Addressables.Release(modelHandle);
            if (materialHandle.IsValid()) Addressables.Release(materialHandle);
            m_rotationTween?.Kill();
            if (currentModel != null)
            {
                GameObject.Destroy(currentModel);
                currentModel = null;
            }
        }

    }
}
