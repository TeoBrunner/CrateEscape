using UnityEngine;

public abstract class CompositeCrateContent : ScriptableObject
{
    public abstract float Chance { get; }
    public abstract GameObject GetItem();
}