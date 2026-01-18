using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemFactory : IItemFactory
{
    private readonly DiContainer container;

    private readonly Dictionary<GameObject, IMemoryPool<PoolableItem>> pools = new();
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
        return item;
    }
    private IMemoryPool<PoolableItem> CreatePool(GameObject prefab)
    {
        var settings = new MemoryPoolSettings(initialSize: 5, maxSize: 20, PoolExpandMethods.OneAtATime);

        // Вместо несуществующего класса используем нашу кастомную фабрику
        var factory = new CustomPrefabFactory(container, prefab);

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

        public CustomPrefabFactory(DiContainer container, GameObject prefab)
        {
            this.container = container;
            this.prefab = prefab;
        }

        public PoolableItem Create()
        {
            return container.InstantiatePrefabForComponent<PoolableItem>(prefab);
        }
    }
}



