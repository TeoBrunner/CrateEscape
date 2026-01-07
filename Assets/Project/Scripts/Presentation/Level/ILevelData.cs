using UnityEngine;
public interface ILevelData 
{
     Vector3 PlayerSpawnPosition { get; }
     Quaternion PlayerSpawnRotation { get; }
}
