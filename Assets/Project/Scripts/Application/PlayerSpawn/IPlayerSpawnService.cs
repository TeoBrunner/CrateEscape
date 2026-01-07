using UniRx;

public interface IPlayerSpawnService 
{
    IReadOnlyReactiveProperty<CarController> CreatedPlayer { get; }
    void SpawnPlayer();
}
