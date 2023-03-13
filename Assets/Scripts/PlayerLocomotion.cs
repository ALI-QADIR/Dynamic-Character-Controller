using UnityEngine;

#pragma warning disable CS0649

// ReSharper disable once CheckNamespace
public class PlayerLocomotion : MonoBehaviour
{
    #region Movement Speed Variables

    [Header("Movement Speeds")]
    [Tooltip("Speed at which player walks")]
    public float walkingSpeed = 1.5f;

    [Tooltip("Speed at which player runs")]
    public float runningSpeed = 5f;

    [Tooltip("Speed at which player sprints")]
    public float sprintingSpeed = 7f;

    [Tooltip("Speed at which player rotates")]
    public float rotationSpeed = 15f;

    [HideInInspector]
    public bool isSprinting;

    #endregion Movement Speed Variables

    #region Jumping Mechanic Variables

    [Header("Jumping Mechanic")]
    public float jumpForce = 150;

    [Tooltip("Additional gravity multiplier for snappy falls"), SerializeField]
    private float _fallMultiplier;

    [Tooltip("Checks if the player is jumping or not")]
    public bool isJumpPressed = false;

    [Tooltip("Ground Check empty object that is placed at the feet of the character")]
    public Transform groundCheck;

    [Tooltip("Ground Layer Mask"), SerializeField] private LayerMask _groundLayer;

    [HideInInspector] public bool isJumping;

    #endregion Jumping Mechanic Variables

    private Vector3 _moveDirection;
    private Transform _cameraObjectTransform;

    private Rigidbody _playerRigidBody;
    private AnimationManager _animationManager;
    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _animationManager = GetComponent<AnimationManager>();

        _playerRigidBody = GetComponent<Rigidbody>();
        _cameraObjectTransform = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        _moveDirection = _cameraObjectTransform.forward * _inputManager.verticalInput;
        _moveDirection += _cameraObjectTransform.right * _inputManager.horizontalInput;
        _moveDirection.Normalize();

        float movementSpeed;
        if (isSprinting)
        {
            movementSpeed = sprintingSpeed;
        }
        else
        {
            movementSpeed = _inputManager.moveAmount > 0.5 ? runningSpeed : walkingSpeed;
        }

        Vector3 movementVelocity;
        movementVelocity.x = _moveDirection.x * movementSpeed;
        movementVelocity.y = _playerRigidBody.velocity.y;
        movementVelocity.z = _moveDirection.z * movementSpeed;

        _playerRigidBody.velocity = movementVelocity;
    }

    /*private void HandleRotation()
    {
        var targetDirection = _cameraObjectTransform.forward * _inputManager.verticalInput;
        targetDirection += _cameraObjectTransform.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        var targetRotation = Quaternion.LookRotation(targetDirection);
        var playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

        transform.rotation = playerRotation;
    }*/

    private void HandleJump()
    {
        if (isJumping || !isJumpPressed || !GroundCheck()) return;
        isJumping = true;
        _animationManager.HandleJumpAnimation(isJumping);
        _playerRigidBody.AddForce(0, jumpForce, 0, ForceMode.Acceleration);
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, _groundLayer);
    }

    private void Update()
    {
        if (!isJumpPressed || GroundCheck() || isJumping)
        {
            _animationManager.HandleJumpAnimation(!GroundCheck());
            isJumping = false;
        }

        if (!_inputManager.isMovementPressed && (_playerRigidBody.velocity.x != 0 || _playerRigidBody.velocity.z != 0))
        {
            _playerRigidBody.velocity = new Vector3(0, _playerRigidBody.velocity.y, 0);
        }

        if (_playerRigidBody.velocity.y < 0)
        {
            _playerRigidBody.velocity += (_fallMultiplier - 1) * Time.deltaTime * Physics.gravity.y * Vector3.up;
        }
    }
}