using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private PlayerHealthUI playerHealthUI;

    private bool _invulnerable = false;
    private bool _isDead = false;

    public UnityEvent OnTakeDamageEvent;
    public UnityEvent OnDieEvent;

    public int Health => health;
    public int MaxHealth => maxHealth;

    private void Start()
    {
        playerHealthUI.Setup(maxHealth);
        playerHealthUI.RefreshHealth(health);
    }

    public void TakeDamage(int damage)
    {
        if (!_invulnerable)
        {
            health -= damage;
            playerHealthUI.RefreshHealth(health);
            if (health <= 0 && _isDead == false)
            {
                health = 0;
                Die();
            }

            _invulnerable = true;
            Invoke(nameof(StopInvulnerable), 1f);
            OnTakeDamageEvent?.Invoke();
        }
    }

    public void TakeHealing(int healthToAdd)
    {
        health += healthToAdd;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        playerHealthUI.RefreshHealth(health);
        
        // PLAY HEALING SOUND
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }
    
    private void Die()
    {
        _isDead = true;
        Debug.Log("<color='red'>DEAD</color>");
        OnDieEvent?.Invoke();
    }
}
