using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    private int _horizontal;
    private int _vertical;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _horizontal = Animator.StringToHash("Horizontal");
        _vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
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

        if (isSprinting)
            snappedVertical = 2;

        _animator.SetFloat(_horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        _animator.SetFloat(_vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}