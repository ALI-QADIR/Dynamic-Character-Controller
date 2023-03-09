using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;

    private float moveAmount;

    private AnimationManager _animationManager;
    private PlayerControls _playerControls;

    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        }

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animationManager.UpdateAnimatorValues(0, moveAmount);
    }
}