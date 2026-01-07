using UnityEngine;
using UniRx;
public class LevelProvider : ILevelProvider
{
    private readonly ReactiveProperty<ILevelData> currentLevel = new();
    public IReadOnlyReactiveProperty<ILevelData> CurrentLevel => currentLevel;
    public void SetLevel(ILevelData levelData)
    {
        currentLevel.Value = levelData;
    }
}