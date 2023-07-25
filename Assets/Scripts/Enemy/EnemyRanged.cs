using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : EnemyBase
{
    [SerializeField] private float maxDistanceFromPlayer = 10;
    [SerializeField] private float _attackCooldown = 1;
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileDamage;
    [SerializeField] private int _projectileLifetime = 5;
    
    private float normalSpeed;
    

    protected override void Start()
    {
        normalSpeed = _speed;
        base.Start();
        StartCoroutine(LaunchProjectileCooldown());
    }

    protected override void Update()
    {
        base.Update();
        if (Vector2.Distance(transform.position, _target.transform.position) < maxDistanceFromPlayer &&
            _speed != 0)
        {
            _speed = 0;
        }
        else if (Vector2.Distance(transform.position, _target.transform.position) > maxDistanceFromPlayer &&
                  _speed == 0)
        {
            _speed = normalSpeed;
        }
    }
    
    private void LaunchProjectile()
    {
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<EnemyProjectile>();
        
        projectile.Initialize(_projectileDamage, _projectileSpeed, _target.transform);

        Destroy(projectile.gameObject, _projectileLifetime);
    }

    private IEnumerator LaunchProjectileCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackCooldown);
            LaunchProjectile();
        }
    }
}
