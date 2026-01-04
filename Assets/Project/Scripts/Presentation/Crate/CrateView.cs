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

    private IMemoryPool pool;
    public void ActivateSequence(float delay)
    {
        col.enabled = false;
        rb.isKinematic = true;
        meshRenderer.material.color = inactiveColor;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        StartCoroutine(BecomeObstacleRoutine(delay));
    }
    private IEnumerator BecomeObstacleRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        col.enabled = true;
        rb.isKinematic = false;
        meshRenderer.material.DOColor(activeColor, 0.2f);
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