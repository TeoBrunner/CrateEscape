using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystemView : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private IInputService inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        this.inputService = inputService;
    }
    private void OnEnable()
    {
        InputAction leftAction = playerInput.actions["SteerLeft"];
        InputAction rightAction = playerInput.actions["SteerRight"];

        leftAction.performed += ctx => inputService.PressLeft(true);
        leftAction.canceled += ctx => inputService.PressLeft(false);

        rightAction.performed += ctx => inputService.PressRight(true);
        rightAction.canceled += ctx => inputService.PressRight(false);
    }
    private void OnDisable()
    {
        InputAction leftAction = playerInput.actions["SteerLeft"];
        InputAction rightAction = playerInput.actions["SteerRight"];

        leftAction.performed -= ctx => inputService.PressLeft(true);
        leftAction.canceled -= ctx => inputService.PressLeft(false);

        rightAction.performed -= ctx => inputService.PressRight(true);
        rightAction.canceled -= ctx => inputService.PressRight(false);
    }
}
