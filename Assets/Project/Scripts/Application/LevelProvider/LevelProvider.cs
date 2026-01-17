using UnityEngine;
using UniRx;
public class LevelProvider : ILevelProvider
{
    private readonly ReactiveProperty<ILevelData> currentLevel = new();
    public IReadOnlyReactiveProperty<ILevelData> CurrentLevel => currentLevel;

    private readonly ReactiveProperty<Transform> playerTransform = new();
    public IReadOnlyReactiveProperty<Transform> PlayerTransform => playerTransform;
    public void SetLevel(ILevelData levelData)
    {
        currentLevel.Value = levelData;
    }
    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform.Value = playerTransform;
    }
}