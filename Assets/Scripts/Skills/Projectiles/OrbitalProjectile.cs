using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OrbitalProjectile : ProjectileBase
{
    protected override void LaunchProjectile()
    {
        return;
    }

    protected override void Update()
    {
        base.Update();
        transform.position = transform.parent.position;
    }
}