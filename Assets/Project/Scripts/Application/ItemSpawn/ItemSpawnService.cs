using UnityEngine;

public class ItemSpawnService : IItemSpawnService
{
    private readonly CrateContentDatabase contentDatabase;
    private readonly IItemFactory itemFactory;
    public ItemSpawnService(
        CrateContentDatabase contentDatabase, 
        IItemFactory itemFactory)
    {
        this.contentDatabase = contentDatabase;
        this.itemFactory = itemFactory;
    }

    public void SpawnItem(Vector3 spawnPos)
    {
        GameObject prefab = contentDatabase.GetPrefab();
        if(prefab != null)
        {
            itemFactory.CreateItem(prefab, spawnPos);
        }
    }
}