
using UniRx;

public class GameStateService : IGameStateService
{
    private readonly ReactiveProperty<GameState> currentState = new();
    public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;

    public void SetCurrentState(GameState gameState)
    {
        currentState.Value = gameState;
    }
}
