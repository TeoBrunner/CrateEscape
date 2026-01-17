using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ServiceInstaller", menuName = "Installers/ServiceInstaller")]
public class ServiceInstaller : ScriptableObjectInstaller<ServiceInstaller>
{
    [SerializeField] private AudioView audioViewPrefab;
    [SerializeField] private CrateView crateViewPrefab;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerPrefsSaveService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<MockAdsService>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<LevelProvider>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<LifeService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CurrencyService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ReviveService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CarSelectionService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerControlService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerSpawnService>().AsSingle().NonLazy();

        Container.BindMemoryPool<CrateView, CrateView.Pool>()
             .WithInitialSize(20)
             .FromComponentInNewPrefab(crateViewPrefab)
             .UnderTransformGroup("CratesPool");
        Container.BindInterfacesAndSelfTo<CrateSpawnService>().AsSingle().NonLazy();

        Container.Bind<AudioView>().FromComponentInNewPrefab(audioViewPrefab).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AudioService>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<GameFlowService>().AsSingle().NonLazy();
    }
}