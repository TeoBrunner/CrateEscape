using UniRx;
using UnityEngine;

public class LifeService : ILifeService
{
    private readonly ReactiveProperty<int> currentLife;
    public IReadOnlyReactiveProperty<int> CurrentLife => currentLife;

    private readonly ReactiveProperty<bool> revived;
    public IReadOnlyReactiveProperty<bool> Revived => revived;

    private readonly int maxLives;

    public LifeService(int maxLives)
    {
        this.maxLives = maxLives;
    }

    public void Damage(int amount = 1)
    {
        currentLife.Value = Mathf.Max(0, currentLife.Value - amount);
    }

    public void Heal(int amount = 1)
    {
        currentLife.Value = Mathf.Min(currentLife.Value + amount, maxLives);
    }

    public void Reset()
    {
        currentLife.Value = maxLives;
    }

    public void Revive()
    {
        Heal();
        revived.Value = true;
    }
}