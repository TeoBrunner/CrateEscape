using UniRx;

public class ScoreService : IScoreService
{
    private readonly ReactiveProperty<int> currentScore = new();
    public IReadOnlyReactiveProperty<int> CurrentScore => currentScore;

    private readonly ReactiveProperty<int> topScore = new();
    public IReadOnlyReactiveProperty<int> TopScore => topScore;

    private ISaveService saveService;
    public ScoreService(ISaveService saveService)
    {
        this.saveService = saveService;

        topScore.Value = this.saveService.LoadTopScore();
    }
    public void Add(int amount)
    {
        currentScore.Value += amount;

        if(currentScore.Value > topScore.Value)
        {
            topScore.Value = currentScore.Value;
        }
    }

    public void Reset()
    {
        currentScore.Value = 0;
    }
}