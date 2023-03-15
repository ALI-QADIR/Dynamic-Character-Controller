using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable once CheckNamespace
public class InputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public bool isMovementPressed;

    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;

    /*[HideInInspector] public bool lTriggerInput;*/
    [HideInInspector] public bool sprintInput;
    [HideInInspector] public bool jumpInput;
    [HideInInspector] public bool blockInput;
    [HideInInspector] public bool dodgeInput;

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

        _playerControls.PlayerActions.Block.started += OnBlockInput;
        _playerControls.PlayerActions.Block.canceled += OnBlockInput;

        _playerControls.PlayerActions.Dodge.started += OnDodgeInput;
        _playerControls.PlayerActions.Dodge.canceled += OnDodgeInput;
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
        sprintInput = context.ReadValueAsButton();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValueAsButton();
    }

    private void OnBlockInput(InputAction.CallbackContext context)
    {
        blockInput = context.ReadValueAsButton();
    }

    private void OnDodgeInput(InputAction.CallbackContext context)
    {
        dodgeInput = context.ReadValueAsButton();
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
        HandleBlockInput();
        HandleDodgeInput();
        HandleJumpInput();
        HandleMovementInput();
        HandleWalkInput();
    }

    private void HandleMovementInput()
    {
        float horizontal, vertical;
        if (sprintInput)
        {
            horizontal = horizontalInput / 2;
            vertical = verticalInput / 2;
        }
        else
        {
            horizontal = horizontalInput;
            vertical = verticalInput;
        }

        _animationManager.UpdateMovementAnimatorValues(horizontal, vertical);
    }

    private void HandleWalkInput()
    {
        _playerLocomotion.isWalking = sprintInput;
    }

    private void HandleJumpInput()
    {
        _playerLocomotion.isJumpPressed = jumpInput;
    }

    private void HandleBlockInput()
    {
        _playerLocomotion.isBlockingPressed = blockInput;
    }

    private void HandleDodgeInput()
    {
        _playerLocomotion.isDodgePressed = dodgeInput;
    }

    public bool CheckInputFlags()
    {
        return (jumpInput || isMovementPressed);
    }
}