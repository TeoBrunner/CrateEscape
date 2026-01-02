using UniRx;
public interface ILifeService
{
    IReadOnlyReactiveProperty<int> CurrentLife { get; }
    IReadOnlyReactiveProperty<bool> Revived { get; }
    void SetMaxLives(int maxLives);
    void Damage(int amount = 1);
    void Heal(int amount = 1);
    void Revive();
    void Reset();
}
