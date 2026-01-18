using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}
