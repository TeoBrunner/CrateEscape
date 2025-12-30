
using UniRx;

public interface ICarSelectionService
{
    public IReadOnlyReactiveProperty<CarConfig> CurrentCar {  get; }
    public void ChangeCar(string id);
}
