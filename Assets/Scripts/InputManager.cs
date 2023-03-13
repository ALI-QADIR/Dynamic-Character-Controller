using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable once CheckNamespace
public class InputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public bool isMovementPressed;

    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;

    [HideInInspector] public bool lTriggerInput;
    [HideInInspector] public bool xButtonInput;

    [HideInInspector] public float moveAmount;

    private AnimationManager _animationManager;
    private PlayerControls _playerControls;
    private PlayerLocomotion _playerLocomotion;

    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _playerControls = new PlayerControls();

        _playerControls.PlayerMovement.Movement.started += OnMovementInput;
        _playerControls.PlayerMovement.Movement.performed += OnMovementInput;
        _playerControls.PlayerMovement.Movement.canceled += OnMovementInput;

        _playerControls.PlayerActions.Sprinting.performed += OnSprintingInput;
        _playerControls.PlayerActions.Sprinting.canceled += OnSprintingInput;

        _playerControls.PlayerActions.Jump.started += OnJumpInput;
        _playerControls.PlayerActions.Jump.canceled += OnJumpInput;
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        isMovementPressed = verticalInput != 0 || horizontalInput != 0;
    }

    private void OnSprintingInput(InputAction.CallbackContext context)
    {
        lTriggerInput = context.ReadValueAsButton();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        xButtonInput = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleJumpInput();
        HandleMovementInput();
        HandleSprintingInput();
    }

    private void HandleMovementInput()
    {
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animationManager.UpdateAnimatorValues(0, moveAmount, _playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (lTriggerInput && moveAmount > 0.5f)
            _playerLocomotion.isSprinting = true;
        else
            _playerLocomotion.isSprinting = false;
    }

    private void HandleJumpInput()
    {
        _playerLocomotion.isJumpPressed = xButtonInput;
    }

    public bool CheckInputFlags()
    {
        return (lTriggerInput || xButtonInput || isMovementPressed);
    }
}