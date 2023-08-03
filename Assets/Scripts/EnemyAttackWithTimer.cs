using System;
using UnityEngine;

public class EnemyAttackWithTimer : MonoBehaviour
{
    [SerializeField] private float reloadTimer = 3f;

    private Animator _animator;
    private float _timer;
    
    private static readonly int Throw = Animator.StringToHash("Throw");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > reloadTimer)
        {
            _timer = 0;
            _animator.SetTrigger(Throw);
        }
    }
}
