using UnityEngine;
using UnityEngine.Android;
using Zenject;

[CreateAssetMenu(fileName = "ServiceInstaller", menuName = "Installers/ServiceInstaller")]
public class ServiceInstaller : ScriptableObjectInstaller<ServiceInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ISaveService>().To<PlayerPrefsSaveService>().AsSingle().NonLazy();

        Container.Bind<IGameStateService>().To<GameStateService>().AsSingle().NonLazy();
        Container.Bind<ILifeService>().To<LifeService>().AsSingle().NonLazy();
        Container.Bind<ICurrencyService>().To<CurrencyService>().AsSingle().NonLazy();
        Container.Bind<IScoreService>().To<ScoreService>().AsSingle().NonLazy();
        Container.Bind<ICarSelectionService>().To<CarSelectionService>().AsSingle().NonLazy();
        Container.Bind<IPlayerControlService>().To<PlayerControlService>().AsSingle().NonLazy();
        Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
        Container.Bind<IAudioService>().To<AudioService>().AsSingle().NonLazy();
        
        Container.Bind<IAdsService>().To<MockAdsService>().AsSingle().NonLazy();

        Container.Bind<IGameFlowService>().To<GameFlowService>().AsSingle().NonLazy();
    }
}