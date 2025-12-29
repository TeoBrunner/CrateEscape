using UniRx;

public interface IGameStateService
{
    IReadOnlyReactiveProperty<GameState> CurrentState { get; }
    public void SetCurrentState(GameState gameState);
}
