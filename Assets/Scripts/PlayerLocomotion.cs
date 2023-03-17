using UnityEngine;

#pragma warning disable CS0649

// ReSharper disable once CheckNamespace
public class PlayerLocomotion : MonoBehaviour
{
    #region Variables

    #region Movement Mechanic Variables

    [Header("Movement Speeds")]
    [Tooltip("Speed at which player walks")]
    public float walkingSpeed = 1.5f;

    [Tooltip("Speed at which player runs")]
    public float runningSpeed = 5f;

    [Tooltip("Speed at which player rotates")]
    public float rotationSpeed = 15f;

    [HideInInspector]
    public bool isWalking;

    #endregion Movement Mechanic Variables

    #region Jumping Mechanic Variables

    [Header("Jumping Mechanic")]
    public float jumpForce = 150;

    [Tooltip("Additional gravity multiplier for snappy falls"), SerializeField]
    private float _fallMultiplier;

    [Tooltip("Ground Check empty object that is placed at the feet of the character"), SerializeField]
    private Transform _groundCheck;

    [Tooltip("Ground Layer Mask"), SerializeField] private LayerMask _groundLayer;

    [HideInInspector] public bool isJumping;

    [HideInInspector] public bool isJumpPressed = false;

    #endregion Jumping Mechanic Variables

    #region Block Mechanic Variables

    [HideInInspector] public bool isBlockingPressed;

    [HideInInspector] public bool isBlocking;

    #endregion Block Mechanic Variables

    #region Dodge Mechanic Variables

    [HideInInspector] public bool isDodgePressed;

    [HideInInspector] public bool isDodging;

    [Tooltip("The Force with which player moves up to dodge")]
    public float dodgeVelocityY = 2f;

    #endregion Dodge Mechanic Variables

    #region Private Variables

    private Vector3 _moveDirection;

    private Transform _cameraObjectTransform;

    #endregion Private Variables

    #region Component Variables

    private Rigidbody _playerRigidBody;

    private AnimationManager _animationManager;

    private InputManager _inputManager;

    #endregion Component Variables

    #endregion Variables

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _animationManager = GetComponent<AnimationManager>();

        _playerRigidBody = GetComponent<Rigidbody>();
        _cameraObjectTransform = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        if (isBlockingPressed) return;
        if (isDodging) return;
        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    private void HandleMovement()
    {
        _moveDirection = transform.forward * _inputManager.verticalInput;
        _moveDirection += transform.right * _inputManager.horizontalInput;
        _moveDirection.Normalize();

        float movementSpeed;
        if (isWalking)
            movementSpeed = walkingSpeed;
        else if (Mathf.Abs(_inputManager.verticalInput) is > 0 and < 0.7f ||
                 Mathf.Abs(_inputManager.horizontalInput) is > 0 and < 0.7f)
            movementSpeed = walkingSpeed;
        else
            movementSpeed = runningSpeed;

        Vector3 movementVelocity;
        movementVelocity.x = _moveDirection.x * movementSpeed;
        movementVelocity.y = _playerRigidBody.velocity.y;
        movementVelocity.z = _moveDirection.z * movementSpeed;

        _playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (_playerRigidBody.velocity == Vector3.zero) return;

        var currentRotationX = transform.rotation.x;
        var currentRotationZ = transform.rotation.z;

        var targetAngle = _cameraObjectTransform.eulerAngles.y;
        var targetRotationEuler = Quaternion.Euler(currentRotationX, targetAngle, currentRotationZ);
        var currentRotationEuler = Quaternion.Euler(transform.eulerAngles);

        transform.rotation = Quaternion.Lerp(currentRotationEuler, targetRotationEuler, rotationSpeed * Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
        if (isJumping || !isJumpPressed || !GroundCheck()) return;
        isJumping = true;
        _animationManager.HandleJumpAnimation(isJumping);
        _playerRigidBody.AddForce(0, jumpForce, 0, ForceMode.Acceleration);
    }

    public void HandleBlock()
    {
        if (isBlocking.Equals(isBlockingPressed)) return;
        isBlocking = isBlockingPressed;
        _animationManager.HandleBlockAnimation(isBlocking);
    }

    public void HandleDodge()
    {
        if (isDodging || !isDodgePressed || !GroundCheck() || !_inputManager.isMovementPressed) return;
        isDodging = true;
        _animationManager.HandleDodgeAnimation();
        _playerRigidBody.velocity = new Vector3(0, dodgeVelocityY, 0);
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(_groundCheck.position, 0.1f, _groundLayer);
    }

    private void Update()
    {
        if (!isJumpPressed || GroundCheck() || isJumping)
        {
            _animationManager.HandleJumpAnimation(!GroundCheck());
            isJumping = false;
        }

        if (!isDodgePressed || !GroundCheck()) { isDodging = false; }

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