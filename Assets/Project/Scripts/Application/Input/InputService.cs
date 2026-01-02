using System;
using UniRx;
using Zenject;

public class InputService : IInputService, IDisposable
{
    private readonly ReactiveProperty<bool> turnLeft = new();
    public IReadOnlyReactiveProperty<bool> TurnLeft => turnLeft;

    private readonly ReactiveProperty<bool> turnRight = new();
    public IReadOnlyReactiveProperty<bool> TurnRight => turnRight;

    private IPlayerControlService playerControlService;
    private CompositeDisposable disposable = new();
    [Inject]
    public InputService(IPlayerControlService playerControlService)
    {
        this.playerControlService = playerControlService;

        this.playerControlService.IsInputEnabled.Subscribe(EnableInput).AddTo(disposable);
    }
    private void EnableInput(bool value)
    {
        if (value == false)
        {
            PressLeft(false);
            PressRight(false);
        }
    }
    public void PressLeft(bool value)
    {
        if (!playerControlService.IsInputEnabled.Value)
            return;

        turnLeft.Value = value;
    }

    public void PressRight(bool value)
    {
        if (!playerControlService.IsInputEnabled.Value)
            return;

        turnRight.Value = value;
    }

    public void Dispose()
    {
        disposable.Dispose();
    }
}