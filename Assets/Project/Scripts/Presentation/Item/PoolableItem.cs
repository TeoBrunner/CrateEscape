using UnityEngine;
using Zenject;

public abstract class PoolableItem : MonoBehaviour, IPoolable
{
    protected IMemoryPool pool;
    public void SetPool (IMemoryPool pool)
    {
        this.pool = pool;
    }
    public  void OnSpawned()
    {

    }

    public void OnDespawned()
    {

    }
    public void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Despawn(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
