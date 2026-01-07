using UniRx;
using UnityEngine;
using Zenject;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private IInputService inputService;
    private float moveSpeed;
    private float turnSpeed;
    private bool isInitialized = false;

    private float turnInput;

    [Inject]
    public void Construct(IInputService inputService)
    {
        this.inputService = inputService;

        this.inputService.TurnLeft.Subscribe(HandleSteerLeft).AddTo(this);
        this.inputService.TurnRight.Subscribe(HandleSteerRight).AddTo(this);
    }
    public void Initialize(float speed, float turnSpeed)
    {
        this.moveSpeed = speed;
        this.turnSpeed = turnSpeed;
        this.isInitialized = true;
    }
    private void HandleSteerLeft(bool isPressed)
    {
        if (isPressed)
            turnInput -= 1f;
        else
            turnInput += 1f;
    }
    private void HandleSteerRight(bool isPressed)
    {
        if (isPressed)
            turnInput += 1f;
        else
            turnInput -= 1f;
    }
    private void FixedUpdate()
    {
        if (!isInitialized) return;

        Vector3 newPos = rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);

        if (turnInput != 0)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
