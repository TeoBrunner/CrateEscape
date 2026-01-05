using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private readonly ReactiveProperty<bool> isPressed = new(false);
    public IReadOnlyReactiveProperty<bool> IsPressed => isPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed.Value = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed.Value = false;
    }
}
