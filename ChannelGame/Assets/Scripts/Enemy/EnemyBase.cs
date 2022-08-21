using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject experience;
    [SerializeField] private float life;
    [SerializeField] private float _speed;

    private float maxLife;
    private EnemyView _enemyView;
    private PlayerMovement _target;
    private Rigidbody2D _rbd;
    
    private List<ProjectileBase> _projectileHited = new List<ProjectileBase>();


    public void TakeDamage(float damage, ProjectileBase projectile)
    {
        if (_projectileHited.Contains(projectile))
            return;
        //_projectileHited.Add(projectile);
        life -= damage;
        _enemyView.UpdateHealthBar(life/maxLife);
        if (life <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(experience,transform.position,experience.transform.rotation);
        }
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
}