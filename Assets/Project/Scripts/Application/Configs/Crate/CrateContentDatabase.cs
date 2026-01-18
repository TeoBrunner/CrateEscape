using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CrateContentDatabase", menuName = "Configs/Crate/CrateContentDatabase")]
public class CrateContentDatabase : ScriptableObject
{
    [SerializeField] private List<CompositeCrateContent> contentList = new();
    public GameObject GetPrefab()
    {
        if (contentList.Count == 0)
        {
            Debug.Log("ContentList of " + name + " is empty");
            return null;
        }

        float totalChance = 0;
        foreach (var content in contentList)
        {
            totalChance += content.Chance;
        }

        float targetChance = Random.Range(0, totalChance);

        float currentChance = 0;
        foreach (var item in contentList)
        {
            currentChance += item.Chance;
            if (currentChance > targetChance)
            {
                return item.GetItem();
            }
        }
        Debug.Log("Error while getting an item from " + name);
        return null;
    }


}
