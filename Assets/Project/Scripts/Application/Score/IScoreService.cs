using UniRx;

public interface IScoreService 
{
    IReadOnlyReactiveProperty<int> CurrentScore { get; }    
    IReadOnlyReactiveProperty<int> TopScore { get; }
    void Add(int amount);
    void Reset();
}
