using System;
using UniRx;
using UnityEngine;
using Zenject;

public class CrateSpawnService : ICrateSpawnService, IInitializable, IDisposable
{
    private readonly IGameStateProvider gameStateService;
    private readonly CrateView.Pool cratePool;
    private readonly CompositeDisposable disposables = new();

    private IDisposable spawnTimer;

    private float crateSpawnDelay;
    private float crateActivationDelay;
    private Transform playerTransform;

    public CrateSpawnService(
        IGameStateProvider gameStateService,
        CrateView.Pool cratePool)
    {
        this.gameStateService = gameStateService;
        this.cratePool = cratePool;
    }


    public void Initialize()
    {
        
        gameStateService.CurrentState
            .Where(state => state == GameState.Playing)
            .Subscribe(_ => StartSpawning())
            .AddTo(disposables);

        gameStateService.CurrentState
            .Where(state => state != GameState.Playing)
            .Subscribe(_ => StopSpawning())
            .AddTo(disposables);
    }
    public void RegisterDelays(float crateSpawnDelay, float crateActivationDelay)
    {
        this.crateSpawnDelay = crateSpawnDelay;
        this.crateActivationDelay = crateActivationDelay;

    }
    public void RegisterPlayer(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
    private void StartSpawning()
    {
        spawnTimer = Observable.Interval(TimeSpan.FromSeconds(crateSpawnDelay))
            .TakeWhile(_ => gameStateService.CurrentState.Value == GameState.Playing)
            .Subscribe(_ => SpawnCrate())
            .AddTo(disposables);
    }
    private void StopSpawning()
    {
        spawnTimer?.Dispose();
    }
    private void SpawnCrate()
    {
        if (playerTransform == null) return;

        var crate = cratePool.Spawn();
        crate.transform.position = playerTransform.position;
        crate.transform.rotation = playerTransform.rotation;

        crate.ActivateSequence(crateActivationDelay);
    }
    public void Dispose()
    {
        disposables.Dispose();
    }

}