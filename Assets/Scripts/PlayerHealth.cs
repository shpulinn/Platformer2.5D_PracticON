using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private PlayerHealthUI playerHealthUI;
    [SerializeField] private AudioSource healthAudioSource;

    [SerializeField] private List<AudioClip> healingSounds = new List<AudioClip>();

    private bool _invulnerable = false;
    private bool _isDead = false;

    private PlayerMovement _playerMovement;

    public UnityEvent OnTakeDamageEvent;
    public UnityEvent OnDieEvent;

    public int Health => health;
    public int MaxHealth => maxHealth;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        playerHealthUI.Setup(maxHealth);
        playerHealthUI.RefreshHealth(health);
    }

    public void TakeDamage(int damage)
    {
        if (!_invulnerable && _playerMovement.IsBlocking == false)
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
        AudioClip randomClip = healingSounds[Random.Range(0, healingSounds.Count)];
        healthAudioSource.PlayOneShot(randomClip);
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
