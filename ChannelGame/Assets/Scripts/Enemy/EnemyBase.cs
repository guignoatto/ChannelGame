using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Windows.WebCam;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    public Action<EnemyBase> returnToPool;

    [SerializeField] protected float _speed;
    
    [SerializeField] private GameObject experience, DeathParticle;
    [SerializeField] private float life;
    [SerializeField] private float _damage;

    protected PlayerMovement _target;
    protected Rigidbody2D _rbd;
    
    private float maxLife;
    private EnemyView _enemyView;

    public void TakeDamage(float damage, ProjectileBase projectile)
    {
        life -= damage;
        _enemyView.UpdateHealthBar(life / maxLife);

        CheckDeath();
    }

    protected virtual void Start()
    {
        _enemyView = GetComponent<EnemyView>();
        _rbd = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<PlayerMovement>();
        maxLife = life;
        _enemyView.Initialize();
    }
    
    protected virtual void FixedUpdate()
    {
        var directionToPlayer = (_target.transform.position - transform.position).normalized;
        _rbd.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * _speed * Time.fixedDeltaTime;
    }

    protected virtual void Update()
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