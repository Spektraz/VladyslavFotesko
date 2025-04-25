using Assets.Scripts.Application;
using SignalClass;
using Zenject;

namespace DebugMenu
{
    public class DebugMenuController : Controller<DebugMenuModel>
    {
        private readonly SignalBus m_signalBus;
        private bool isMainGame = false;
        private bool isClaimReward = false;
        private bool isSkin = false;
        private bool isCard = false;
        public DebugMenuController(DebugMenuModel viewModel, SignalBus signalBus)
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
            m_viewModel.ToggleMainGame.onValueChanged.AddListener((isOn) =>
            {
                 MainGameLevel();
            });

            m_viewModel.ToggleSkins.onValueChanged.AddListener((isOn) =>
            {
                SkinLevel();
            });

            m_viewModel.ToggleDaily.onValueChanged.AddListener((isOn) =>
            {
                ClaimDailyReward(); 
            });

            m_viewModel.ToggleCards.onValueChanged.AddListener((isOn) =>
            {
                 CardLevel();
            });
        }
        private void AllInfo()
        {
            if(isClaimReward && isCard && isSkin && isMainGame)
            {            
                SaveManager.ResetAll();
                m_signalBus.Fire(new OnResetDailySignal());
            }
        }
        private void ClaimDailyReward()
        {
            isClaimReward = !isClaimReward;
            m_signalBus.Fire(new OnDebugButtonSignal
            {
                TypeButtonMainMenu = TypeButtonMainMenu.Daily,
            });
           
            AllInfo();
        }
        private void CardLevel()
        {
            isCard = !isCard;
            m_signalBus.Fire(new OnDebugButtonSignal
            {
                TypeButtonMainMenu = TypeButtonMainMenu.Card,
            });
            AllInfo();
        }
        private void SkinLevel()
        {
            isSkin = !isSkin;
            m_signalBus.Fire(new OnDebugButtonSignal
            {
                TypeButtonMainMenu = TypeButtonMainMenu.Skin,
            });
            AllInfo();
        }
        private void MainGameLevel()
        {
            isMainGame = !isMainGame;
            m_signalBus.Fire(new OnDebugButtonSignal
            {
                TypeButtonMainMenu = TypeButtonMainMenu.Game,
            });
            AllInfo();
        }
        protected override void OnDispose()
        {
            m_viewModel.ToggleMainGame.onValueChanged.RemoveAllListeners();
            m_viewModel.ToggleSkins.onValueChanged.RemoveAllListeners();
            m_viewModel.ToggleDaily.onValueChanged.RemoveAllListeners();
            m_viewModel.ToggleCards.onValueChanged.RemoveAllListeners();
        }
    }
}