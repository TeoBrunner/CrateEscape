using UnityEngine;
using Zenject;

public class Spikes : PoolableItem, IInteractable
{
    [SerializeField] int damageAmount = 1;
    private ILifeService lifeService;

    [Inject]
    private void Construct(ILifeService lifeService)
    {
        this.lifeService = lifeService;
    }
    public void Interact()
    {
        lifeService.Damage(damageAmount);
        ReturnToPool();
    }
}
