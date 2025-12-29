using System;
using UniRx;

public class GameFlowService : IGameFlowService, IDisposable
{
    private readonly IGameStateService gameStateService;
    private readonly ILifeService lifeService;
    private readonly IScoreService scoreService;
    private readonly ISaveService saveService;

    private readonly CompositeDisposable disposables = new();
    public GameFlowService(
        IGameStateService gameStateService, 
        ILifeService lifeService,
        IScoreService scoreService,
        ISaveService saveService)
    {
        this.gameStateService = gameStateService;
        this.lifeService = lifeService;
        this.scoreService = scoreService;
        this.saveService = saveService;

        SubscribeToLife();
    }
    private void SubscribeToLife()
    {
        lifeService.CurrentLife
            .Where(lives => lives <=0)
            .Subscribe( _ => OnPlayerDied())
            .AddTo(disposables);
    }

    public void StartGame() 
    {
        lifeService.Reset();
        scoreService.Reset();
        gameStateService.SetCurrentState(GameState.Playing);
    }
    public void Pause()
    {
        if (gameStateService.CurrentState.Value != GameState.Playing)
            return;

        gameStateService.SetCurrentState(GameState.Paused);
    }
    public void Resume()
    {
        if (gameStateService.CurrentState.Value != GameState.Paused)
            return;

        gameStateService.SetCurrentState(GameState.Playing);
    }
    public void OnPlayerDied()
    {
        gameStateService.SetCurrentState(GameState.GameOver);

        saveService.SaveTopScore(scoreService.TopScore.Value);
    }
    public void TryRevive()
    {
        if (gameStateService.CurrentState.Value != GameState.GameOver)
            return;

        if (lifeService.Revived.Value == true)
            return;

        gameStateService.SetCurrentState(GameState.Reviving);
    }
    public void OnReviveSuccess()
    {
        lifeService.Revive();
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
