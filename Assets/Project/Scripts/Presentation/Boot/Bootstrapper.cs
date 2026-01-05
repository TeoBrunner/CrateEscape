using UnityEngine;
using Zenject;

public class Bootstrapper : MonoBehaviour
{
    private IGameFlowService gameFlowService;
    private ISaveService saveService;
    private ICarSelectionService carSelectionService;

    [Inject]
    public void Construct(
        IGameFlowService gameFlowService,
        ISaveService saveService,
        ICarSelectionService carSelectionService)
    {
        this.gameFlowService = gameFlowService;
        this.saveService = saveService;
        this.carSelectionService = carSelectionService;
    }

    private void Awake()
    {
        Debug.Log("Game Booting...");

        LoadSaveData();

        gameFlowService.ExitToMenu();
    }
    private void LoadSaveData()
    {
        string currentCarId = saveService.LoadCurrentCarId();
        carSelectionService.ChangeCar(currentCarId);
    }

}
