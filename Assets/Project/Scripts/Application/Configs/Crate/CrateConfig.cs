using UnityEngine;

[CreateAssetMenu(fileName = "CrateConfig", menuName = "Configs/Crate/CrateConfig")]

public class CrateConfig : ScriptableObject
{
    [SerializeField] private float crateSpawnTweenDuration = 0.3f;
    [SerializeField] private float crateActivationDelay = 1;
    [SerializeField] private float crateTransformationDelay = 2;
    public float CrateSpawnTweenDuration => crateSpawnTweenDuration;
    public float CrateActivationDelay => crateActivationDelay;
    public float CrateTransformationDelay => crateTransformationDelay;
}
