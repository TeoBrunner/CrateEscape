using UniRx;

public class CurrencyService : ICurrencyService
{
    private readonly ReactiveProperty<int> totalCurrency;
    public IReadOnlyReactiveProperty<int> TotalCurrency => totalCurrency;

    private readonly ISaveService saveService;
    public CurrencyService (ISaveService saveService)
    {
        this.saveService = saveService;
        totalCurrency.Value = this.saveService.LoadCurrency();
    }
    public void Add(int amount)
    {
        if(amount == 0)
            return;

        totalCurrency.Value += amount;
        saveService.SaveCurrency(totalCurrency.Value);
    }

    public bool TrySpend(int amount)
    {
        if(totalCurrency.Value >= amount)
        {
            totalCurrency.Value -= amount;
            saveService.SaveCurrency(totalCurrency.Value);
            return true;
        }
        return false;
    }
}