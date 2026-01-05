using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TouchInputView : MonoBehaviour
{
    [SerializeField] TouchInputButton leftButton;
    [SerializeField] TouchInputButton rightButton;

    private IInputService inputService;

    [Inject]
    private void Construct(IInputService inputService)
    {
        this.inputService = inputService;
    }
    private void OnEnable()
    {
        leftButton.IsPressed.Subscribe(OnLeftButton).AddTo(this);
        rightButton.IsPressed.Subscribe(OnRightButton).AddTo(this);
    }
    private void OnLeftButton(bool isPressed)
    {
        inputService.PressLeft(isPressed);
    }
    private void OnRightButton(bool isPressed)
    {
        inputService.PressRight(isPressed);
    }

}
