using UniRx;

public interface IScoreService 
{
    IReadOnlyReactiveProperty<int> CurrentScore { get; }    
    IReadOnlyReactiveProperty<int> TopScore { get; }    
}
