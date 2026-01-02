using UniRx;
public interface IPlayerControlService
{
    IReadOnlyReactiveProperty<bool> IsInputEnabled { get; }
    public void SetInputEnabled(bool isInputEnabled);
}
