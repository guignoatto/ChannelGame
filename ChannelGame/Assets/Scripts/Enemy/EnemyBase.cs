using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Windows.WebCam;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public Action<EnemyBase> returnToPool;

    [SerializeField] private GameObject experience, DeathParticle;
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
        _enemyView.UpdateHealthBar(life / maxLife);
        CheckDeath();
    }

    private void Start()
    {
        _enemyView = GetComponent<EnemyView>();
        _target = FindObjectOfType<PlayerMovement>();
        _rbd = GetComponent<Rigidbody2D>();
        maxLife = life;
        _enemyView.Initialize();
    }

    private void FixedUpdate()
    {
        var directionToPlayer = (_target.transform.position - transform.position).normalized;
        _rbd.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * _speed * Time.deltaTime;
    }

    private void Update()
    {
        if (transform.position.x - _target.transform.position.x < 0)
        {
            _enemyView.FlipSpriteLeft();
        }
        else
        {
            _enemyView.FlipSpriteRight();
        }
    }

    private void CheckDeath()
    {
        if (life <= 0)
        {
            Instantiate(experience,transform.position,experience.transform.rotation);
            var deathParticle = Instantiate(DeathParticle, transform.position, DeathParticle.transform.rotation);
            Destroy(deathParticle, 2);
            life = maxLife;
            _enemyView.UpdateHealthBar(life / maxLife);
            returnToPool?.Invoke(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.TryGetComponent(out PlayerHealth player);
        if (player != null)
        {
            player.TakeDamage(_damage);
        }    
    }
}