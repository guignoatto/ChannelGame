using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileBase : MonoBehaviour
{
    
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _multiTarget = false;
    private float _projectileSpeed;
    private float _projectileDuration;
    private float _damage;
    private Transform _nearestEnemy;
    private Rigidbody2D _rbd;
    
    public void Initialize()
    {
        _rbd = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _projectileDuration);
    }
    
    private void Start()
    {
        Initialize();
        LaunchProjectile();
    }

    private void Update()
    {transform.Rotate(0f,0f,_rotationSpeed,Space.Self);
        
    }

    private void LaunchProjectile()
    {
        var heading = _nearestEnemy.position - transform.position;
        var direction = heading.normalized; 
        var moveDirection = direction * _projectileSpeed;
        _rbd.velocity = moveDirection;
    }
    private void OnTriggerEnter2D(Collider2D other2D)
    {
        if (other2D.gameObject.TryGetComponent(out EnemyBase enemy))
        {
            enemy.TakeDamage(_damage, this);
            if (!_multiTarget)
                Destroy(gameObject);
        }
    }

    public Transform NearestEnemy { set { _nearestEnemy = value; } }
    public float ProjectileDuration { set { _projectileDuration = value; } }
    public float ProjectileSpeed { set { _projectileSpeed = value; } }
    public float Damage { set { _damage = value; } }
}
