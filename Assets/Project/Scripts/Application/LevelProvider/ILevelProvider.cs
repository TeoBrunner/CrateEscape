using UniRx;
using UnityEngine;
public interface ILevelProvider 
{
    IReadOnlyReactiveProperty<ILevelData> CurrentLevel { get; }
    IReadOnlyReactiveProperty<Transform> PlayerTransform { get; }
    void SetLevel(ILevelData levelData);
    void SetPlayerTransform(Transform playerTransform);
}
