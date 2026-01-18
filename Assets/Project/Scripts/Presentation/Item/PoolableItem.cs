using UnityEngine;
using Zenject;

public abstract class PoolableItem : MonoBehaviour, IPoolable
{
    protected IMemoryPool pool;
    [Inject]
    private void Construct (IMemoryPool pool)
    {
        this.pool = pool;
    }
    public  void OnSpawned()
    {

    }

    public void OnDespawned()
    {
        pool?.Despawn(this);
    }
}
