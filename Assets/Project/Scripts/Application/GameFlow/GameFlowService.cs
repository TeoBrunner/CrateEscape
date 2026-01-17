using System;
using UniRx;

public class GameFlowService : IGameFlowService, IDisposable
{
    private readonly ISaveService saveService;
    private readonly IGameStateProvider gameStateProvider;
    private readonly IGameStateController gameStateController;
    private readonly ILifeService lifeService;
    private readonly ICurrencyService currencyService;
    private readonly IReviveService reviveService;
    private readonly IScoreService scoreService;
    private readonly ICarSelectionService carSelectionService;
    private readonly ICrateSpawnService crateSpawnService;
    private readonly IPlayerSpawnService playerSpawnService;
    private readonly IPlayerControlService playerControlService;
    private readonly IAdsService adsService;

    private readonly ILevelProvider levelProvider;

    private readonly CompositeDisposable disposables = new();
    public GameFlowService(
        ISaveService saveService,
        IGameStateProvider gameStateProvider, 
        IGameStateController gameStateController, 
        ILifeService lifeService,
        ICurrencyService currencyService,
        IReviveService reviveService,
        IScoreService scoreService,
        ICarSelectionService carSelectionService,
        ICrateSpawnService crateSpawnService,
        IPlayerSpawnService playerSpawnService,
        IPlayerControlService playerControlService,
        IAdsService adsService,
        ILevelProvider levelProvider)
    {
        this.saveService = saveService;
        this.gameStateProvider = gameStateProvider;
        this.gameStateController = gameStateController;
        this.lifeService = lifeService;
        this.currencyService = currencyService;
        this.reviveService = reviveService;
        this.scoreService = scoreService;
        this.carSelectionService = carSelectionService;
        this.crateSpawnService = crateSpawnService;
        this.playerSpawnService = playerSpawnService;
        this.playerControlService = playerControlService;
        this.adsService = adsService;
        this.levelProvider = levelProvider;

        SubscribeToLife();
        SubscribeToRevive();
    }
    private void SubscribeToLife()
    {
        lifeService.CurrentLife
            .Where(lives => lives <=0)
            .Where(_ => gameStateProvider.CurrentState.Value == GameState.Playing)
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
        gameStateController.SetCurrentState(GameState.Playing);
    }

    private void HandleReviveFailed()
    {
        gameStateController.SetCurrentState(GameState.GameOver);
    }

    public void StartGame() 
    {
        lifeService.SetMaxLives(carSelectionService.CurrentCar.Value.MaxLives);
        scoreService.Reset();
        playerControlService.SetInputEnabled(true);
        playerSpawnService.SpawnPlayer();
        crateSpawnService.RegisterPlayer(levelProvider.PlayerTransform.Value);
        crateSpawnService.RegisterDelays(
            carSelectionService.CurrentCar.Value.CrateSpawnDelay,
            carSelectionService.CurrentCar.Value.CrateActivationDelay);
        gameStateController.SetCurrentState(GameState.Playing);
        
    }
    public void Pause()
    {
        if (gameStateProvider.CurrentState.Value != GameState.Playing)
            return;
        playerControlService.SetInputEnabled(false);
        gameStateController.SetCurrentState(GameState.Paused);
    }
    public void Resume()
    {
        if (gameStateProvider.CurrentState.Value != GameState.Paused)
            return;
        playerControlService.SetInputEnabled(true);
        gameStateController.SetCurrentState(GameState.Playing);
    }
    public void OnPlayerDied()
    {
        gameStateController.SetCurrentState(GameState.GameOver);
        playerControlService.SetInputEnabled(false);
        saveService.SaveTopScore(scoreService.TopScore.Value);
    }
    public void ExitToMenu()
    {
        gameStateController.SetCurrentState(GameState.MainMenu);
    }

    public void Dispose()
    {
        disposables.Dispose();
    }
}
