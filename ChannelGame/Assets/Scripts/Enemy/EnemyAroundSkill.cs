using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundSkill : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private int _damage;
    private int _count;
    private float _speed;
    private float _lifetime;

    public GameObject projectilePrefab;


    public void Initialize(int damage, float speed, int count, float lifetime)
    {
        _damage = damage;
        _speed = speed;
        _count = count;
        _lifetime = lifetime;
        transform.rotation = Quaternion.identity;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        AddForceSelf();
    }

    private void AddForceSelf()
    {
        float angleIncrement = 360f / _count;
        Vector2 currentDirection = transform.up;

        GameObject projectilesParent = new GameObject("Projectiles");

        for (int i = 0; i < _count; i++)
        {
            float angle = i * angleIncrement;   
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Vector2 direction = rotation * currentDirection;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, rotation, projectilesParent.transform);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = direction * _speed;

            EnemyProjectile projectilescript = projectile.GetComponent<EnemyProjectile>();
            if (projectilescript != null) {

                projectilescript.SetDamage(_damage);
            }
        }
        transform.SetParent(projectilesParent.transform);

        Destroy(projectilesParent, _lifetime);
    }

}
