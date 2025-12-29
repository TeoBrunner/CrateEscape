using System;
using UniRx;

public interface IAdsService 
{
    IReadOnlyReactiveProperty<bool> IsRewardedAvailable();
    void ShowRewarded(Action onRewarded, Action onFailed);
}
