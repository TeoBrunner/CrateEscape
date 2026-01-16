using System;
using UniRx;

public class GameFlowService : IGameFlowService, IDisposable
{
    private readonly ISaveService saveService;
    private readonly IGameStateService gameStateService;
    private readonly ILifeService lifeService;
    private readonly ICurrencyService currencyService;
    private readonly IReviveService reviveService;
    private readonly IScoreService scoreService;
    private readonly ICarSelectionService carSelectionService;
    private readonly ICrateSpawnService crateSpawnService;
    private readonly IPlayerSpawnService playerSpawnService;
    private readonly IPlayerControlService playerControlService;
    private readonly IAdsService adsService;

    private readonly CompositeDisposable disposables = new();
    public GameFlowService(
        ISaveService saveService,
        IGameStateService gameStateService, 
        ILifeService lifeService,
        ICurrencyService currencyService,
        IReviveService reviveService,
        IScoreService scoreService,
        ICarSelectionService carSelectionService,
        ICrateSpawnService crateSpawnService,
        IPlayerSpawnService playerSpawnService,
        IPlayerControlService playerControlService,
        IAdsService adsService)
    {
        this.saveService = saveService;
        this.gameStateService = gameStateService;
        this.lifeService = lifeService;
        this.currencyService = currencyService;
        this.reviveService = reviveService;
        this.scoreService = scoreService;
        this.carSelectionService = carSelectionService;
        this.crateSpawnService = crateSpawnService;
        this.playerSpawnService = playerSpawnService;
        this.playerControlService = playerControlService;
        this.adsService = adsService;

        SubscribeToLife();
        SubscribeToRevive();
    }
    private void SubscribeToLife()
    {
        lifeService.CurrentLife
            .Where(lives => lives <=0)
            .Where(_ => gameStateService.CurrentState.Value == GameState.Playing)
            .Subscribe( _ => OnPlayerDied())
            .AddTo(disposables);
    }
    private void SubscribeToRevive()
    {
        reviveService.OnReviveSuccess
            .Subscribe(_ => HandleReviveSuccess())
            .AddTo(disposables);

        reviveService.OnReviveFailed
            .Subscribe(_ => HandleReviveFailed())
            .AddTo(disposables);
    }
    private void HandleReviveSuccess()
    {
        playerControlService.SetInputEnabled(true);
        gameStateService.SetCurrentState(GameState.Playing);
    }

    private void HandleReviveFailed()
    {
        gameStateService.SetCurrentState(GameState.GameOver);
    }

    public void StartGame() 
    {
        lifeService.SetMaxLives(carSelectionService.CurrentCar.Value.MaxLives);
        crateSpawnService.RegisterDelays(
            carSelectionService.CurrentCar.Value.CrateSpawnDelay, 
            carSelectionService.CurrentCar.Value.CrateActivationDelay);
        scoreService.Reset();
        playerControlService.SetInputEnabled(true);
        playerSpawnService.SpawnPlayer();
        gameStateService.SetCurrentState(GameState.Playing);
    }
    public void Pause()
    {
        if (gameStateService.CurrentState.Value != GameState.Playing)
            return;
        playerControlService.SetInputEnabled(false);
        gameStateService.SetCurrentState(GameState.Paused);
    }
    public void Resume()
    {
        if (gameStateService.CurrentState.Value != GameState.Paused)
            return;
        playerControlService.SetInputEnabled(true);
        gameStateService.SetCurrentState(GameState.Playing);
    }
    public void OnPlayerDied()
    {
        gameStateService.SetCurrentState(GameState.GameOver);
        playerControlService.SetInputEnabled(false);
        saveService.SaveTopScore(scoreService.TopScore.Value);
    }
    public void ExitToMenu()
    {
        gameStateService.SetCurrentState(GameState.MainMenu);
    }

    public void Dispose()
    {
        disposables.Dispose();
    }
}
