using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public PlayerInputActions playerInputActions => _playerInputActions;
    
    public event EventHandler OnJumpAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnBlockActionPerformed;

    public event EventHandler OnBlockActionCanceled;
    
    private void Awake()
    {
         _playerInputActions = new PlayerInputActions();
        
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.Jump.performed += Jump_Performed;
        
        _playerInputActions.Player.Pause.performed += Pause_Performed;
        
        _playerInputActions.Player.Attack.performed += Attack_Performed;
        
        _playerInputActions.Player.Block.performed += Block_Performed;
        
        _playerInputActions.Player.Block.canceled += Block_Canceled;
    }

    private void Block_Canceled(InputAction.CallbackContext obj)
    {
        OnBlockActionCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Block_Performed(InputAction.CallbackContext obj)
    {
        OnBlockActionPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_Performed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= Jump_Performed;
        
        _playerInputActions.Player.Pause.performed -= Pause_Performed;
        
        _playerInputActions.Player.Attack.performed -= Attack_Performed;
        
        _playerInputActions.Player.Block.performed -= Block_Performed;
        
        _playerInputActions.Player.Block.canceled -= Block_Canceled;
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
