using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile360 : ProjectileBase
{
    protected override void LaunchProjectile()
    {
        var heading = transform.position - _parent.position;
        var direction = heading.normalized; 
        transform.rotation = _parent.rotation;
        var moveDirection = transform.up * _projectileSpeed;
        _rbd.velocity = moveDirection;
    }
}
