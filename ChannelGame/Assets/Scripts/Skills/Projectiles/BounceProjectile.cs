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
            _myTransform.position = transform.parent.position +
                                 (_myTransform.position - _myTransform.parent.position).normalized * 2;
            transform.RotateAround(_myTransform.parent.position, new Vector3(0, 0, 1), 90f * Time.deltaTime);
            return;
        }

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


        // var direction = ( _target.position - transform.position ).normalized;

        // _rbd.velocity = direction * _projectileSpeed;
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
                _bounces = _maxBounces;
                return;
            }

            if (enemy.transform == _target)
            {
                enemy.TakeDamage(_damage, this);
                LaunchProjectile();
                _bounces -= 1;
            }
        }
    }
}