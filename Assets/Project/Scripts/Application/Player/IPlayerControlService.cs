using UniRx;
public interface IPlayerControlService
{
    IReadOnlyReactiveProperty<bool> isInputEnabled { get; }
}
