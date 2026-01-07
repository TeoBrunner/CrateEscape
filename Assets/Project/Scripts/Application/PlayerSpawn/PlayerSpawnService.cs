using UniRx;
using UnityEngine;
using Zenject;
public class PlayerSpawnService : IPlayerSpawnService
{
    private readonly ReactiveProperty<CarController> createdPlayer = new();
    public IReadOnlyReactiveProperty<CarController> CreatedPlayer => createdPlayer;

    private readonly DiContainer container;
    private readonly ICarSelectionService carSelectionService;
    private readonly ILevelProvider levelProvider;

    public PlayerSpawnService(
        DiContainer container,
        ICarSelectionService carSelectionService,
        ILevelProvider levelProvider)
    {
        this.container = container;
        this.carSelectionService = carSelectionService;
        this.levelProvider = levelProvider;
    }
    public void SpawnPlayer()
    {
        DespawnCurrent();

        var config = carSelectionService.CurrentCar.Value;

        GameObject carObj = container.InstantiatePrefab(config.CarPrefab);
        if(levelProvider.CurrentLevel.Value == null)
        {
            Debug.LogError("No level set in LevelProvider. Cannot spawn player.");
            return;
        }

        carObj.transform.position = levelProvider.CurrentLevel.Value.PlayerSpawnPosition;
        carObj.transform.rotation = levelProvider.CurrentLevel.Value.PlayerSpawnRotation;

        var controller = carObj.GetComponent<CarController>();
        controller.Initialize(config.Speed, config.TurnSpeed);

        createdPlayer.Value = controller;
    }

    public void DespawnCurrent()
    {
        if (createdPlayer.Value != null)
        {
            Object.Destroy(createdPlayer.Value.gameObject);
            createdPlayer.Value = null;
        }
    }
}