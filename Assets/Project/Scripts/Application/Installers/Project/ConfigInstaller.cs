using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
{
    [SerializeField] private AudioDatabase audioDatabase;
    [SerializeField] private CarDatabase carDatabase;
    [SerializeField] private CrateContentDatabase crateContentDatabase;
    [SerializeField] private CrateConfig crateConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(audioDatabase);
        Container.BindInstance(carDatabase);
        Container.BindInstance(crateContentDatabase);
        Container.BindInstance(crateConfig);
    }
}