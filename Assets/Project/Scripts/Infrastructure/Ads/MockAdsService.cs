using System;
using UniRx;

public class MockAdsService : IAdsService
{
    public IReadOnlyReactiveProperty<bool> IsRewardedAvailable()
    {
        return new ReactiveProperty<bool>(true);
    }

    public void ShowRewarded(Action onRewarded, Action onFailed)
    {
        onRewarded?.Invoke();
    }

}
