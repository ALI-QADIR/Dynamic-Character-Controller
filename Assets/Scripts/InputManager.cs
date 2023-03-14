using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable once CheckNamespace
public class InputManager : MonoBehaviour
{
    public Vector2 movementInput;
    [HideInInspector] public bool isMovementPressed;

    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;

    /*[HideInInspector] public bool lTriggerInput;*/
    [HideInInspector] public bool lControlInput;
    [HideInInspector] public bool xButtonInput;

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

        _playerControls.PlayerMovement.Walk.performed += OnWalkInput;
        _playerControls.PlayerMovement.Walk.canceled += OnWalkInput;

        /*_playerControls.PlayerActions.Sprinting.performed += OnSprintingInput;
        _playerControls.PlayerActions.Sprinting.canceled += OnSprintingInput;*/

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

    /*private void OnSprintingInput(InputAction.CallbackContext context)
    {
        lTriggerInput = context.ReadValueAsButton();
    }*/

    private void OnWalkInput(InputAction.CallbackContext context)
    {
        lControlInput = context.ReadValueAsButton();
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
        HandleWalkInput();
    }

    private void HandleMovementInput()
    {
        float horizontal, vertical;
        if (lControlInput)
        {
            horizontal = horizontalInput / 2;
            vertical = verticalInput / 2;
        }
        else
        {
            horizontal = horizontalInput;
            vertical = verticalInput;
        }
        _animationManager.UpdateAnimatorValues(horizontal, vertical);
    }

    private void HandleWalkInput()
    {
        _playerLocomotion.isWalking = lControlInput;
    }

    private void HandleJumpInput()
    {
        _playerLocomotion.isJumpPressed = xButtonInput;
    }

    public bool CheckInputFlags()
    {
        return (xButtonInput || isMovementPressed);
    }
}