using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperieceDrop : MonoBehaviour
{
    [SerializeField] private float exPoints;
    [SerializeField] private float _speed;

    private bool flyToPlayer = false;
    private Transform _target;

    public void FlyToPlayer(Transform target)
    {
        _target = target;
        flyToPlayer = true;
    }

    private void Update()
    {
        if (flyToPlayer)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, _target.position, _speed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerController pc))
        {
            pc.GetExperience(exPoints);
            Destroy(gameObject);
        }
    }
}