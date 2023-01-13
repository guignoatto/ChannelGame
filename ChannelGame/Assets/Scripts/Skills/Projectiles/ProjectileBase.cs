using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileBase : MonoBehaviour
{
    public Action<ISkillType ,ProjectileBase> projectileDestroyed;
    
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _multiTarget = false;
    protected float _projectileSpeed;
    protected float _projectileDuration;
    protected float _damage;
    protected Transform _nearestEnemy;
    protected Transform _parent;
    protected Rigidbody2D _rbd;
    protected ISkillType _skillType;
    protected IEnumerator _projectileLifeCooldown;

    public virtual void Initialize(Transform parent, ISkillType skillType)
    {
        _rbd = GetComponent<Rigidbody2D>();
        transform.parent = parent;
        _skillType = skillType;
        transform.localPosition = Vector3.zero;
        LaunchProjectile();
        _projectileLifeCooldown = ProjectileLifeCooldown(_projectileDuration);
        StartCoroutine(_projectileLifeCooldown);
    }

    protected virtual void Update()
    {
        if (_rotationSpeed > 0)
            transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime, Space.Self);
    }

    protected virtual void LaunchProjectile()
    {
        var heading = _nearestEnemy.position - transform.position;
        var direction = heading.normalized;
        var moveDirection = direction * _projectileSpeed;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        _rbd.velocity = moveDirection;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other2D)
    {
        if (other2D.gameObject.TryGetComponent(out EnemyBase enemy))
        {
            enemy.TakeDamage(_damage, this);
            if (!_multiTarget)
            {
                StopCoroutine(_projectileLifeCooldown);
                projectileDestroyed?.Invoke(_skillType, this);
            }
        }
    }

    protected IEnumerator ProjectileLifeCooldown(float projectileDuration)
    {
        yield return new WaitForSeconds(projectileDuration);
        projectileDestroyed?.Invoke(_skillType,this);
    }

    public Transform NearestEnemy
    {
        set { _nearestEnemy = value; }
    }

    public float ProjectileDuration
    {
        set { _projectileDuration = value; }
    }

    public float ProjectileSpeed
    {
        set { _projectileSpeed = value; }
    }

    public float Damage
    {
        set { _damage = value; }
    }
}