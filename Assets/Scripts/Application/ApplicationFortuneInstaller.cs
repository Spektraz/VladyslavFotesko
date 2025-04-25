using Fortune;
using Zenject;

namespace Assets.Scripts.Application
{
    public class ApplicationFortuneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.Bind<FortuneMainModel>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<FortuneMainController>().AsTransient();
        }
    }
}