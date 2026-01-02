using UniRx;

public interface IInputService
{
    public IReadOnlyReactiveProperty<bool> TurnLeft { get; }
    public IReadOnlyReactiveProperty<bool> TurnRight { get; }
    public void PressLeft(bool value);
    public void PressRight(bool value);
}