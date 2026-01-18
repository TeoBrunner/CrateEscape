using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEngine.Rendering.STP;

public class ItemFactory : IItemFactory
{
    private readonly DiContainer container;

    private readonly Dictionary<GameObject, IMemoryPool<PoolableItem>> pools = new();

    private Transform poolsRoot;
    public ItemFactory(DiContainer container)
    {
        this.container = container;
    }
    public PoolableItem CreateItem(GameObject prefab, Vector3 position)
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = CreatePool(prefab);
        }

        PoolableItem item = pools[prefab].Spawn();
        item.transform.position = position;
        item.SetPool(pools[prefab]);
        return item;
    }
    private IMemoryPool<PoolableItem> CreatePool(GameObject prefab)
    {
        if (poolsRoot == null)
            poolsRoot = new GameObject("ItemsPoolsRoot").transform;

        Transform poolContainer = new GameObject(prefab.name+"Pool").transform;
        poolContainer.SetParent(poolsRoot);

        var settings = new MemoryPoolSettings(initialSize: 20, maxSize: 320, PoolExpandMethods.Double);

        var factory = new CustomPrefabFactory(container, prefab, poolContainer);

        return container.Instantiate<MonoMemoryPool<PoolableItem>>(new object[] 
        {        
            settings,
            factory
        });
    }

    private class CustomPrefabFactory : IFactory<PoolableItem>
    {
        private readonly DiContainer container;
        private readonly GameObject prefab;
        private readonly Transform parent;

        public CustomPrefabFactory(DiContainer container, GameObject prefab, Transform parent)
        {
            this.container = container;
            this.prefab = prefab;
            this.parent = parent;
        }

        public PoolableItem Create()
        {
            return container.InstantiatePrefabForComponent<PoolableItem>(prefab,parent);
        }
    }
}



