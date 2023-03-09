using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 movementInput;

    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public bool lTriggerInput;

    [HideInInspector] public float moveAmount;

    private AnimationManager _animationManager;
    private PlayerControls _playerControls;
    private PlayerLocomotion _playerLocomotion;

    private void Awake()
    {
        _animationManager = GetComponent<AnimationManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();

            _playerControls.PlayerActions.Sprinting.performed += ctx => lTriggerInput = true;
            _playerControls.PlayerActions.Sprinting.canceled += ctx => lTriggerInput = false;
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
        HandleSprintingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

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
}