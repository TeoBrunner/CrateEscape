using UniRx;

public interface ICurrencyService
{
    IReadOnlyReactiveProperty<int> TotalCurrency { get; }
    void Add(int amount);
    bool TrySpend(int amount);
}
