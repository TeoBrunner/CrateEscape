using UnityEngine;

[CreateAssetMenu(fileName = "CarConfig", menuName = "Configs/Car/CarConfig")]

public class CarConfig : ScriptableObject
{
    [SerializeField] private string carId;
    [SerializeField] private string carName;
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float speed = 1;
    [SerializeField] private float turnSpeed = 1;
    [SerializeField] private float crateSpawnDelay = 0.5f;

    public string CarId => carId;
    public string CarName => carName;
    public GameObject CarPrefab => carPrefab;
    public int MaxLives => maxLives;
    public float Speed => speed;
    public float TurnSpeed => turnSpeed;
    public float CrateSpawnDelay => crateSpawnDelay;
}
