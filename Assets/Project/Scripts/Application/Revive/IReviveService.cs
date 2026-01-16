using System;
using UniRx;

public interface IReviveService
{
    IReadOnlyReactiveProperty<bool> ReviveProcessing { get; }
    IObservable<Unit> OnReviveSuccess { get; }
    IObservable<Unit> OnReviveFailed { get; }
    void TryRevive();
    void TryReviveWithCurrency(int cost);
    void TryReviveWithAds();
}
