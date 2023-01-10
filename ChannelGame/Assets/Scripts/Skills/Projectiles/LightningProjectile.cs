using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : ProjectileBase
{
    [SerializeField] private float _changeTargetCooldown;
    [SerializeField] private EnemyDetectionField _enemyDetectionField;
    private int _bounces;
    private bool _isFirstTarget = true;
    private Transform _firstTarget;
    private Transform _target;
    private EnemyDetectionField _detectionField;
    public void SetFirstTarget(Transform firstTarget, EnemyDetectionField detectionField)
    {
        _firstTarget = firstTarget;
        _detectionField = detectionField;
        LaunchProjectile();
    }

    public void SetBounces(int bounces)
    {
        _bounces = bounces;
    }
    protected override void Update()
    {
        base.Update();
        transform.position = Vector3.MoveTowards(
            transform.position, _target.transform.position, _projectileSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.FromToRotation(Vector3.up, ( _target.position - transform.position ).normalized);
    }
    
    
    protected override void LaunchProjectile()
    {
        if (_firstTarget == null)
            return;

        if (!_isFirstTarget)
        {
            _target = _detectionField.GetRandomEnemyPosition();
        }
        else
        {
            _target = _firstTarget;
            _isFirstTarget = false;
        }

            
        var direction = ( _target.position - transform.position ).normalized;
        
        // _rbd.velocity = direction * _projectileSpeed;
    }

    private IEnumerator SkillCooldown()
    { 
        LaunchProjectile();
        yield return null;
    }
    

    protected override void OnTriggerEnter2D(Collider2D other2D)
    {
        EnemyBase enemy = null;
        other2D.TryGetComponent(out enemy);
        if (enemy != null)
        {
            if (_bounces <= 0)
            {
                projectileDestroyed?.Invoke(_skillType, this);
                return;
            }
            if (enemy.transform == _target)
            {
                enemy.TakeDamage(_damage, this);
                StartCoroutine(SkillCooldown());
                _bounces -= 1;
            }
        }
        else
        {
            StartCoroutine(SkillCooldown());
            _bounces -= 1;
        }
    }
}
