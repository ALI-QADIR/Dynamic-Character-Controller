using UnityEngine;

#pragma warning disable CS0649

// ReSharper disable once CheckNamespace
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private int _horizontalHash;
    private int _verticalHash;
    private int _isJumpingHash;

    private void Awake()
    {
        _horizontalHash = Animator.StringToHash("Horizontal");
        _verticalHash = Animator.StringToHash("Vertical");
        _isJumpingHash = Animator.StringToHash("IsJumping");
    }

    public void HandleJumpAnimation(bool isJumping)
    {
        _animator.SetBool(_isJumpingHash, isJumping);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        // Animation Snapping

        float snappedHorizontal;
        float snappedVertical;

        #region Horizontal Snapping

        if (horizontalMovement > 0 && horizontalMovement < 0.55)
            snappedHorizontal = 0.5f;
        else if (horizontalMovement > 0.55)
            snappedHorizontal = 1;
        else if (horizontalMovement < 0 && horizontalMovement > -0.55)
            snappedHorizontal = -0.5f;
        else if (horizontalMovement < -0.55)
            snappedHorizontal = -1;
        else
            snappedHorizontal = 0;

        #endregion Horizontal Snapping

        #region Vertical Snapping

        if (verticalMovement > 0 && verticalMovement < 0.55)
            snappedVertical = 0.5f;
        else if (verticalMovement > 0.55)
            snappedVertical = 1;
        else if (verticalMovement < 0 && verticalMovement > -0.55)
            snappedVertical = -0.5f;
        else if (verticalMovement < -0.55)
            snappedVertical = -1;
        else
            snappedVertical = 0;

        #endregion Vertical Snapping

        _animator.SetFloat(_horizontalHash, snappedHorizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat(_verticalHash, snappedVertical, 0.1f, Time.deltaTime);
    }
}