using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperieceDrop : MonoBehaviour, IExperience
{
    [SerializeField] private float exPoints;
    [SerializeField] private float _speed;

    public Transform _target { get; set; }
    
    private bool flyToPlayer = false;
    
    private void FlyToPlayer()
    {
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
        if (col.gameObject.TryGetComponent(out IMagneticField mf))
        {
            FlyToPlayer();
        }
        if (col.gameObject.TryGetComponent(out IPlayerExperienceCollector pc))
        {
            pc.GetExperience(exPoints);
            Destroy(gameObject);
        }
    }

}