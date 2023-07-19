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
    
    private void Awake()
    {
         _playerInputActions = new PlayerInputActions();
        
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Jump.performed += Jump_Performed;
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
