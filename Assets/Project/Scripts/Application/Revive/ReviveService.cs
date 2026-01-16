using System;
using UniRx;

public class ReviveService : IReviveService
{
    private readonly ILifeService lifeService;
    private readonly ICurrencyService currencyService;
    private readonly IAdsService adsService;

    private readonly Subject<Unit> onReviveSuccess = new();
    private readonly Subject<Unit> onReviveFailed = new();
    private readonly ReactiveProperty<bool> reviveProcessing = new(false);

    public IObservable<Unit> OnReviveSuccess => onReviveSuccess;
    public IObservable<Unit> OnReviveFailed => onReviveFailed;
    public IReadOnlyReactiveProperty<bool> ReviveProcessing => reviveProcessing;
    public ReviveService(
        ILifeService lifeService, 
        ICurrencyService currencyService, 
        IAdsService adsService)
    {
        this.lifeService = lifeService;
        this.currencyService = currencyService;
        this.adsService = adsService;
    }
    public void TryRevive()
    {
        if(!CanRevive())
        {
            onReviveFailed.OnNext(Unit.Default);
            return;
        }
        Revive();
    }

    public void TryReviveWithAds()
    {
        if(!CanRevive())
        {
            onReviveFailed.OnNext(Unit.Default);
            return;
        }
        if (adsService.IsRewardedAvailable.Value)
        {
            reviveProcessing.Value = true;
            adsService.ShowRewarded(
                onRewarded: () => {
                    reviveProcessing.Value = false;
                    Revive();
                },
                onFailed: () => {
                    reviveProcessing.Value = false;
                    onReviveFailed.OnNext(Unit.Default);
                }
            );
        }
    }

    public void TryReviveWithCurrency(int cost)
    {
        if(!CanRevive())
        {
            onReviveFailed.OnNext(Unit.Default);
            return;
        }
        if (currencyService.TrySpend(cost))
        {
            Revive();
        }
        else
        {
            onReviveFailed.OnNext(Unit.Default);
        }
    }
    private bool CanRevive()
    {
        return !lifeService.Revived.Value;
    }
    private void Revive()
    {
        lifeService.Revive();
        onReviveSuccess.OnNext(Unit.Default);
    }
}