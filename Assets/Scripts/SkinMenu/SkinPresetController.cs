using SignalClass;
using UnityEngine;
using Zenject;

namespace SkinMenu
{
    public class SkinPresetController : Controller<SkinPresetModel>
    {
        private readonly SignalBus m_signalBus;
        private int m_index;

        [Inject]
        public SkinPresetController(SkinPresetModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
        }


        public void SetIndex(int index)
        {
            m_index = index;
        }

        protected override void OnInitialize()
        {
            m_viewModel.ChooseButton.onClick.AddListener(OnSkinClicked);
        }

        protected override void OnDispose()
        {
            m_viewModel.ChooseButton.onClick.RemoveListener(OnSkinClicked);
        }

        private void OnSkinClicked()
        {
            m_signalBus.Fire(new OnChooseSkinSignal
            {
               SkinIndex = m_index,
            });
        }
        public void Set3DModel(GameObject model, Material material)
        {
            m_viewModel.SkinBrush.Init(model, material);
        }
    }
}