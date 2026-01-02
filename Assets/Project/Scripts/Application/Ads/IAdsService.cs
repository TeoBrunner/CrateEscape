using System;
using UniRx;

public interface IAdsService 
{
    IReadOnlyReactiveProperty<bool> IsRewardedAvailable { get; }
    void ShowRewarded(Action onRewarded, Action onFailed);
}
