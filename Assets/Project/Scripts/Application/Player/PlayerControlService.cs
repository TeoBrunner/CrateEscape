using UniRx;

public class PlayerControlService : IPlayerControlService
{
    private readonly ReactiveProperty<bool> isInputEnabled;
    public IReadOnlyReactiveProperty<bool> IsInputEnabled => isInputEnabled;

    public void SetInputEnabled(bool isInputEnabled)
    {
        if (this.isInputEnabled.Value != isInputEnabled)
        {
            this.isInputEnabled.Value = isInputEnabled;
        }
    }
}