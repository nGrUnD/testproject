using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent<float> OnHit = new UnityEvent<float>();
    public UnityEvent<GameObject> OnDeath = new UnityEvent<GameObject>();
    [SerializeField] private float _health;

    private float _maxHealth;
    private float _currentHealth;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _maxHealth = _health;
        _currentHealth = _maxHealth;
    }

    public void ChangeHealth(float newHealth)
    {
        _health = newHealth;
        Init();
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        _currentHealth -= damage;
        _health = _currentHealth;
        OnHit?.Invoke(_currentHealth);
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            OnDeath?.Invoke(attacker);
        }
    }
}
