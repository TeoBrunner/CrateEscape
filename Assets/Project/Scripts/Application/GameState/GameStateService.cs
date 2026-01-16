
using UniRx;

public class GameStateService : IGameStateProvider, IGameStateController
{
    private readonly ReactiveProperty<GameState> currentState = new();
    public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;

    public void SetCurrentState(GameState gameState)
    {
        currentState.Value = gameState;
    }
}
