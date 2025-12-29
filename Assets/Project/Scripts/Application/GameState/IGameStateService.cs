using UniRx;

public interface IGameStateService
{
    IReadOnlyReactiveProperty<GameState> CurrentState { get; }
}
