
using UniRx;

public class GameStateService : IGameStateService
{
    private readonly ReactiveProperty<GameState> currentState;
    public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;

    public void SetCurrentState(GameState gameState)
    {
        currentState.Value = gameState;
    }
}
