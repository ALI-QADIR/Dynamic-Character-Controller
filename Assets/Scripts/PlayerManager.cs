using UnityEngine;

// ReSharper disable once CheckNamespace
public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private PlayerLocomotion _playerLocomotion;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        _inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        _playerLocomotion.HandleBlock();
        _playerLocomotion.HandleDodge();
        if (_inputManager.CheckInputFlags())
            _playerLocomotion.HandleAllMovements();
    }
}