using DG.Tweening;
using System.Collections;
using UnityEngine;
using Zenject;
public class CrateView : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private Collider col;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color inactiveColor = new Color(1, 1, 1, 0.5f);
    [SerializeField] private Color activeColor = Color.white;

    private float spawnTweenDuration;
    private float activationDelay;
    private float transformationDelay;

    private IMemoryPool pool;
    [Inject]
    private void Construct(CrateConfig config, IMemoryPool pool)
    {
        spawnTweenDuration = config.CrateSpawnTweenDuration;
        activationDelay = config.CrateActivationDelay;
        transformationDelay = config.CrateTransformationDelay;
        this.pool = pool;
    }
    
    public void ActivateSequence()
    {
        col.enabled = false;
        rb.isKinematic = true;
        meshRenderer.material.color = inactiveColor;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, spawnTweenDuration).SetEase(Ease.OutBack);

        StartCoroutine(BecomeObstacleRoutine());
    }
    private IEnumerator BecomeObstacleRoutine()
    {
        yield return new WaitForSeconds(activationDelay);

        col.enabled = true;
        rb.isKinematic = false;
        meshRenderer.material.DOColor(activeColor, 0.2f);

        yield return new WaitForSeconds(transformationDelay);

        pool.Despawn(this);

    }
    public void OnDespawned()
    {
        pool = null;
        StopAllCoroutines();
    }
    public void OnSpawned(IMemoryPool pool)
    {
        this.pool = pool;
        col.enabled = false;
    }
    public class Pool : MonoMemoryPool<CrateView>
    {
    }
}