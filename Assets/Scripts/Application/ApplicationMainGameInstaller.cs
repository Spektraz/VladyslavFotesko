using MainGame;
using SignalClass;
using Zenject;

namespace Assets.Scripts.Application
{
    public class ApplicationMainGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<MainGameUiModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MainGameResultModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MainGameCameraModel>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MainGamePlayerModel>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<MainGameUiController>().AsTransient();
            Container.BindInterfacesAndSelfTo<MainGameResultController>().AsTransient();
            Container.BindInterfacesAndSelfTo<MainGameCameraController>().AsTransient();
            Container.BindInterfacesAndSelfTo<MainGamePlayerController>().AsTransient();
            
            Container.BindInterfacesAndSelfTo<LooseTrigger>().AsTransient();

            Container.BindInterfacesAndSelfTo<CollisionCore>().FromComponentsInHierarchy().AsTransient();

            Container.DeclareSignal<OnFinishGameSignal>();
            Container.DeclareSignal<OnHitPlayerSignal>();
        }
    }
}