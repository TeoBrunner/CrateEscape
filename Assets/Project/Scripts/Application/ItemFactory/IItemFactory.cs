using UnityEngine;

public interface IItemFactory
{
    PoolableItem CreateItem(GameObject prefab, Vector3 position);
}