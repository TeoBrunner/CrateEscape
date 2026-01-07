using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private LevelDataProvider levelDataProvider;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LevelDataProvider>().FromInstance(levelDataProvider).AsSingle();
    }
}