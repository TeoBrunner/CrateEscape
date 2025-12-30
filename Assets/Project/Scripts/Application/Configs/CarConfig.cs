using UnityEngine;

[CreateAssetMenu(fileName = "CarConfig", menuName = "Configs/CarConfig")]

public class CarConfig : ScriptableObject
{
    [SerializeField] private string carId;
    [SerializeField] private string carName;
    [SerializeField] private int maxLives;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    public string CarId => carId;
    public string CarName => carName;
    public int MaxLives => maxLives;
    public float Speed => speed;
    public float TurnSpeed => turnSpeed;
}
