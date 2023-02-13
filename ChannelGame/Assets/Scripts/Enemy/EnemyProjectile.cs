using System;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Transform _target;
    private int _damage;
    private float _speed;

    public void Initialize(int damage, float speed, Transform target)
    {
        _damage = damage;
        _speed = speed;
        _target = target;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, (_target.position - transform.position).normalized);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        AddForceSelf();
    }

    private void AddForceSelf()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        _rigidbody2D.AddForce(direction * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent(out PlayerHealth player)) return;
        player.TakeDamage(_damage);
        Destroy(this.gameObject);
    }
}
