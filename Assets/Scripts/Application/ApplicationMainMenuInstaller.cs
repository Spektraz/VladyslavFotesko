using Daily;
using DebugMenu;
using MainMenu;
using SignalClass;
using Zenject;
namespace Assets.Scripts.Application
{
    public class ApplicationMainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DailyModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DebugMenuModel>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuController>().AsTransient();
            Container.BindInterfacesAndSelfTo<DailyController>().AsTransient();
            Container.BindInterfacesAndSelfTo<DebugMenuController>().AsTransient();

            Container.DeclareSignal<OnCloseDailySignal>();
        }
    }
}