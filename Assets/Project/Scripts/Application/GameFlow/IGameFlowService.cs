
public interface IGameFlowService
{
    void StartGame();
    void Pause();
    void Resume();
    void OnPlayerDied();
    void TryRevive();
    void OnReviveSuccess();
    void OnReviveFailed();
    void ExitToMenu();
}
