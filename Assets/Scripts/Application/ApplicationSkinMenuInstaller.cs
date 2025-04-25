using SignalClass;
using SkinMenu;
using Zenject;

namespace Assets.Scripts.Application
{
    public class ApplicationSkinMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<SkinMenuModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SkinPresetModel>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<SkinMenuController>().AsTransient();
            Container.BindInterfacesAndSelfTo<SkinPresetController>().AsTransient();

            Container.DeclareSignal<OnChooseSkinSignal>();
        }
    }
}