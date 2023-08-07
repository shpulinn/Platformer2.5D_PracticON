using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public PlayerInputActions playerInputActions => _playerInputActions;
    
    public event EventHandler OnJumpAction;
    public event EventHandler OnPauseAction;
    
    private void Awake()
    {
         _playerInputActions = new PlayerInputActions();
        
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Jump.performed += Jump_Performed;
        
        _playerInputActions.Player.Pause.performed += Pause_Performed;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= Jump_Performed;
        
        _playerInputActions.Player.Pause.performed -= Pause_Performed;
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_Performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementDirectionNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        //inputVector = inputVector.normalized;

        return inputVector.normalized;
    }
}
