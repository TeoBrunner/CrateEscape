using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CarDatabase", menuName = "Configs/CarDatabase")]
public class CarDatabase : ScriptableObject
{
    [SerializeField] private List<CarConfig> carConfigs = new();
    public CarConfig GetCarConfig(string id)
    {
        if(carConfigs.Count == 0)
        {
            Debug.Log("Car database is empty!");
            return null;
        }

        CarConfig car = carConfigs.FirstOrDefault(c => c.CarId == id);
        if(car == null)
        {
            return carConfigs.First();
        }

        return car;
    }
}
