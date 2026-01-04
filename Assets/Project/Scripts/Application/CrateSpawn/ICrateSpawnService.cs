
using UnityEngine;

public interface ICrateSpawnService
{
    void RegisterDelays(float crateSpawnDelay, float crateActivationDelay);
    void RegisterPlayer(Transform playerTransform);
}
