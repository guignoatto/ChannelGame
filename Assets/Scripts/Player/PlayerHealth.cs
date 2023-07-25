using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _health;
    
    public Action<float, float> OnTakeDamage;
    public Action OnPlayerDie;

    private float _maxHealth;

    public void Initialize()
    {
        _maxHealth = _health;
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnTakeDamage.Invoke(_health, _maxHealth);
        if (_health <= 0)
        {
            OnPlayerDie.Invoke();
        }
    }
}
