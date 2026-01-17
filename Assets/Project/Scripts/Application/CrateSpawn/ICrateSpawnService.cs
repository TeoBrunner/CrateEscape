
using UnityEngine;

public interface ICrateSpawnService
{
    void RegisterSpawnDelay(float crateSpawnDelay);
    void RegisterPlayer(Transform playerTransform);
}
