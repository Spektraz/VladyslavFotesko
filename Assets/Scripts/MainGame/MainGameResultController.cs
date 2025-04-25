using Assets.Scripts.Application;
using SignalClass;
using UnityEngine.SceneManagement;
using Zenject;

namespace MainGame
{
    public class MainGameResultController : Controller<MainGameResultModel>
    {
        private readonly SignalBus m_signalBus;
        private bool result;
        public MainGameResultController(MainGameResultModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
        }

        protected override void OnInitialize()
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
               m_viewModel.BackButton.onClick.AddListener(BackToMenu);
        }
        protected override void OnDispose()
        {
              m_viewModel.BackButton.onClick.RemoveListener(BackToMenu);
        } 
        private void BackToMenu()
        {
            SceneManager.LoadSceneAsync(GlobalConst.MenuGameLvl);
        }
        private void ResultFinish(OnFinishGameSignal onFinishGameSignal)
        {
            if (result)
                return;
            result = true;
            m_viewModel.MainCanvas.enabled = false;
            m_viewModel.DebugCanvas.enabled = true;
            m_viewModel.WinImage.enabled = onFinishGameSignal.isStateResult;
            m_viewModel.LooseImage.enabled = !onFinishGameSignal.isStateResult;
            m_viewModel.TextCoins.text = SaveManager.LoadInt(GlobalConst.CoinsNow).ToString();
            if(onFinishGameSignal.isStateResult)
                m_viewModel.TextResult.text = GlobalConst.WinResult;
            else
                m_viewModel.TextResult.text = GlobalConst.LooseResult;
        }
        protected override void BindEvents()
        {
            m_signalBus.Subscribe<OnFinishGameSignal>(ResultFinish);
        }

        protected override void UnbindEvents()
        {
             m_signalBus.TryUnsubscribe<OnFinishGameSignal>(ResultFinish);

        }

    }
}