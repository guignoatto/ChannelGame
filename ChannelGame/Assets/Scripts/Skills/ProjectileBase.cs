using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileBase : MonoBehaviour
{
    
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _multiTarget = false;
    protected float _projectileSpeed;
    protected float _projectileDuration;
    protected float _damage;
    protected Transform _nearestEnemy;
    protected Transform _parent;
    protected Rigidbody2D _rbd;
    
    public virtual void Initialize(Transform parent)
    {
        _rbd = GetComponent<Rigidbody2D>();
        _parent = parent;
        
        LaunchProjectile();
        Destroy(gameObject, _projectileDuration);
    }
    
    protected virtual void Update()
    {
        transform.Rotate(0f,0f,_rotationSpeed, Space.Self);
    }

    protected virtual void LaunchProjectile()
    {
        var heading = _nearestEnemy.position - transform.position;
        var direction = heading.normalized; 
        var moveDirection = direction * _projectileSpeed;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
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
