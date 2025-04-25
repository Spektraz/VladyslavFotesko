using Assets.Scripts.Application;
using SignalClass;
using Zenject;

namespace Assets.Scripts.Application
{
    public class ApplicationContainerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<GlobalConst>().AsSingle();
            Container.BindInterfacesAndSelfTo<ApplicationContainer>().AsSingle();


            SignalEvent();
        }
        private void SignalEvent()
        {
            Container.DeclareSignal<OnStartTimeSignal>();
            Container.DeclareSignal<OnTimeTickSignal>();
            Container.DeclareSignal<OnChooseCardSignal>();
            Container.DeclareSignal<OnDebugButtonSignal>();
            Container.DeclareSignal<OnResetDailySignal>();
        }
    }
}