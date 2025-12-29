using UniRx;
public interface ILifeService
{
    ReadOnlyReactiveProperty<int> CurrentLife { get; }
}
