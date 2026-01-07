using UniRx;

public interface ILevelProvider 
{
    IReadOnlyReactiveProperty<ILevelData> CurrentLevel { get; }
    void SetLevel(ILevelData levelData);
}
