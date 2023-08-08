using System;
using UnityEngine;

public class SwordColliderActivation : MonoBehaviour
{
    [SerializeField] private Collider swordCollider;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }
    
    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
        _playerMovement.StopAttack();
    }
}
