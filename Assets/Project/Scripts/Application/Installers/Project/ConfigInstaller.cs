using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
{
    [SerializeField] private AudioDatabase audioDatabase;
    [SerializeField] private CarDatabase carDatabase;
    public override void InstallBindings()
    {
        Container.BindInstance(audioDatabase);
        Container.BindInstance(carDatabase);
    }
}