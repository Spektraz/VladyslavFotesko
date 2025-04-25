using Assets.Scripts.Application;
using SignalClass;
using UnityEngine.SceneManagement;
using Zenject;

namespace MainMenu
{
    public class MainMenuController : Controller<MainMenuModel>
    {
        private readonly SignalBus m_signalBus;

        public MainMenuController(MainMenuModel viewModel, SignalBus signalBus)
            : base(viewModel)
        {
            m_signalBus = signalBus;
            m_viewModel.StartGameButton.enabled = false;
        }

        protected override void OnInitialize()
        {
            InitializeButtons();
            m_viewModel.NamePlayer.onValueChanged.AddListener(OnNameChanged);
        }

        private void InitializeButtons()
        {
            m_viewModel.CardGameButton.onClick.AddListener(CardLevel);
            m_viewModel.DailyGameButton.onClick.AddListener(DailyBonus);
            m_viewModel.StartGameButton.onClick.AddListener(MainGameLevel);
            m_viewModel.SkinGameButton.onClick.AddListener(SkinLevel);
        }
        protected override void OnDispose()
        {
            m_viewModel.CardGameButton.onClick.RemoveListener(CardLevel);
            m_viewModel.DailyGameButton.onClick.RemoveListener(DailyBonus);
            m_viewModel.StartGameButton.onClick.RemoveListener(MainGameLevel);
            m_viewModel.SkinGameButton.onClick.RemoveListener(SkinLevel);
        }
        private void CardLevel()
        {
            SceneManager.LoadSceneAsync(GlobalConst.FortuneGameLvl);
        }
        private void DailyBonus()
        {
            m_viewModel.DailyCanvas.enabled = !m_viewModel.DailyCanvas.enabled;
            m_viewModel.DebugCanvas.enabled = !m_viewModel.DebugCanvas.enabled;
        }
        private void SetButton(OnDebugButtonSignal signal)
        {
            switch (signal.TypeButtonMainMenu)
            {
                case TypeButtonMainMenu.Card:
                    m_viewModel.CardGameButton.interactable = !m_viewModel.CardGameButton.interactable;
                    break;
                case TypeButtonMainMenu.Skin:
                    m_viewModel.SkinGameButton.interactable = !m_viewModel.SkinGameButton.interactable;
                    break;
                case TypeButtonMainMenu.Game:
                    m_viewModel.StartGameButton.interactable = !m_viewModel.StartGameButton.interactable;
                    break;
                case TypeButtonMainMenu.Daily:
                    m_viewModel.DailyGameButton.interactable = !m_viewModel.DailyGameButton.interactable;
                    break;

            }
        }
        private void OnNameChanged(string text)
        {
            if (IsValidName(text))
            {
                m_viewModel.StartGameButton.enabled = true;
            }
            else
            {
                m_viewModel.StartGameButton.enabled = false;
            }
        }
        private void SkinLevel()
        {
            SceneManager.LoadSceneAsync(GlobalConst.SkinGameLvl);
        }
        private void MainGameLevel()
        {
            SceneManager.LoadSceneAsync(GlobalConst.MainGameLvl);
        }
        protected override void BindEvents()
        {
            m_signalBus.Subscribe<OnCloseDailySignal>(DailyBonus);
            m_signalBus.Subscribe<OnDebugButtonSignal>(SetButton);
        }

        protected override void UnbindEvents()
        {
            m_signalBus.TryUnsubscribe<OnCloseDailySignal>(DailyBonus);
            m_signalBus.TryUnsubscribe<OnDebugButtonSignal>(SetButton);
        }
        private bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }
    }
}