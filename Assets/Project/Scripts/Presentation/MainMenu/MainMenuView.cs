using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private CanvasGroup canvasGroup;
    private IGameFlowService gameFlowService;
    private IGameStateService gameStateService;

    [Inject]
    public void Construct(IGameFlowService gameFlowService, IGameStateService gameStateService)
    {
        this.gameFlowService = gameFlowService;
        this.gameStateService = gameStateService;
    }
    private void Start()
    {
        playButton.OnClickAsObservable().Subscribe(_=>gameFlowService.StartGame()).AddTo(this);
        gameStateService.CurrentState.Subscribe(OnStateChanged).AddTo(this);

        print(gameStateService.CurrentState.Value);
    }
    private void OnStateChanged(GameState state)
    {
        if (state == GameState.MainMenu)
        {
            canvasGroup.DOFade(1,0.2f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.DOFade(0, 0.2f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
