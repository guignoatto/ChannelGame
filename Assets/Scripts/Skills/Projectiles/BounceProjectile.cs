using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceProjectile : ProjectileBase
{
    [SerializeField] private float _changeTargetCooldown;
    [SerializeField] private EnemyDetectionField _enemyDetectionField;
    private int _maxBounces;
    private int _bounces;
    private bool _isFirstTarget = true;
    private Transform _firstTarget;
    private Transform _target;
    private Transform _myTransform;
    private EnemyDetectionField _detectionField;
    private Collider2D _collider2D;

    public override void Initialize(Transform parent, ISkillType skillType)
    {
        _myTransform = transform;
        _rbd = GetComponent<Rigidbody2D>();
        transform.parent = parent;
        _skillType = skillType;
        _myTransform.localPosition = Vector3.zero;
        _projectileLifeCooldown = ProjectileLifeCooldown(_projectileDuration);
        StartCoroutine(_projectileLifeCooldown);
        _collider2D = GetComponent<Collider2D>();
        _collider2D.enabled = true;
    }

    public void SetFirstTarget(Transform firstTarget, EnemyDetectionField detectionField)
    {
        _firstTarget = firstTarget;
        _detectionField = detectionField;
        LaunchProjectile();
    }

    public void SetBounces(int bounces)
    {
        _maxBounces = _bounces = bounces;
    }

    protected override void Update()
    {
        base.Update();

        if (_target == null)
        {
            return;
        }
        
        if(!_target.gameObject.activeSelf)
            LaunchProjectile();

        transform.position = Vector3.MoveTowards(
            transform.position, _target.transform.position, _projectileSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.FromToRotation(Vector3.up, (_target.position - _myTransform.position).normalized);
    }


    protected override void LaunchProjectile()
    {
        if (_firstTarget == null)
            return;

        if (!_isFirstTarget)
        {
            var newTarget = _detectionField.GetRandomEnemyPosition(_target);
            Debug.Log(newTarget);
            if (newTarget == null || !newTarget.gameObject.activeSelf)
            {
                DestroyProjectile();
                return;
            }
            
            _collider2D.enabled = false;
            
            if (newTarget == _target)
            {
                _collider2D.enabled = false;
                _target = null;
                return;
            }
            if (newTarget == null)
            {
                _target = null;
                _collider2D.enabled = false;
                return;
            }
            _collider2D.enabled = true;
            _target = newTarget;
        }
        else
        {
            _target = _firstTarget;
            _isFirstTarget = false;
        }
    }

    private void DestroyProjectile()
    {
        projectileDestroyed?.Invoke(_skillType, this);
        _bounces = _maxBounces;
    }
    protected override void OnTriggerEnter2D(Collider2D other2D)
    {

            if (other2D.gameObject.transform == _target)
            {
                if (!_target.gameObject.activeSelf)
                {
                    LaunchProjectile();
                    return;
                }

                if (!other2D.TryGetComponent(out IEnemy enemy)) return;
                enemy.TakeDamage(_damage);
                _bounces -= 1;
                if (_bounces <= 0)
                {
                    DestroyProjectile();
                }
                else
                {
                    _collider2D.enabled = false;
                    LaunchProjectile();
                }
            }
    }
}