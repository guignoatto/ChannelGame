using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private float _speed;
    private PlayerMovement _target;
    private Rigidbody2D _rbd;


    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        _target = FindObjectOfType<PlayerMovement>();
        _rbd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, _target.transform.position, _speed * Time.deltaTime
            );
    }
}