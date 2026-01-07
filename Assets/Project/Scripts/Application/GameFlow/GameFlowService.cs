using System;
using UniRx;

public class GameFlowService : IGameFlowService, IDisposable
{
    private readonly ISaveService saveService;
    private readonly IGameStateService gameStateService;
    private readonly ILifeService lifeService;
    private readonly ICurrencyService currencyService;
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
        this.scoreService = scoreService;
        this.carSelectionService = carSelectionService;
        this.crateSpawnService = crateSpawnService;
        this.playerSpawnService = playerSpawnService;
        this.playerControlService = playerControlService;
        this.adsService = adsService;

        SubscribeToLife();
    }
    private void SubscribeToLife()
    {
        lifeService.CurrentLife
            .Where(lives => lives <=0)
            .Where(_ => gameStateService.CurrentState.Value == GameState.Playing)
            .Subscribe( _ => OnPlayerDied())
            .AddTo(disposables);
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
    public void TryRevive()
    {
        if (gameStateService.CurrentState.Value != GameState.GameOver)
            return;

        if (lifeService.Revived.Value == true)
            return;

        gameStateService.SetCurrentState(GameState.Reviving);
        OnReviveSuccess();
    }
    public void TryReviveWithCurrency(int cost)
    {
        if (gameStateService.CurrentState.Value != GameState.GameOver)
            return;

        if (lifeService.Revived.Value == true)
            return;

        if (currencyService.TrySpend(cost))
        {
            gameStateService.SetCurrentState(GameState.Reviving);
            OnReviveSuccess();
        }
    }
    public void TryReviveWithAds()
    {
        if (gameStateService.CurrentState.Value != GameState.GameOver)
            return;

        if (lifeService.Revived.Value == true)
            return;

        if (adsService.IsRewardedAvailable.Value)
        {
            adsService.ShowRewarded(OnReviveSuccess, OnReviveFailed);
        }
    }
    public void OnReviveSuccess()
    {
        lifeService.Revive();
        playerControlService.SetInputEnabled(true);
        gameStateService.SetCurrentState(GameState.Playing);
    }
    public void OnReviveFailed()
    {
        if (gameStateService.CurrentState.Value != GameState.Reviving)
            return;

        gameStateService.SetCurrentState(GameState.GameOver);
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
