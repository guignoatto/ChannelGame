using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public Action<EnemyBase> returnToPool;
    
    [SerializeField] private GameObject experience;
    [SerializeField] private float life;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private float maxLife;
    private EnemyView _enemyView;
    private PlayerMovement _target;
    private Rigidbody2D _rbd;
    
    private List<ProjectileBase> _projectileCollided = new List<ProjectileBase>();


    public void TakeDamage(float damage, ProjectileBase projectile)
    {
        // if (_projectileHited.Contains(projectile))
        //     return;
        // _projectileHited.Add(projectile);
        life -= damage;
        _enemyView.UpdateHealthBar(life/maxLife);
        CheckDeath();
    }
    private void Start()
    {
        _enemyView = GetComponent<EnemyView>();
        _target = FindObjectOfType<PlayerMovement>();
        _rbd = GetComponent<Rigidbody2D>();
        maxLife = life;
    }

    private void Update()
    {
         transform.position = Vector3.MoveTowards(
            transform.position, _target.transform.position, _speed * Time.deltaTime
            );
    }

    private void CheckDeath()
    {
        if (life <= 0)
        {
            Instantiate(experience,transform.position,experience.transform.rotation);
            life = maxLife;
            _enemyView.UpdateHealthBar(life/maxLife);
            returnToPool?.Invoke(this);
        }
    }
}