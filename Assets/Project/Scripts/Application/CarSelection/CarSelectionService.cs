using UniRx;

public class CarSelectionService : ICarSelectionService
{
    private readonly ReactiveProperty<CarConfig> currentCar = new();
    public IReadOnlyReactiveProperty<CarConfig> CurrentCar => currentCar;

    private CarDatabase database;
    public CarSelectionService (CarDatabase database)
    {
        this.database = database;
    }
    public void ChangeCar(string id)
    {
        currentCar.Value = database.GetCarConfig(id);
    }
}