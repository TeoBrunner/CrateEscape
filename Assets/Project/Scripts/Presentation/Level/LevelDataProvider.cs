using UnityEngine;
using Zenject;

public class LevelDataProvider : MonoBehaviour, ILevelData
{
    [SerializeField] private Transform playerSpawnPoint;

    public Vector3 PlayerSpawnPosition => playerSpawnPoint.position;
    public Quaternion PlayerSpawnRotation => playerSpawnPoint.rotation;

    private ILevelProvider levelProvider;

    [Inject]
    public void Construct(ILevelProvider levelProvider)
    {
        this.levelProvider = levelProvider;
        this.levelProvider.SetLevel(this);
    }
}
