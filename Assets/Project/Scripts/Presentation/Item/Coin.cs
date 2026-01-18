using UnityEngine;
using Zenject;

public class Coin : PoolableItem, IInteractable
{
    [SerializeField] private int rewardAmount = 1;

    private ICurrencyService currencyService;

    [Inject]
    private void Construct(ICurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }
    public void Interact()
    {
        currencyService.Add(rewardAmount);
        print(currencyService.TotalCurrency.Value);
        ReturnToPool();
    }
}
